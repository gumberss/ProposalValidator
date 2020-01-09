using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Proposals.Validators.Events
{
    public class ProponentRemovedEvent : Event
    {
        public ProponentRemovedEvent(String[] data) : base(data)
        {
            ProposalLoanValue = decimal.Parse(data[5], _cultureInfo);
            ProposalNumberOfMonthlyInstallments = int.Parse(data[6]);
        }

        public decimal ProposalLoanValue { get; set; }
        public int ProposalNumberOfMonthlyInstallments { get; set; }

        public override void Change(ref List<Proposal> proposals)
        {
            Proposal newProposal = new Proposal(ProposalId, ProposalLoanValue, ProposalNumberOfMonthlyInstallments);

            proposals.Add(newProposal);
        }
    }
}
