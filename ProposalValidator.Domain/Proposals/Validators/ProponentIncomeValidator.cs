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

            return (mainProponent.Age, mainProponent.MonthlyIncome) switch
            {
                var (age, monthlyIncome) when age < 24 && monthlyIncome < installmentValue * 4 => false,
                var (age, monthlyIncome) when age < 50 && monthlyIncome < installmentValue * 3 => false,
                var (age, monthlyIncome) when age >= 50 && monthlyIncome < installmentValue * 2 => false,
                _ => true
            };
        }
    }
}
