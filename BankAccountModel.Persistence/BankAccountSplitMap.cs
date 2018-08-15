using BankAccountModel.Domain;
using FluentNHibernate.Mapping;

namespace BankAccountModel.Persistence
{
    public class BankAccountSplitMap : ClassMap<BankAccountSplit>
    {
        public BankAccountSplitMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();

            HasOne(x => x.BankAccount)
                .PropertyRef(x => x.BankAccountSplit)
                .Cascade.All();

            References(x => x.BankAccountPortfolio, "BankAccountPortfolioId");
        }
    }
}