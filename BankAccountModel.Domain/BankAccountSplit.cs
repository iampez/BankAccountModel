using System;

namespace BankAccountModel.Domain
{
    public class BankAccountSplit
    {
        protected BankAccountSplit()
        {
        }

        internal BankAccountSplit(BankAccountPortfolio bankAccountPortfolio, string accountNumber)
        {
            BankAccountPortfolio = bankAccountPortfolio;
            BankAccount = new BankAccount(this, accountNumber);
        }

        public virtual Guid Id { get; set; }

        public virtual BankAccountPortfolio BankAccountPortfolio { get; protected set; }

        public virtual BankAccount BankAccount { get; protected set; }
    }
}
