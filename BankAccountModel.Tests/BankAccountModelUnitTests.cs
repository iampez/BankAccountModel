using BankAccountModel.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankAccountModel.Tests
{
    [TestClass]
    public class BankAccountModelUnitTests
    {
        [TestMethod]
        public void FirstBankAccountDefaultsTo100PercentSplit()
        {
            BankAccountPortfolio portfolio = new BankAccountPortfolio();
            BankAccount firstBankAccount = portfolio.AddBankAccount("666666666");

            Assert.AreEqual(100, firstBankAccount.BankAccountSplit.PercentageSplit);
        }

        [TestMethod]
        public void SecondBankAccountDefaultsTo0PercentSplit()
        {
            BankAccountPortfolio portfolio = new BankAccountPortfolio();
            BankAccount firstBankAccount = portfolio.AddBankAccount("666666666");
            BankAccount secondBankAccount = portfolio.AddBankAccount("777777777");

            Assert.AreEqual(100, firstBankAccount.BankAccountSplit.PercentageSplit);
            Assert.AreEqual(0, secondBankAccount.BankAccountSplit.PercentageSplit);
        }

        [TestMethod]
        public void CanChangeBankAccountSplit()
        {
            BankAccountPortfolio portfolio = new BankAccountPortfolio();
            BankAccount firstBankAccount = portfolio.AddBankAccount("666666666");
            BankAccount secondBankAccount = portfolio.AddBankAccount("777777777");

            portfolio.ChangeBankAccountSplits(new []
            {
                new BankAccountSplitDDto
                {
                    BankAccountId = firstBankAccount.Id,
                    Split = 50
                },
                new BankAccountSplitDDto
                {
                    BankAccountId = secondBankAccount.Id,
                    Split = 50
                }
            });

            Assert.AreEqual(50, firstBankAccount.BankAccountSplit.PercentageSplit);
            Assert.AreEqual(50, secondBankAccount.BankAccountSplit.PercentageSplit);
        }
    }
}