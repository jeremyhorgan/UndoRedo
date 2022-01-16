using System;
using System.Collections.Generic;
using System.ComponentModel;
using UndoRedo.Core;
using UndoRedo.Tests.Fakes;
using Xunit;

namespace UndoRedo.Tests.Core
{
    public class TestUndoRedoPropertyChanges
    {
        [Fact]
        public void NotifyPropertyChanged_WhenSinglePropertyChanged_ShouldBeAbleToUndoSingleChange()
        {
            // Arrange
            var bankAccount = new FakeBankAccount(75);

            var undoService = new UndoRedoService();
            var undoRedoPropertyChanges = new UndoRedoPropertyChanges<FakeBankAccount>(undoService);
            undoRedoPropertyChanges.Subscribe(bankAccount);

            // Act
            bankAccount.Lodge(10);
            var balanceAfterLodgement = bankAccount.Balance;

            undoService.Undo();

            var balanceAfterUndo = bankAccount.Balance;
            undoRedoPropertyChanges.Unsubscribe(bankAccount);

            // Assert
            Assert.Equal(85, balanceAfterLodgement);
            Assert.Equal(75, balanceAfterUndo);
        }

        [Fact]
        public void NotifyPropertyChanged_WhenSinglePropertyChanged_ShouldBeAbleToUndoAndRedoSingleChange()
        {
            // Arrange
            var bankAccount = new FakeBankAccount(75);

            var undoService = new UndoRedoService();
            var undoRedoPropertyChanges = new UndoRedoPropertyChanges<FakeBankAccount>(undoService);
            undoRedoPropertyChanges.Subscribe(bankAccount);

            // Act
            bankAccount.Lodge(10);
            undoService.Undo();
            undoService.Redo();
            var balanceAfterRedo = bankAccount.Balance;

            undoRedoPropertyChanges.Unsubscribe(bankAccount);

            // Assert
            Assert.Equal(85, balanceAfterRedo);
        }

        [Fact]
        public void NotifyPropertyChanged_WhenMultiplePropertiesAreChanged_ShouldBeAbleToUndoMultipleChanges()
        {
            // Arrange
            var bankAccount = new FakeBankAccount(75);

            var undoService = new UndoRedoService();
            var undoRedoPropertyChanges = new UndoRedoPropertyChanges<FakeBankAccount>(undoService);
            undoRedoPropertyChanges.Subscribe(bankAccount);

            // Act
            bankAccount.Lodge(10);
            bankAccount.Lodge(15);
            bankAccount.Lodge(20);
            bankAccount.Lodge(40);
            var balanceAfterLodgement = bankAccount.Balance;

            undoService.Undo();
            undoService.Undo();
            undoService.Undo();
            undoService.Undo();
            
            var balanceAfterUndo = bankAccount.Balance;
            undoRedoPropertyChanges.Unsubscribe(bankAccount);

            // Assert
            Assert.Equal(160, balanceAfterLodgement);
            Assert.Equal(75, balanceAfterUndo);
        }

        [Fact]
        public void NotifyPropertyChanged_WhenMultiplePropertiesAreChanged_ShouldBeAbleToUndoRedoMultipleChanges()
        {
            // Arrange
            var bankAccount = new FakeBankAccount(75);

            var undoService = new UndoRedoService();
            var undoRedoPropertyChanges = new UndoRedoPropertyChanges<FakeBankAccount>(undoService);
            undoRedoPropertyChanges.Subscribe(bankAccount);

            // Act
            bankAccount.Lodge(10);
            bankAccount.Lodge(15);
            bankAccount.Lodge(20);
            bankAccount.Lodge(40);
            var balanceAfterLodgement = bankAccount.Balance;

            undoService.Undo();
            undoService.Undo();
            undoService.Undo();
            undoService.Undo();

            undoService.Redo();
            undoService.Redo();
            undoService.Redo();
            
            var balanceAfterRedo = bankAccount.Balance;
            undoRedoPropertyChanges.Unsubscribe(bankAccount);

            // Assert
            Assert.Equal(160, balanceAfterLodgement);
            Assert.Equal(120, balanceAfterRedo);
        }

        [Fact]
        public void NotifyPropertyChanged_WhenINotifyPropertyChangingNotImplementedCorrectly_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Exception expectedException = null;
            
            var bankAccount = new FakeBankAccountWithInvalidNotifyPropertyChangingImpl(100);
            var undoService = new UndoRedoService();
            var undoRedoPropertyChanges = new UndoRedoPropertyChanges<FakeBankAccountWithInvalidNotifyPropertyChangingImpl>(undoService);
            undoRedoPropertyChanges.Subscribe(bankAccount);

            undoService.Error += (sender, e) =>
            {
                expectedException = e.Exception;
            };

            // Act
            bankAccount.Lodge(10);
            undoRedoPropertyChanges.Unsubscribe(bankAccount);

            // Assert
            Assert.NotNull(expectedException);
            Assert.IsAssignableFrom<InvalidOperationException>(expectedException);
        }
    }
}