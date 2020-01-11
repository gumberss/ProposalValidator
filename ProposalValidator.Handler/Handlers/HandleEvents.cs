using ProposalValidator.Domain.Models;
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

            var events = stringEvents.Select(stringEvent => Event.Create(stringEvent));

            var distictEvents = events
                .GroupBy(x => x.Id)
                .Select(x => x.First())
                .GroupBy(x => new { x.Schema, x.Action })
                .SelectMany(x => x.Key.Action == "updated" ? new[] { x.OrderBy(y => y.Timestamp).First() } : x.ToArray())
                .ToList();

            distictEvents.ForEach(evt => evt.Change(ref proposals));

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
