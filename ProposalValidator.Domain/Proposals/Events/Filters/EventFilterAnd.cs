using ProposalValidator.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Domain.Proposals.Events.Filters
{
    public class EventFilterAnd : IEventFilter
    {
        private readonly IEnumerable<IEventFilter> _filters;

        public EventFilterAnd(params IEventFilter[] filters)
        {
            _filters = filters;
        }

        public IEnumerable<Event> Filter(IEnumerable<Event> events) 
            => _filters.Aggregate(events, (aggregate, filter) => filter.Filter(events));
    }
}
