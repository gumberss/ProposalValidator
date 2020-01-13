using System.Collections.Generic;
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class ProposalUpdatedEvent : Event
    {
        private const int PROPOSAL_LOAN_VALUE = 5;
        private const int PROPOSAL_NUMBER_OF_MONTHLY_INSTALLMENTS = 6;

        public ProposalUpdatedEvent(string[] data) : base(data)
        {
            _proposalLoanValue = decimal.Parse(data[PROPOSAL_LOAN_VALUE], _cultureInfo);
            _proposalNumberOfMonthlyInstallments = int.Parse(data[PROPOSAL_NUMBER_OF_MONTHLY_INSTALLMENTS]);
        }

        private readonly decimal _proposalLoanValue;
        private readonly int _proposalNumberOfMonthlyInstallments;

        public override void Change(ref List<Proposal> proposals)
        {
            proposals
                .Find(proposal => proposal.Id == _proposalId)
                .Update(_proposalLoanValue, _proposalNumberOfMonthlyInstallments);
        }
    }
}