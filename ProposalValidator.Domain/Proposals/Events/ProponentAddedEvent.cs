using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Proposals.Events
{
    public class ProponentAddedEvent : Event
    {
        private const int PROPONENT_ID = 5;
        private const int PROPONENT_NAME = 6;
        private const int PROPONENT_AGE = 7;
        private const int PROPONENT_MONTHLY_INCOME = 8;
        private const int PROPONENT_IS_MAIN = 9;

        private readonly Guid _proponentId;
        private readonly string _proponentName;
        private readonly int _proponentAge;
        private readonly decimal _proponentMonthlyIncome;
        private readonly bool _proponentIsMain;

        public ProponentAddedEvent(string[] data) : base(data)
        {
            _proponentId = Guid.Parse(data[PROPONENT_ID]);
            _proponentName = data[PROPONENT_NAME];
            _proponentAge = int.Parse(data[PROPONENT_AGE]);
            _proponentMonthlyIncome = Convert.ToDecimal(data[PROPONENT_MONTHLY_INCOME]);
            _proponentIsMain = Convert.ToBoolean(data[PROPONENT_IS_MAIN]);
        }

        public override void Change(ref List<Proposal> proposals)
        {
            proposals
                .Find(proposal => proposal.Id == _proposalId)
                .Add(new Proponent(_proponentId, _proponentName, _proponentAge, _proponentMonthlyIncome, _proponentIsMain));
        }
    }
}