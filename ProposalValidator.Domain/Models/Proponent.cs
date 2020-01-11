using System;

namespace ProposalValidator.Domain.Models
{
    public class Proponent
    {
        public Proponent() { }

        public Proponent(Guid id, String name, int age, decimal monthlyIncome, bool isMain)
        {
            Id = id;
            Name = name;
            Age = age;
            MonthlyIncome = monthlyIncome;
            IsMain = isMain;
        }

        public Guid Id { get; set; }

        public String Name { get; set; }

        public int Age { get; set; }

        public decimal MonthlyIncome { get; set; }

        public bool IsMain { get; set; }
    }
}
