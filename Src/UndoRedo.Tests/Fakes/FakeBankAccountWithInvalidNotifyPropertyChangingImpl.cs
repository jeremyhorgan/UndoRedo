using System.Collections.Generic;
using System.ComponentModel;

namespace UndoRedo.Tests.Fakes
{
    internal class FakeBankAccountWithInvalidNotifyPropertyChangingImpl : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private int _balance;

#pragma warning disable CS0067 // Not used in Fake but INotifyPropertyChanging signature required   
        public event PropertyChangingEventHandler PropertyChanging;
#pragma warning restore CS0067
        public event PropertyChangedEventHandler PropertyChanged;

        internal FakeBankAccountWithInvalidNotifyPropertyChangingImpl(int balance)
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

            // Invalid impl does not fire the PropertyChanging event
            // PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}