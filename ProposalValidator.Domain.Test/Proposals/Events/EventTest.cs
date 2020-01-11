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
        [DataRow("warranty", "added", typeof(WarrantyAddedEvent))]
        [DataRow("warranty", "updated", typeof(WarrantyUpdatedEvent))]
        [DataRow("warranty", "removed", typeof(WarrantyRemovedEvent))]

        [DataRow("proponent", "added", typeof(ProponentAddedEvent))]
        [DataRow("proponent", "updated", typeof(ProponentUpdatedEvent))]
        [DataRow("proponent", "removed", typeof(ProponentRemovedEvent))]
        public void Deveria_criar_corretamente_os_eventos(String schema, String action, Type expected)
        {
            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},1,1,1,1,1,1,1,1,1,1,1,1,1,1";

            var @event = Event.Create(stringEvent);

            @event.Should().BeAssignableTo(expected);
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_os_evento_de_criacao_de_proposta()
        {
            var (schema, action) = ("proposal", "created");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},3000,36";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProposalCreatedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_os_evento_de_atualizacao_de_proposta()
        {
            var (schema, action) = ("proposal", "updated");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},3000,36";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProposalUpdatedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_os_evento_de_exclusao_de_proposta()
        {
            var (schema, action) = ("proposal", "deleted");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProposalDeletedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_lancar_excecao_de_negocio_quando_nao_for_encontrado_o_evento_para_o_schema_e_action_fornecidos()
        {
            var stringEvent = $@"{Guid.NewGuid()},batman,robin,{DateTime.Now},{Guid.NewGuid()}";

            Action action = () => Event.Create(stringEvent);

            action.Should().ThrowExactly<BusinessException>()
                .And
                .Message.Should().Be("There is no event for schema: batman action: robin registered");
        }
    }
}
