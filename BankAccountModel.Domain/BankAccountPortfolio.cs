using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountModel.Domain
{
    public class BankAccountPortfolio
    {
        private readonly ISet<BankAccountSplit> _bankAccountSplits = new HashSet<BankAccountSplit>();

        public virtual Guid Id { get; set; }

        public virtual IEnumerable<BankAccountSplit> BankAccountSplits => _bankAccountSplits.AsEnumerable();

        public virtual BankAccountSplit PrimaryBankAccountSplit { get; protected set; }

        public virtual BankAccount AddBankAccount(string accountNumber)
        {
            BankAccountSplit bankAccountSplit = new BankAccountSplit(this, accountNumber);
            _bankAccountSplits.Add(bankAccountSplit);
            if (PrimaryBankAccountSplit == null)
            {
                PrimaryBankAccountSplit = bankAccountSplit;
            }
            
            return bankAccountSplit.BankAccount;
        }

        public virtual void ChangePrimaryBankAccount(Guid bankAccountId)
        {
            BankAccountSplit bankAccountSplit = _bankAccountSplits.SingleOrDefault(x => x.BankAccount.Id == bankAccountId);
            if (bankAccountSplit != null)
            {
                PrimaryBankAccountSplit = bankAccountSplit;
            }
        }
    }
}