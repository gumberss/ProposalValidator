using System;

namespace ProposalValidator.Domain.Models
{
    public class Warranty
    {
        public Warranty() { }

        public Warranty(Guid id, decimal value, string province)
        {
            Id = id;
            Value = value;
            Province = province;
        }

        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public String Province { get; set; }
    }
}
