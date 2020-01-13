using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class ProponentRemovedEvent : Event
    {
        private const int PROPONENT_ID = 5;

        public ProponentRemovedEvent(string[] data) : base(data)
        {
            _proponentId = Guid.Parse(data[PROPONENT_ID]);
        }
        private readonly Guid _proponentId;

        public override void Change(ref List<Proposal> proposals)
        {
            proposals
                .Find(proposal => proposal.Id == _proposalId)
                .RemoveProponentBy(_proponentId);
        }
    }
}
