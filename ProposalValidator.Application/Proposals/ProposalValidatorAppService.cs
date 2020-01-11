using ProposalValidator.Application.Interfaces;
using ProposalValidator.Domain.Interfaces;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Events.Filters;
using ProposalValidator.Domain.Proposals.Validators;
using ProposalValidator.Domain.Proposals.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Application.Proposals
{
    public class ProposalValidatorAppService : IProposalValidatorAppService
    {
        public IEnumerable<Proposal> Validate(String[] stringEvents)
        {
            var events = stringEvents
                .Select(stringEvent => Event.Create(stringEvent));

            IEventFilter filter = new EventFilterAnd(
                new EventFilterById(),
                new EventFilterByDate()
            );

            var filteredEvents = filter
                .Filter(events)
                .ToList();

            List<Proposal> proposals = new List<Proposal>();

            filteredEvents.ForEach(@event => @event.Change(ref proposals));

            BaseValidator validatorChain = new LoanValueValidator();

            validatorChain
                .SetNext(new LoanMonthlyInStallmentsValidator())
                .SetNext(new MinimumProponentQuantityValidator())
                .SetNext(new MainProponentQuantityValidator())
                .SetNext(new ProponentAgeValidator())
                .SetNext(new WarrantyQuantityValidator())
                .SetNext(new WarrantyValueValidator())
                .SetNext(new WarrantyStateValidator())
                .SetNext(new ProponentIncomeValidator());

            var validProposals = proposals.Where(proposal => validatorChain.Validate(proposal));

            return validProposals;
        }
    }
}
