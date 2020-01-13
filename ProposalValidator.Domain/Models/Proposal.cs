using System;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Domain.Models
{
    public class Proposal
    {
        public Proposal(Guid id, decimal loanValue, int numberOfMonthlyInStallments)
        {
            Id = id;
            LoanValue = loanValue;
            NumberOfMonthlyInstallments = numberOfMonthlyInStallments;

            Proponents = new List<Proponent>();
            Warranties = new List<Warranty>();
        }

        public Guid Id { get; set; }

        public decimal LoanValue { get; set; }

        public int NumberOfMonthlyInstallments { get; set; }

        public List<Proponent> Proponents { get; set; }

        public List<Warranty> Warranties { get; set; }

        public Proponent MainProponent()
        {
            return Proponents.FirstOrDefault(x => x.IsMain);
        }

        public void Update(decimal loanValue, int numberOfMonthlyInstallments)
        {
            LoanValue = loanValue;
            NumberOfMonthlyInstallments = numberOfMonthlyInstallments;
        }

        public void Add(Proponent proponent)
        {
            Proponents.Add(proponent);
        }

        public void Add(Warranty warranty)
        {
            Warranties.Add(warranty);
        }

        public void RemoveProponentBy(Guid id)
        {
            Proponents.RemoveAll(proponent => proponent.Id == id);
        }

        public void RemoveWarrantyBy(Guid id)
        {
            Warranties.RemoveAll(warranty => warranty.Id == id);
        }
    }
}
