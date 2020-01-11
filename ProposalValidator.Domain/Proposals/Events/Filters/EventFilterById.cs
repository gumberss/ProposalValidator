using ProposalValidator.Domain.Interfaces;
using ProposalValidator.Domain.Proposals.Validators.Events;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Domain.Proposals.Events.Filters
{
    public class EventFilterById : IEventFilter
    {
        public IEnumerable<Event> Filter(IEnumerable<Event> events)
        {
            return events
                .GroupBy(x => x.Id)
                .Select(x => x.First());
        }
    }
}
