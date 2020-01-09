using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class ProponentIncomeValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            var mainProponent = proposal.MainProponent();

            if (mainProponent == null) return false;

            if (mainProponent.Age < 24 && mainProponent.MonthlyIncome * 4 < proposal.LoanValue)
                return false;

            if (mainProponent.Age < 50 && mainProponent.MonthlyIncome * 3 < proposal.LoanValue)
                return false;

            if (mainProponent.Age >= 50 && mainProponent.MonthlyIncome * 2 < proposal.LoanValue)
                return false;

            return true;
        }
    }
}
