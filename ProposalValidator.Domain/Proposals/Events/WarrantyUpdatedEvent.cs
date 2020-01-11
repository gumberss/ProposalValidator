﻿using System.Collections.Generic;
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Events
{
    internal class WarrantyUpdatedEvent : Event
    {
        public WarrantyUpdatedEvent(string[] data) : base(data)
        {
        }

        public override void Change(ref List<Proposal> proposals)
        {
            throw new System.NotImplementedException();
        }
    }
}