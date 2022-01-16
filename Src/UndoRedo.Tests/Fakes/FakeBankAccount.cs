using System.Collections.Generic;
using System.ComponentModel;

namespace UndoRedo.Tests.Fakes
{
    internal class FakeBankAccount : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private int _balance;

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        internal FakeBankAccount(int balance)
        {
            _balance = balance;
        }

        public int Balance
        {
            get => _balance;
            private set => SetField(ref _balance, value, nameof(Balance));
        }

        internal void Lodge(int amount)
        {
            Balance = _balance + amount;
        }

        private void SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
