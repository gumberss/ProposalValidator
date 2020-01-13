using System;
using System.Collections.Generic;
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class WarrantyRemovedEvent : Event
    {
        private const int WARRANTY_ID = 5;

        public WarrantyRemovedEvent(string[] data) : base(data)
        {
            _warrantyId = Guid.Parse(data[WARRANTY_ID]);
        }
        private readonly Guid _warrantyId;

        public override void Change(ref List<Proposal> proposals)
        {
            proposals
                .Find(proposal => proposal.Id == _proposalId)
                .RemoveWarrantyBy(_warrantyId);
        }
    }
}