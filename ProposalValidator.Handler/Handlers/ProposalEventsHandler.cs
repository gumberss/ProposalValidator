using ProposalValidator.Application.Interfaces;
using System;
using System.Linq;

namespace ProposalValidator.Handler.Handlers
{
    public class ProposalEventsHandler
    {
        private readonly IProposalValidatorAppService _proposalValidator;

        public ProposalEventsHandler(IProposalValidatorAppService proposalValidator)
        {
            _proposalValidator = proposalValidator;
        }

        public String Handle(String[] stringEvents)
        {
            var validProposals = _proposalValidator.Validate(stringEvents);

            return String.Join(',', validProposals.Select(x => x.Id));
        }
    }
}
