using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Exceptions;
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

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_lancar_excecao_de_negocio_quando_nao_for_encontrado_o_evento_para_o_schema_e_action_fornecidos()
        {
            var stringEvent = $@"{Guid.NewGuid()},batman,robin,{DateTime.Now},{Guid.NewGuid()},1,1,1,1,1,1,1,1,1,1,1,1,1,1";

            Action action = () => Event.Create(stringEvent);

            action.Should().ThrowExactly<BusinessException>()
                .And
                .Message.Should().Be("There is no event for schema: batman action: robin registered");
        }
    }
}
