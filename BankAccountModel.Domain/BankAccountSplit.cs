using System;

namespace BankAccountModel.Domain
{
    public class BankAccountSplit
    {
        protected BankAccountSplit()
        {
        }

        internal BankAccountSplit(BankAccountPortfolio bankAccountPortfolio, string accountNumber, int percentageSplit)
        {
            Id = Guid.NewGuid();
            BankAccountPortfolio = bankAccountPortfolio;
            PercentageSplit = percentageSplit;
            BankAccount = new BankAccount(this, accountNumber);
        }

        public virtual Guid Id { get; set; }

        public virtual BankAccountPortfolio BankAccountPortfolio { get; protected set; }

        public virtual BankAccount BankAccount { get; protected set; }

        public virtual int PercentageSplit { get; set; }
    }
}
