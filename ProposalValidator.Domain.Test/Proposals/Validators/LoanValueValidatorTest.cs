using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators;
using ProposalValidator.Domain.Test.Catagories;
using System;

namespace ProposalValidator.Domain.Test.Proposals.Validators
{
    [TestClass]
    public class LoanValueValidatorTest
    {
        private readonly LoanValueValidator _loanValueValidator;

        public LoanValueValidatorTest()
        {
            _loanValueValidator = new LoanValueValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(30_000.00, false, "O valor da proposta deve ser no mínimo maior que R$ 30.000,00")]
        [DataRow(3_000_000.00, false, "O valor da proposta deve ser menor que R$ 3.000.000,00")]
        [DataRow(30_000.01, true, "O valor da proposta é superior ao valor mínimo permitido")]
        [DataRow(2_999_999.99, true, "O valor da proposta é inferior ao valor máximo permitido")]
        public void Deveria_validar_corretamente_os_limites_dos_valores_aceitos_em_uma_proposta(double value, bool expected, string because)
        {
            var proposalValue = Convert.ToDecimal(value);

            var proposal = new Proposal(Guid.NewGuid(), proposalValue, 1);

            var isValid = _loanValueValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }
    }
}
