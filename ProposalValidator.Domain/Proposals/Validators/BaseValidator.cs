using ProposalValidator.Domain.Models;

namespace ProposalValidator.Domain.Proposals.Validators
{
    public abstract class BaseValidator 
    {
        private BaseValidator _next; 

        public bool Validate(Proposal proposal)
        {
            var isValid = ValitationWrapper(proposal);

            if (isValid && _next != null)
            {
                return _next.Validate(proposal);
            }

            return isValid;
        }

        public BaseValidator SetNext(BaseValidator validator) => _next = validator;
        
        protected abstract bool ValitationWrapper(Proposal proposal);

    }
}
