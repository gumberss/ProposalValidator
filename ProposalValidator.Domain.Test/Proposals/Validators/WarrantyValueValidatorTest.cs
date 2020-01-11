using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators;
using ProposalValidator.Domain.Test.Catagories;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Test.Proposals.Validators
{
    [TestClass]
    public class WarrantyValueValidatorTest
    {
        private readonly WarrantyValueValidator _warrantyValueValidator;

        public WarrantyValueValidatorTest()
        {
            _warrantyValueValidator = new WarrantyValueValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(10, 19.99, false, "O valor da garantia é inferior ao dobro do valor do empréstimo")]
        [DataRow(10, 20, true, "O valor da garantia é igual ao dobro do valor do empréstimo")]
        [DataRow(10, 30, true, "O valor da garantia é maior que o dobro do valor do empréstimo")]
        public void Deveria_validar_corretamente_o_valor_da_proposta_com_base_na_garantia(double doubleLoanValue, double doubleWarrantyValue, bool expected, String because)
        {
            var loanValue = Convert.ToDecimal(doubleLoanValue);
            var warrantyValue = Convert.ToDecimal(doubleWarrantyValue);

            var proposal = new Proposal(Guid.NewGuid(), loanValue, 0)
            {
                Warranties = new List<Warranty>
                {
                    new Warranty() { Value = warrantyValue }
                }
            };

            var isValid = _warrantyValueValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }

        [TestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        public void Deveria_validar_corretamente_o_valor_da_proposta_quando_a_soma_dos_valores_das_garantias_forem_o_dobro_do_valor_do_emprestimo()
        {
            var loanValue = 20;
            var (firstWarrantyValue, secondWarrantyValue) = (30, 10);

            var proposal = new Proposal(Guid.NewGuid(), loanValue, 0)
            {
                Warranties = new List<Warranty>
                {
                    new Warranty() { Value = firstWarrantyValue },
                    new Warranty() { Value = secondWarrantyValue },
                }
            };

            var isValid = _warrantyValueValidator.Validate(proposal);

            isValid.Should().BeTrue(because: "O valor da soma da das garantias é o dobro do valor do emprestimo");
        }
    }
}
