using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        [DataRow(17, false, "Proponente é menor de idade")]
        [DataRow(18, true, "Proponente é maior de idade")]
        [DataRow(19, true, "Proponente é maior de idade")]
        public void Deveria_validar_corretamente_a_idade_dos_proponentes(int proponentAge, bool expected, String because)
        {
            var proposal = new Proposal(Guid.NewGuid(), 0, 0);

            proposal.Proponents = new List<Proponent> { new Proponent() { Age = proponentAge } };

            var isValid = proponentAgeValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }
    }
}
