using System.Linq;
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class WarrantyStateValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            var unableStatesToWarranty = new[] { "SC", "PR", "RS" };

            return proposal.Warranties.All(warranty => !unableStatesToWarranty.Contains(warranty.Province));
        }
    }
}
