using ProposalValidator.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProposalValidator.Application.Interfaces
{
    public interface IProposalValidatorAppService
    {
        IEnumerable<Proposal> Validate(String[] stringEvents);
    }
}
