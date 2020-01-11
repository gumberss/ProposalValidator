using ProposalValidator.Domain.Proposals.Validators.Events;
using System.Collections.Generic;

namespace ProposalValidator.Domain.Interfaces
{
    public interface IEventFilter
    {
        IEnumerable<Event> Filter(IEnumerable<Event> events);
    }
}
