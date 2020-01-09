using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Validators.Events
{
    public class WarrantyAddedEvent : Event
    {
        public WarrantyAddedEvent(string[] data) : base(data)
        {
        }

        public override void Change(System.Collections.Generic.List<Proposal> proposals)
        {
            throw new System.NotImplementedException();
        }
    }
}