using ProposalValidator.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProposalValidator.Domain.Proposals.Events.Filters
{
    public class EventFilterByDate : IEventFilter
    {
        public IEnumerable<Event> Filter(IEnumerable<Event> events) 
            => events.GroupBy(x => new { x.Schema, x.Action })
                .SelectMany(x => x.Key.Action switch
                {
                    "updated" => new[] { x.OrderByDescending(y => y.Timestamp).First() },
                    _ => x.ToArray()
                });
    }
}
