using ProposalValidator.Domain.Models;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class ProponentAddedEvent : Event
    {
        public ProponentAddedEvent(string[] data) : base(data)
        {
        }

        public override void Change(ref List<Proposal> proposals)
        {
            var newProponent = new Proponent();
        }
    }
}