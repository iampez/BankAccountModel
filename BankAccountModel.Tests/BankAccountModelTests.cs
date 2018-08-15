using System;
using BankAccountModel.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace BankAccountModel.Tests
{
    [TestClass]
    public class BankAccountModelTests
    {
        [TestMethod]
        public void CanUpdatePrimaryBankAccount()
        {
            Guid bankAccountPortfolioId;

            ISession session = SessionFactory.Create();
            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio portfolio = new BankAccountPortfolio();
                BankAccount firstBankAccount = portfolio.AddBankAccount("First-0123456789");

                session.Save(portfolio);
                bankAccountPortfolioId = portfolio.Id;

                tran.Commit();

                Assert.AreEqual(portfolio.PrimaryBankAccountSplit.BankAccount, firstBankAccount);
            }

            BankAccount secondBankAccount;
            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(bankAccountPortfolioId);
                secondBankAccount = bankAccountPortfolio.AddBankAccount("Second-9876543210");

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(bankAccountPortfolioId);
                bankAccountPortfolio.ChangePrimaryBankAccount(secondBankAccount.Id);

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(bankAccountPortfolioId);
                Assert.AreEqual(bankAccountPortfolio.PrimaryBankAccountSplit.BankAccount, secondBankAccount);
                tran.Commit();
            }
        }
    }
}
