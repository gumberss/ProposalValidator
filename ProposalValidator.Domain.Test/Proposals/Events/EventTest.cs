using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Proposals.Events;
using ProposalValidator.Domain.Test.Catagories;
using System;

namespace ProposalValidator.Domain.Test.Proposals.Events
{
    [TestClass]
    public class EventTest
    {
        [DataTestMethod]
        [TestCategory(TestCategories.EVENT)]
        [DataRow("proposal", "created", typeof(ProposalCreatedEvent))]
        [DataRow("proposal", "updated", typeof(ProposalUpdatedEvent))]
        [DataRow("proposal", "deleted", typeof(ProposalDeletedEvent))]

        [DataRow("warranty", "added", typeof(WarrantyAddedEvent))]
        [DataRow("warranty", "updated", typeof(WarrantyUpdatedEvent))]
        [DataRow("warranty", "removed", typeof(WarrantyRemovedEvent))]

        [DataRow("proponent", "added", typeof(ProponentAddedEvent))]
        [DataRow("proponent", "updated", typeof(ProponentUpdatedEvent))]
        [DataRow("proponent", "removed", typeof(ProponentRemovedEvent))]
        public void Deveria_validar_corretamente_a_quantidade_de_proponente_por_proposta(String schema, String action, Type expected)
        {
            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},1,1,1,1,1,1,1,1,1,1,1,1,1,1";

            var @event = Event.Create(stringEvent);

            @event.Should().BeAssignableTo(expected);
        }
    }
}
