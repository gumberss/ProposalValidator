using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class ProposalCreatedEvent : Event
    {
        private const int PROPOSAL_LOAN_VALUE = 5;
        private const int PROPOSAL_NUMBER_OF_MONTHLY_INSTALLMENTS = 6;

        public ProposalCreatedEvent(String[] data) : base(data)
        {
            _proposalLoanValue = decimal.Parse(data[PROPOSAL_LOAN_VALUE], _cultureInfo);
            _proposalNumberOfMonthlyInstallments = int.Parse(data[PROPOSAL_NUMBER_OF_MONTHLY_INSTALLMENTS]);
        }

        private decimal _proposalLoanValue { get; set; }
        private int _proposalNumberOfMonthlyInstallments { get; set; }

        public override void Change(ref List<Proposal> proposals)
        {
            Proposal newProposal = new Proposal(_proposalId, _proposalLoanValue, _proposalNumberOfMonthlyInstallments);

            proposals.Add(newProposal);
        }
    }
}
