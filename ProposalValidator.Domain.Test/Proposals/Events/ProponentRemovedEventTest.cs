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
    public class ProponentRemovedEventTest
    {
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_remover_o_proponente_da_proposta()
        {
            var proposalId = Guid.NewGuid();
            var proponentId = Guid.NewGuid();

            List<Proposal> proposals = new List<Proposal>()
            {
                new Proposal(proposalId, 0, 0)
                .Add(new Proponent(proponentId, "old", 0, 0, false))
            };

            String stringEvent = $"{Guid.NewGuid()},proposal,created,{DateTime.Now},{proposalId},{proponentId}";

            var @event = new ProponentRemovedEvent(stringEvent.Split(','));

            @event.Change(ref proposals);

            var proposal = proposals.First();

            proposal.Proponents.Should().HaveCount(0, because: "O Proponente foi removido");
        }
    }
}
