using ConsoleApp4.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class LoanMonthlyInStallmentsValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            var twoYears = 12 * 2;
            var fifteenYears = 12 * 15;

            return proposal.NumberOfMonthlyInStallments >= twoYears && proposal.NumberOfMonthlyInStallments <= fifteenYears;
        }
    }
}
