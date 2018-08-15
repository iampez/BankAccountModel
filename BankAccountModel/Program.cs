using System;
using System.Linq;
using BankAccountModel.Domain;
using NHibernate;

namespace BankAccountModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid bankAccountPortfolioId;
            Guid bankAccountId;

            ISession session = SessionFactory.Create();
            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio portfolio = new BankAccountPortfolio();
                portfolio.AddBankAccount("0123456789");

                session.Save(portfolio);
                bankAccountPortfolioId = portfolio.Id;

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(bankAccountPortfolioId);
                bankAccountPortfolio.AddBankAccount("9876543210");

                tran.Commit();

                bankAccountId = bankAccountPortfolio.BankAccountSplits
                    .Single(x => x.BankAccount.AccountNumber == "9876543210").BankAccount.Id;
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(bankAccountPortfolioId);
                bankAccountPortfolio.ChangePrimaryBankAccount(bankAccountId);

                tran.Commit();
            }
        }
    }
}
