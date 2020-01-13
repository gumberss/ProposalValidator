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
    public class ProposalDeletedEventTest
    {
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_remover_uma_proposta_quando_recepcionado_evento_de_exclusao_de_proposta()
        {
            var proposalToRemoveId = Guid.NewGuid();

            var proposals = new List<Proposal>
            {
                new Proposal(Guid.NewGuid(), 1000, 5),
                new Proposal(proposalToRemoveId, 1000, 5),
                new Proposal(Guid.NewGuid(), 1000, 5),
            };

            var (schema, action) = ("proposal", "deleted");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{proposalToRemoveId}";

            var @event = new ProposalDeletedEvent(stringEvent.Split(','));

            @event.Change(ref proposals);

            proposals.Should().HaveCount(2);

            proposals.Find(proposal => proposal.Id == proposalToRemoveId).Should().BeNull();

        }
    }
}
