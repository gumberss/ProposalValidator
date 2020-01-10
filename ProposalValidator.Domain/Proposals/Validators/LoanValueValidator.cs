
using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class LoanValueValidator : BaseValidator
    {
        //todo: ]30.000, 3.000.000[ ou [30.000, 3.000.000] ?
        protected override bool ValitationWrapper(Proposal proposal)
        {
            return proposal.LoanValue > 30_000 && proposal.LoanValue < 3_000_000;
        }
    }
}
