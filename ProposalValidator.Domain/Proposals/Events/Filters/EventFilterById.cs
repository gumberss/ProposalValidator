using ProposalValidator.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Domain.Proposals.Events.Filters
{
    public class EventFilterById : IEventFilter
    {
        public IEnumerable<Event> Filter(IEnumerable<Event> events)
            => events
                .GroupBy(x => x.Id)
                .Select(x => x.First());
    }
}
