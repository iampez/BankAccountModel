using BankAccountModel.Domain;
using FluentNHibernate.Mapping;

namespace BankAccountModel.Persistence
{
    public class BankAccountPortfolioMap : ClassMap<BankAccountPortfolio>
    {
        public BankAccountPortfolioMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();

            References(x => x.PrimaryBankAccountSplit, "PrimaryBankAccountSplitId");

            HasMany(x => x.BankAccountSplits)
                .Inverse()
                .AsSet()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}