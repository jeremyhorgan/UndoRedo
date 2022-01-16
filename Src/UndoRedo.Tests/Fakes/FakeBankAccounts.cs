using System.Collections.Generic;

namespace UndoRedo.Tests.Fakes
{
    internal class FakeBankAccounts
    {
        internal static List<FakeBankAccount> Create()
        {
            return new List<FakeBankAccount>
            {
                new FakeBankAccount(10),
                new FakeBankAccount(20),
                new FakeBankAccount(30),
                new FakeBankAccount(40),
                new FakeBankAccount(50),
            };
        }
    }
}
