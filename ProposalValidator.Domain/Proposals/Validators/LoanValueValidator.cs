using ConsoleApp4.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class LoanValueValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            return proposal.LoanValue >= 30_000 && proposal.LoanValue <= 3_000_000;
        }
    }
}
