using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Events;
using ProposalValidator.Domain.Test.Catagories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProposalValidator.Domain.Test.Proposals.Events
{
    [TestClass]
    public class WarrantyUpdatedEventTest
    {
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_atualizar_o_garantia_recebido_na_proposta()
        {
            var proposalId = Guid.NewGuid();
            var warrantyId = Guid.NewGuid();
            var warrantyValue = 3_000M;
            var warrantyProvince = "SC";

            List<Proposal> proposals = new List<Proposal>()
            {
                new Proposal(proposalId, 0, 0)
                .Add(new Warranty(warrantyId, 0, "Batman"))
            };

            String stringEvent = $"{Guid.NewGuid()},proposal,updated,{DateTime.Now},{proposalId}," +
                $"{warrantyId},{warrantyValue},{warrantyProvince}";

            var @event = new WarrantyUpdatedEvent(stringEvent.Split(','));

            @event.Change(ref proposals);

            var proposal = proposals.First();

            proposal.Warranties.Should().HaveCount(1);

            var warranty = proposal.Warranties.First();
            warranty.Id.Should().Be(warrantyId);
            warranty.Value.Should().Be(warrantyValue);
            warranty.Province.Should().Be(warrantyProvince);
        }
    }
}
