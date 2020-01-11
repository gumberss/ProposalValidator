using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class ProponentRemovedEvent : Event
    {
        public ProponentRemovedEvent(string[] data) : base(data)
        {
        }

        public override void Change(ref List<Proposal> proposals)
        {
            throw new NotImplementedException();
        }
    }
}
