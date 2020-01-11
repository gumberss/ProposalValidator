using ProposalValidator.Domain.Interfaces;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Events.Filters;
using ProposalValidator.Domain.Proposals.Validators;
using ProposalValidator.Domain.Proposals.Validators.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Handler.Handlers
{
    public class HandleEvents
    {
        public void Handle(String[] stringEvents)
        {
            List<Proposal> proposals = new List<Proposal>();

            var events = stringEvents
                .Select(stringEvent => Event.Create(stringEvent));

            IEventFilter filter = new EventFilterAnd(
                new EventFilterById(),
                new EventFilterByDate()
            );

            var filteredEvents = filter
                .Filter(events)
                .ToList();

            filteredEvents.ForEach(evt => evt.Change(ref proposals));

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
        }

    }
}
