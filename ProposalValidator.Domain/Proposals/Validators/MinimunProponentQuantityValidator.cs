using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp4.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public class MinimunProponentQuantityValidator : BaseValidator
    {
        protected override bool ValitationWrapper(Proposal proposal)
        {
            return proposal.Proponents.Count >= 2;
        }
    }
}
