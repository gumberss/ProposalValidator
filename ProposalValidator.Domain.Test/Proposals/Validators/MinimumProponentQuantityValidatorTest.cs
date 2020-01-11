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
    public class MinimumProponentQuantityValidatorTest
    {
        private readonly MinimumProponentQuantityValidator _minimumProponentQuantityValidator;

        public MinimumProponentQuantityValidatorTest()
        {
            _minimumProponentQuantityValidator = new MinimumProponentQuantityValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(1, false, "Quantidade de proponentes é inferior a quantidade mínima permitida")]
        [DataRow(2, true, "Quantidade de proponentes é igual a quantidade mínima permitida")]
        [DataRow(3, true, "Quantidade de proponentes é superior a quantidade minima permitida")]
        public void Deveria_validar_corretamente_a_quantidade_de_proponente_por_proposta(int proponentQuantity, bool expected, String because)
        {
            var proposal = new Proposal(Guid.NewGuid(), 0, 0);

            proposal.Proponents =
                Enumerable.Range(0, proponentQuantity)
                .Select(x => new Proponent())
                .ToList();

            var isValid = _minimumProponentQuantityValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }

    }
}
