using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountModel.Domain
{
    public class BankAccountPortfolio
    {
        private readonly ISet<BankAccountSplit> _bankAccountSplits = new HashSet<BankAccountSplit>();

        public virtual Guid Id { get; protected set; }

        public virtual IEnumerable<BankAccountSplit> BankAccountSplits => _bankAccountSplits.AsEnumerable();

        public virtual BankAccountSplit PrimaryBankAccountSplit { get; protected set; }

        public virtual BankAccount AddBankAccount(string accountNumber)
        {
            int percentageSplit = _bankAccountSplits.Any() ? 0: 100;

            BankAccountSplit bankAccountSplit = new BankAccountSplit(this, accountNumber, percentageSplit);
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

        public virtual void ChangeBankAccountSplits(IEnumerable<BankAccountSplitDDto> bankAccountSplits)
        {
            if (bankAccountSplits.Sum(x => x.Split) != 100)
            {
                throw new ArgumentException();
            }

            if (!_bankAccountSplits.Select(x => x.BankAccount.Id).SequenceEqual(bankAccountSplits.Select(y => y.BankAccountId)))
            {
                throw new ArgumentException();   
            }

            _bankAccountSplits.ToList().ForEach(x =>
            {
                x.ChangeSplit(bankAccountSplits.Single(y => y.BankAccountId == x.BankAccount.Id).Split);
            });
        }
    }
}