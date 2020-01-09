using System;

namespace ProposalValidator.Domain.Models
{
    public class Proponent
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public int Age { get; set; }

        public int MonthlyIncome { get; set; }

        public bool IsMain { get; set; }
    }
}
