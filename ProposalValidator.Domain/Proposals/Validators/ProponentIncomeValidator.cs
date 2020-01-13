using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class ProponentIncomeValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            var mainProponent = proposal.MainProponent();

            var installmentQuantity = proposal.NumberOfMonthlyInstallments;

            var installmentValue = proposal.LoanValue / installmentQuantity;

            if (mainProponent == null) return false;

            if (mainProponent.Age < 24 && mainProponent.MonthlyIncome < installmentValue * 4)
                return false;

            if (mainProponent.Age < 50 && mainProponent.MonthlyIncome < installmentValue * 3)
                return false;

            if (mainProponent.Age >= 50 && mainProponent.MonthlyIncome < installmentValue * 2)
                return false;

            return true;
        }
    }
}
