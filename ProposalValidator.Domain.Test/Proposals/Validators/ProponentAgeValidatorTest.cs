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
    public class ProponentAgeValidatorTest
    {
        private readonly ProponentAgeValidator proponentAgeValidator;

        public ProponentAgeValidatorTest()
        {
            proponentAgeValidator = new ProponentAgeValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(17, false, "Proponente é menor de idade")]
        [DataRow(18, true, "Proponente é maior de idade")]
        [DataRow(19, true, "Proponente é maior de idade")]
        public void Deveria_validar_corretamente_a_idade_dos_proponentes(int proponentAge, bool expected, String because)
        {
            var proposal = new Proposal(Guid.NewGuid(), 0, 0)
            {
                Proponents = new List<Proponent> { new Proponent() { Age = proponentAge } }
            };

            var isValid = proponentAgeValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }

        [TestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        public void Deveria_invalidar_proposta_com_um_proponente_menor_de_idade()
        {
            var minor = new Proponent() { Age = 10 };

            var proposal = new Proposal(Guid.NewGuid(), 0, 0)
            {
                Proponents = new List<Proponent> {
                    new Proponent() { Age = 20 },
                    new Proponent() { Age = 30 },
                    minor
                }
            };

            var isValid = proponentAgeValidator.Validate(proposal);

            isValid.Should().BeFalse(because: "Um dos proponentes é menor de idade");
        }

        [TestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        public void Deveria_validar_proposta_com_todos_os_proponente_maiores_de_idade()
        {
            var proposal = new Proposal(Guid.NewGuid(), 0, 0)
            {
                Proponents = new List<Proponent> {
                    new Proponent() { Age = 20 },
                    new Proponent() { Age = 30 },
                    new Proponent() { Age = 50 },
                }
            };

            var isValid = proponentAgeValidator.Validate(proposal);

            isValid.Should().BeTrue(because: "todos os proponentes são maiores de idade");
        }
    }
}
