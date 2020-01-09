using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProposalValidator.Domain.Models
{
    public class Proposal
    {
        public Proposal(Guid id, decimal loanValue, int numberOfMonthlyInStallments)
        {
            Id = id;
            LoanValue = loanValue;
            NumberOfMonthlyInStallments = numberOfMonthlyInStallments;

            Proponents = new List<Proponent>();
            Warranties = new List<Warranty>();
        }

        public Guid Id { get; set; }

        public decimal LoanValue { get; set; }

        public int NumberOfMonthlyInStallments { get; set; }

        public List<Proponent> Proponents { get; set; }

        public List<Warranty> Warranties { get; set; }

        public Proponent MainProponent()
        {
            return Proponents.FirstOrDefault(x => x.IsMain);
        }
    }
}
