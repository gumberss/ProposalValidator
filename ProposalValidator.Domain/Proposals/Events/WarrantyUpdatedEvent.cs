using System;
using System.Collections.Generic;
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class WarrantyUpdatedEvent : Event
    {
        private const int WARRANTY_ID = 5;
        private const int WARRANTY_VALUE = 6;
        private const int WARRANTY_PROVINCE = 7;

        public WarrantyUpdatedEvent(string[] data) : base(data)
        {
            _warrantyId = Guid.Parse(data[WARRANTY_ID]);
            _warrantyValue = Convert.ToDecimal(data[WARRANTY_VALUE]);
            _warrantyProvince = data[WARRANTY_PROVINCE];
        }

        private readonly Guid _warrantyId;
        private readonly decimal _warrantyValue;
        private readonly String _warrantyProvince;

        public override void Change(ref List<Proposal> proposals)
        {
            proposals
                .Find(proposal => proposal.Id == _proposalId)
                .FindWarranty(warranty => warranty.Id == _warrantyId)
                .Update(_warrantyValue, _warrantyProvince);
        }
    }
}