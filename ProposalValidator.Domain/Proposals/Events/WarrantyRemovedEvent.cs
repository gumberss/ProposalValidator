using System.Collections.Generic;
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Validators.Events
{
    internal class WarrantyRemovedEvent : Event
    {
        public WarrantyRemovedEvent(string[] data) : base(data)
        {
        }

        public override void Change(ref List<Proposal> proposals)
        {
            throw new System.NotImplementedException();
        }
    }
}