using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators;
using ProposalValidator.Domain.Test.Catagories;
using System;

namespace ProposalValidator.Domain.Test.Proposals.Validators
{
    [TestClass]
    public class LoanMonthlyInStallmentsValidatorTest
    {
        public LoanMonthlyInStallmentsValidator _loanMonthlyInStallmentsValidator { get; }

        public LoanMonthlyInStallmentsValidatorTest()
        {
            _loanMonthlyInStallmentsValidator = new LoanMonthlyInStallmentsValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(23, false, "A quantidade de messes para pagar o empréstimo é inferior ao mínimo aceito")]
        [DataRow(2 * 12, true, "O empréstimo deve ser pago no mínimo em 2 anos")]
        [DataRow(15 * 12, true, "O empréstimo pode ser pago em até 15 anos")]
        [DataRow(15 * 12 + 1, false, "O empréstimo possui limite máximo de 15 anos para ser pago")]
        public void Deveria_validar_corretamente_o_periodo_do_emprestimo(int months, bool expected, String because)
        {
            var proposal = new Proposal(Guid.NewGuid(), 0, months);

            var isValid = _loanMonthlyInStallmentsValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }
    }
}
