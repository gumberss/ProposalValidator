using System.Linq;
using ConsoleApp4.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class ProponentAgeValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            return proposal.Proponents.All(x => x.Age > 18);
        }
    }
}
