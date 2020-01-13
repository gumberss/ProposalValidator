using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Events;
using ProposalValidator.Domain.Test.Catagories;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Test.Proposals.Events
{
    [TestClass]
    public class ProposalUpdatedEventTest
    {
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_atualizar_a_proposta_corretamente_com_base_no_evento_recebido()
        {
            var proposalToUpdateId = Guid.NewGuid();
            var proposalToUpdateLoanValue = 3000;
            var proposalToUpdateNumberOfMonthlyInstallments = 36;

            var (schema, action) = ("proposal", "updated");

            String stringEvent = $"{Guid.NewGuid()},{schema},{action},{DateTime.Now}," +
                $"{proposalToUpdateId},{proposalToUpdateLoanValue},{proposalToUpdateNumberOfMonthlyInstallments}";

            var @event = new ProposalUpdatedEvent(stringEvent.Split(','));

            var proposals = new List<Proposal>
            {
                new Proposal(Guid.NewGuid(), 1000, 5),
                new Proposal(proposalToUpdateId, 1000, 5),
                new Proposal(Guid.NewGuid(), 1000, 5),
            };

            @event.Change(ref proposals);

            proposals.Should().HaveCount(3);

            var proposal = proposals.Find(proposal => proposal.Id == proposalToUpdateId);
            proposal.LoanValue.Should().Be(proposalToUpdateLoanValue);
            proposal.NumberOfMonthlyInstallments.Should().Be(proposalToUpdateNumberOfMonthlyInstallments);
        }
    }
}
