using System.Linq;
using ConsoleApp4.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class MainProponentQuantityValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            return proposal.Proponents.Count(x => x.IsMain) == 1;
        }
    }
}
