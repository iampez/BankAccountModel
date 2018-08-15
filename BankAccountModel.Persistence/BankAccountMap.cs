using BankAccountModel.Domain;
using FluentNHibernate.Mapping;

namespace BankAccountModel.Persistence
{
    public class BankAccountMap : ClassMap<BankAccount>
    {
        public BankAccountMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.AccountNumber);

            References(x => x.BankAccountSplit, "BankAccountSplitId");
        }
    }
}
