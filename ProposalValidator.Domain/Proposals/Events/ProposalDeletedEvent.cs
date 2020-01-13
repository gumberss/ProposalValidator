using System.Collections.Generic;
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class ProposalDeletedEvent : Event
    {
        public ProposalDeletedEvent(string[] data) : base(data)
        {
        }

        public override void Change(ref List<Proposal> proposals)
        {
            proposals.RemoveAll(proposal => proposal.Id == _proposalId);
        }
    }
}