using System.Collections.Generic;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators.Events;

namespace ProposalValidator.Domain.Proposals.Events
{
    internal class ProponentUpdatedEvent : Event
    {
        public ProponentUpdatedEvent(string[] data) : base(data)
        {
        }

        public override void Change(ref List<Proposal> proposals)
        {
            throw new System.NotImplementedException();
        }
    }
}