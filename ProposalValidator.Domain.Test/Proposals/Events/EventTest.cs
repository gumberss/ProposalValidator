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
      
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_criacao_de_proposta()
        {
            var (schema, action) = ("proposal", "created");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},3000,36";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProposalCreatedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_atualizacao_de_proposta()
        {
            var (schema, action) = ("proposal", "updated");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},3000,36";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProposalUpdatedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_exclusao_de_proposta()
        {
            var (schema, action) = ("proposal", "deleted");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProposalDeletedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_adicao_de_garantia()
        {
            var (schema, action) = ("warranty", "added");
            var (warrantyId, warrantyValue, warrantyProvince) = (Guid.NewGuid(), 1000.21, "SC");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},{warrantyId},{warrantyValue},{warrantyProvince}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<WarrantyAddedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_atualizacao_de_garantia()
        {
            var (schema, action) = ("warranty", "updated");
            var (warrantyId, warrantyValue, warrantyProvince) = (Guid.NewGuid(), 1000.21, "SC");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},{warrantyId},{warrantyValue},{warrantyProvince}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<WarrantyUpdatedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_remocao_de_garantia()
        {
            var (schema, action) = ("warranty", "removed");
            var warrantyId = Guid.NewGuid();

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},{warrantyId}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<WarrantyRemovedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_adicao_de_proponente()
        {
            var (schema, action) = ("proponent", "added");
            var (proponentId, proponentName, proponentAge) = (Guid.NewGuid(), 1000.21, 30);
            var (proponentMonthlyIncome, proponentIsMain) = (2000, true);

            var stringEvent = $"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()}," +
                $"{proponentId},{proponentName},{proponentAge},{proponentMonthlyIncome},{proponentIsMain}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProponentAddedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_atualizacao_de_proponente()
        {
            var (schema, action) = ("proponent", "updated");
            var (warrantyId, warrantyValue, warrantyProvince) = (Guid.NewGuid(), 1000.21, "SC");

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},{warrantyId},{warrantyValue},{warrantyProvince}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProponentUpdatedEvent>();
        }

        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_criar_corretamente_o_evento_de_remocao_de_proponente()
        {
            var (schema, action) = ("proponent", "removed");
            var warrantyId = Guid.NewGuid();

            var stringEvent = $@"{Guid.NewGuid()},{schema},{action},{DateTime.Now},{Guid.NewGuid()},{warrantyId}";

            var @event = Event.Create(stringEvent);

            @event.Should().BeOfType<ProponentRemovedEvent>();
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
