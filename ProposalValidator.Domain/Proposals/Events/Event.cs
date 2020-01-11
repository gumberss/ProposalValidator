using ProposalValidator.Domain.Constants;
using ProposalValidator.Domain.Exceptions;
using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProposalValidator.Domain.Proposals.Events
{
    public abstract class Event
    {
        private const int ID_POSITION = 0;
        private const int SCHEMA_POSITION = 1;
        private const int ACTION_POSITION = 2;
        private const int TIMESTAMP_POSITION = 3;
        private const int PROPOSAL_ID_POSITION = 4;

        public static Event Create(String stringEvent)
        {
            var eventData = stringEvent.Split(',');

            var (schema, action) = (eventData[SCHEMA_POSITION], eventData[ACTION_POSITION]);

            return (schema, action) switch
            {
                (SchemaConst.PROPOSAL, ActionsConst.CREATED) => new ProposalCreatedEvent(eventData),
                (SchemaConst.PROPOSAL, ActionsConst.UPDATED) => new ProposalUpdatedEvent(eventData),
                (SchemaConst.PROPOSAL, ActionsConst.DELETED) => new ProposalDeletedEvent(eventData),
                (SchemaConst.WARRANTY, ActionsConst.ADDED) => new WarrantyAddedEvent(eventData),
                (SchemaConst.WARRANTY, ActionsConst.UPDATED) => new WarrantyUpdatedEvent(eventData),
                (SchemaConst.WARRANTY, ActionsConst.REMOVED) => new WarrantyRemovedEvent(eventData),
                (SchemaConst.PROPONENT, ActionsConst.ADDED) => new ProponentAddedEvent(eventData),
                (SchemaConst.PROPONENT, ActionsConst.UPDATED) => new ProponentUpdatedEvent(eventData),
                (SchemaConst.PROPONENT, ActionsConst.REMOVED) => new ProponentRemovedEvent(eventData),
                _ => throw new BusinessException($"There is no event for schema: {schema} action: {action} registered"),
            };
        }

        protected Event(String[] data)
        {
            Id = Guid.Parse(data[ID_POSITION]);

            Schema = data[SCHEMA_POSITION];
            Action = data[ACTION_POSITION];
            Timestamp = DateTime.Parse(data[TIMESTAMP_POSITION]);
            _proposalId = Guid.Parse(data[PROPOSAL_ID_POSITION]);
        }

        protected static readonly CultureInfo _cultureInfo = new CultureInfo("EN-us");

        public Guid Id { get; set; }

        public String Schema { get; set; }

        public String Action { get; set; }

        public DateTime Timestamp { get; set; }

        protected readonly Guid _proposalId;

        public abstract void Change(ref List<Proposal> proposals);
    }
}
