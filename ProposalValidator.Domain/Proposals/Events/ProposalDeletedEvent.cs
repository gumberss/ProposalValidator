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
            var removedProposal = proposals.Find(proposal => proposal.Id == _proposalId);

            proposals.Remove(removedProposal);

            //Or: proposals = proposals.Where(x => x.Id != _proposalId).ToList(); //If you want
        }
    }
}