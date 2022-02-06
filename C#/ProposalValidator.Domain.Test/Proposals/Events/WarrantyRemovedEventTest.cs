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
    public class WarrantyRemovedEventTest
    {
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_remover_o_proponente_da_proposta()
        {
            var proposalId = Guid.NewGuid();
            var warrantyId = Guid.NewGuid();

            List<Proposal> proposals = new List<Proposal>()
            {
                new Proposal(proposalId, 0, 0)
                .Add(new Warranty(warrantyId, 1_000, "SC"))
            };

            String stringEvent = $"{Guid.NewGuid()},warranty,created,{DateTime.Now},{proposalId},{warrantyId}";

            var @event = new WarrantyRemovedEvent(stringEvent.Split(','));

            @event.Change(ref proposals);

            var proposal = proposals.First();

            proposal.Warranties.Should().HaveCount(0, because: "A garantia foi removida");
        }
    }
}
