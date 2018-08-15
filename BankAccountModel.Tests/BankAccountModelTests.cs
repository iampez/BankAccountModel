using System;
using System.Linq;
using BankAccountModel.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace BankAccountModel.Tests
{
    [TestClass]
    public class BankAccountModelTests
    {
        [TestMethod]
        public void CanCreatePortfolioWithoutBankAccount()
        {
            BankAccountPortfolio portfolio;
            ISession session = SessionFactory.Create();
            using (var tran = session.BeginTransaction())
            {
                portfolio = new BankAccountPortfolio();

                session.Save(portfolio);

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(portfolio.Id);
                Assert.AreEqual(0, bankAccountPortfolio.BankAccountSplits.Count());
                tran.Commit();
            }
        }

        [TestMethod]
        public void CanCreatePortfolioThenAddBankAccountAtLaterDate()
        {
            BankAccountPortfolio portfolio;
            ISession session = SessionFactory.Create();
            using (var tran = session.BeginTransaction())
            {
                portfolio = new BankAccountPortfolio();

                session.Save(portfolio);

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(portfolio.Id);
                bankAccountPortfolio.AddBankAccount("First-111111111");

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(portfolio.Id);
                Assert.AreEqual(1, bankAccountPortfolio.BankAccountSplits.Count());
                tran.Commit();
            }
        }

        [TestMethod]
        public void CanUpdatePrimaryBankAccount()
        {
            BankAccountPortfolio portfolio;
            ISession session = SessionFactory.Create();
            using (var tran = session.BeginTransaction())
            {
                portfolio = new BankAccountPortfolio();
                BankAccount firstBankAccount = portfolio.AddBankAccount("First-0123456789");

                session.Save(portfolio);

                tran.Commit();

                Assert.AreEqual(portfolio.PrimaryBankAccountSplit.BankAccount, firstBankAccount);
            }

            BankAccount secondBankAccount;
            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(portfolio.Id);
                secondBankAccount = bankAccountPortfolio.AddBankAccount("Second-9876543210");

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(portfolio.Id);
                bankAccountPortfolio.ChangePrimaryBankAccount(secondBankAccount.Id);

                tran.Commit();
            }

            using (var tran = session.BeginTransaction())
            {
                BankAccountPortfolio bankAccountPortfolio = session.Get<BankAccountPortfolio>(portfolio.Id);
                Assert.AreEqual(bankAccountPortfolio.PrimaryBankAccountSplit.BankAccount, secondBankAccount);
                tran.Commit();
            }
        }
    }
}
