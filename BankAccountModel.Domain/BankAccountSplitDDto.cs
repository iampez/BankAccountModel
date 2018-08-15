using System;

namespace BankAccountModel.Domain
{
    public class BankAccountSplitDDto
    {
        public Guid BankAccountId { get; set; }

        public int Split { get; set; }
    }
}