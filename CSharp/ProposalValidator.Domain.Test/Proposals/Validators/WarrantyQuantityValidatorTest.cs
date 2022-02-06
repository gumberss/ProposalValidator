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
    public class WarrantyQuantityValidatorTest
    {
        private readonly WarrantyQuantityValidator _warrantyQuantityValidator;

        public WarrantyQuantityValidatorTest()
        {
            _warrantyQuantityValidator = new WarrantyQuantityValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(0, false, "O imóvel não possui garantia")]
        [DataRow(1, true, "O imóvel possui exatamente uma garantia")]
        [DataRow(2, true, "O imóvel possui mais de uma garantia")]
        public void Deveria_validar_corretamente_a_quantidade_de_garantias_de_imovel_por_proposta(int warrantyQuantity, bool expected, String because)
        {
            var warranties = 
                Enumerable.Range(0, warrantyQuantity)
                .Select(_ => new Warranty())
                .ToList();

            var proposal = new Proposal(Guid.NewGuid(), 0, 0, warranties: warranties);

            var isValid = _warrantyQuantityValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }
    }
}
