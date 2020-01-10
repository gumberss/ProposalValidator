using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators;
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
        [DataRow(30_000.00, false)]
        [DataRow(20_000.00, false)]
        [DataRow(3_000_000.00, false)]
        [DataRow(4_000_000.00, false)]
        [DataRow(30_000.01, true)]
        [DataRow(2_999_999.99, true)]
        [DataRow(2_000_000, true)]
        public void Deveria_validar_corretamente_os_limites_dos_valores_aceitos_em_uma_proposta(double value, bool expected)
        {
            var proposalValue = Convert.ToDecimal(value);

            var proposal = new Proposal(Guid.NewGuid(), proposalValue, 1);

            var isValid = _loanValueValidator.Validate(proposal);

            isValid.Should().Be(expected);
        }
    }
}
