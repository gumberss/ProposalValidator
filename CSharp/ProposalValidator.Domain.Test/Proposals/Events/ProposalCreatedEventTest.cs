using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Events;
using ProposalValidator.Domain.Test.Catagories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Domain.Test.Proposals.Events
{
    [TestClass]
    public class ProposalCreatedEventTest
    {
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_crir_a_proposta_corretamente_com_base_no_evento_recebido()
        {
            var proposalId = Guid.NewGuid();
            var proposalLoanValue = 100;
            var proposalNumberOfMonthlyInstallments = 36;

            var (schema, action) = ("proposal", "created");

            String stringEvent = $"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{proposalId},{proposalLoanValue},{proposalNumberOfMonthlyInstallments}";

            var @event = new ProposalCreatedEvent(stringEvent.Split(','));

            List<Proposal> proposals = new List<Proposal>();

            @event.Change(ref proposals);

            proposals.Should().HaveCount(1);
            
            var proposal = proposals.First();
            proposal.Id.Should().Be(proposalId);
            proposal.LoanValue.Should().Be(proposalLoanValue);
            proposal.NumberOfMonthlyInstallments.Should().Be(proposalNumberOfMonthlyInstallments);
        }
    }
}
