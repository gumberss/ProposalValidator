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
    public class ProponentAddedEventTest
    {
        [TestMethod]
        [TestCategory(TestCategories.EVENT)]
        public void Deveria_adicionar_o_proponente_recebido_a_proposta()
        {
            var proposalId = Guid.NewGuid();
            var proponentId = Guid.NewGuid();
            var proponentName = "Batman";
            var proponentAge = 30;
            var proponentMonthlyIncome = 3_000M;
            var proponentIsMain = true;

            List<Proposal> proposals = new List<Proposal>()
            {
                new Proposal(proposalId, 0, 0)
            };

            String stringEvent = $"{Guid.NewGuid()},proposal,created,{DateTime.Now},{proposalId}," +
                $"{proponentId},{proponentName},{proponentAge},{proponentMonthlyIncome},{proponentIsMain}";

            var @event = new ProponentAddedEvent(stringEvent.Split(','));

            @event.Change(ref proposals);

            var proposal = proposals.First();

            proposal.Proponents.Should().HaveCount(1, because: "Deve adicionar um proponente na proposta");
            
            var proponent = proposal.Proponents.First();
            proponent.Id.Should().Be(proponentId);
            proponent.Name.Should().Be(proponentName);
            proponent.Age.Should().Be(proponentAge);
            proponent.MonthlyIncome.Should().Be(proponentMonthlyIncome);
            proponent.IsMain.Should().Be(proponentIsMain);
        }
    }
}
