using System;

namespace BankAccountModel.Domain
{
    public class BankAccount
    {
        protected BankAccount()
        {
        }

        internal BankAccount(BankAccountSplit bankAccountSplit, string accountNumber)
        {
            Id = Guid.NewGuid();
            BankAccountSplit = bankAccountSplit;
            AccountNumber = accountNumber;
        }

        public virtual Guid Id { get; protected set; }

        public virtual string AccountNumber { get; protected set; }

        public virtual BankAccountSplit BankAccountSplit { get; protected set; }
    }
}