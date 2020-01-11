using ProposalValidator.Domain.Models;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class WarrantyAddedEvent : Event
    {
        public WarrantyAddedEvent(string[] data) : base(data)
        {
        }

        public override void Change(ref List<Proposal> proposals)
        {
            throw new System.NotImplementedException();
        }
    }
}