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
    public class WarrantyStateValidatorTest
    {
        private readonly WarrantyStateValidator _warrantyStateValidator;

        public WarrantyStateValidatorTest()
        {
            _warrantyStateValidator = new WarrantyStateValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow("SC", false, "Garantias do estado de SC não são permitidas")]
        [DataRow("PR", false, "Garantias do estado de PR não são permitidas")]
        [DataRow("RS", false, "Garantias do estado de RS não são permitidas")]
        [DataRow("SP", true, "Garantias que não são dos estados de SC, PR e RS são permitidas")]
        public void Deveria_validar_corretamente_o_estado_das_garantias_de_imoveis(String warrantyProvince, bool expected, String because)
        {
            var proposal = 
                new Proposal(Guid.NewGuid(), 0, 0)
                .Add(new Warranty { Province = warrantyProvince });

            var isValid = _warrantyStateValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }

        [TestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        public void Deveria_invalidar_proposta_quando_qualquer_garantia_for_de_um_dos_estados_nao_permitidos()
        {
            var proposal = 
                new Proposal(Guid.NewGuid(), 0, 0)
                .Add(new Warranty { Province = "SP" })
                .Add(new Warranty { Province = "RJ" })
                .Add(new Warranty { Province = "SC" });
            
            var isValid = _warrantyStateValidator.Validate(proposal);

            isValid.Should().BeFalse(because: "Uma das garantias é de SC que é um estado não permitido");
        }
    }
}
