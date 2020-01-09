using System;

namespace ProposalValidator.Domain.Models
{
    public class Warranty
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public String Province { get; set; }
    }
}
