using System;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Domain.Models
{
    public class Proposal
    {
        public Proposal(Guid id, decimal loanValue, int numberOfMonthlyInStallments, List<Proponent> proponents = null, List<Warranty> warranties = null)
        {
            Id = id;
            LoanValue = loanValue;
            NumberOfMonthlyInstallments = numberOfMonthlyInStallments;

            Proponents = proponents ?? new List<Proponent>();
            Warranties = warranties ?? new List<Warranty>();
        }

        public Guid Id { get; private set; }

        public decimal LoanValue { get; private set; }

        public int NumberOfMonthlyInstallments { get; private set; }

        public List<Proponent> Proponents { get; private set; }

        public List<Warranty> Warranties { get; private set; }

        public Proponent MainProponent()
        {
            return Proponents.FirstOrDefault(x => x.IsMain);
        }

        public Proposal Update(decimal loanValue, int numberOfMonthlyInstallments)
        {
            LoanValue = loanValue;
            NumberOfMonthlyInstallments = numberOfMonthlyInstallments;

            return this;
        }

        public Proposal Add(Proponent proponent)
        {
            Proponents.Add(proponent);

            return this;
        }

        public Proposal Add(Warranty warranty)
        {
            Warranties.Add(warranty);

            return this;
        }

        public Proposal RemoveProponentBy(Guid id)
        {
            Proponents.RemoveAll(proponent => proponent.Id == id);

            return this;
        }

        public Proposal RemoveWarrantyBy(Guid id)
        {
            Warranties.RemoveAll(warranty => warranty.Id == id);

            return this;
        }

        public Proponent FindProponent(Predicate<Proponent> match)
        {
            return Proponents.Find(match);
        }

        public Warranty FindWarranty(Predicate<Warranty> match)
        {
            return Warranties.Find(match);
        }
    }
}
