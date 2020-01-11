using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators;
using ProposalValidator.Domain.Test.Catagories;
using System;
using System.Linq;

namespace ProposalValidator.Domain.Test.Proposals.Validators
{
    [TestClass]
    class MainProponentQuantityValidatorTest
    {
        private readonly MainProponentQuantityValidator _mainProponentQuantityValidator;

        public MainProponentQuantityValidatorTest()
        {
            _mainProponentQuantityValidator = new MainProponentQuantityValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(0, false, "Deve haver exatamente 1 proponente principal")]
        [DataRow(1, true, "Deve haver exatamente 1 proponente principal")]
        [DataRow(2, false, "Deve haver exatamente 1 proponente principal")]
        public void Deveria_validar_corretamente_a_quantidade_de_proponente_por_proposta(int mainProponentQuantity, bool expected, String because)
        {
            var proposal = new Proposal(Guid.NewGuid(), 0, 0);

            proposal.Proponents =
                Enumerable.Range(0, mainProponentQuantity)
                .Select(x => new Proponent() { IsMain = true })
                .ToList();

            var isValid = _mainProponentQuantityValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }
    }
}
