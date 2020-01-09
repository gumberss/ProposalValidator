using ProposalValidator.Domain.Constants;
using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProposalValidator.Domain.Proposals.Validators.Events
{
    public abstract class Event
    {
        private static readonly int ID_POSITION = 0;
        private static readonly int SCHEMA_POSITION = 1;
        private static readonly int ACTION_POSITION = 2;
        private static readonly int TIMESTAMP_POSITION = 3;
        private static readonly int PROPOSAL_ID_POSITION = 4;

        public static Event Create(String stringEvent)
        {
            var eventData = stringEvent.Split(',');

            var (schema, action) = (eventData[SCHEMA_POSITION], eventData[ACTION_POSITION]);

            return (schema, action) switch
            {
                (SchemaConst.PROPOSAL, ActionsConst.CREATED) => new ProponentRemovedEvent(eventData),
                (SchemaConst.PROPOSAL, ActionsConst.UPDATED) => new ProposalUpdatedEvent(eventData),
                (SchemaConst.PROPOSAL, ActionsConst.DELETED) => new ProposalDeletedEvent(eventData),
                (SchemaConst.WARRANTY, ActionsConst.ADDED) => new WarrantyAddedEvent(eventData),
                (SchemaConst.WARRANTY, ActionsConst.UPDATED) => new WarrantyUpdatedEvent(eventData),
                (SchemaConst.WARRANTY, ActionsConst.REMOVED) => new WarrantyRemovedEvent(eventData),
                (SchemaConst.PROPONENT, ActionsConst.ADDED) => new ProponentAddedEvent(eventData),
                (SchemaConst.PROPONENT, ActionsConst.UPDATED) => new ProponentUpdatedEvent(eventData),
                (SchemaConst.PROPONENT, ActionsConst.REMOVED) => new ProponentRemovedEvent(eventData),
                _ => throw new ArgumentException(message: $"There is no event {schema} {action} registered", paramNa),
            };
        }

        protected Event(String[] data)
        {
            Id = Guid.Parse(data[ID_POSITION]);

            Schema = data[SCHEMA_POSITION];
            Action = data[ACTION_POSITION];
            Timestamp = DateTime.Parse(data[TIMESTAMP_POSITION]);
            ProposalId = Guid.Parse(data[PROPOSAL_ID_POSITION]);
        }

        protected static CultureInfo _cultureInfo = new CultureInfo("EN-us");

        public Guid Id { get; private set; }

        public String Schema { get; private set; }

        public String Action { get; private set; }

        public DateTime Timestamp { get; private set; }

        public Guid ProposalId { get; private set; }

        public abstract void Change(ref List<Proposal> proposals);
    }
}
