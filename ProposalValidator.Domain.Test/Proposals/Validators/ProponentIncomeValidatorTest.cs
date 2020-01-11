using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProposalValidator.Domain.Models;
using ProposalValidator.Domain.Proposals.Validators;
using ProposalValidator.Domain.Test.Catagories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProposalValidator.Domain.Test.Proposals.Validators
{
    [TestClass]
    public class ProponentIncomeValidatorTest
    {
        private readonly ProponentIncomeValidator _proponentIncomeValidator;

        public ProponentIncomeValidatorTest()
        {
            _proponentIncomeValidator = new ProponentIncomeValidator();
        }

        [DataTestMethod]
        [TestCategory(TestCategories.VALIDATOR)]
        [DataRow(1000, 23, 3_999.99, false, "A renda do proponente principal com menos de 24 anos deve ser 4 vezes maior que o valor da parcela do empréstimo")]
        [DataRow(1000, 23, 4_000.00, true, "A renda do proponente principal com menos de 24 anos é maior que 4 vezes o  valor da parcela do emprestimo")]

        [DataRow(1000, 24, 3_999.99, true, "O proponente principal possui 24 anos, sendo asim é necessário ter renda 3 vezes maior que o valor da parcela do emprestimo")]
        [DataRow(1000, 49, 2_999.99, false, "A renda do proponente principal idade entre [24 e 49] deve ser 3 vezes maior que o valor da parcela do empréstimo")]
        [DataRow(1000, 49, 3_000.00, true, "A renda do proponente principal idade entre [24 e 49] é 3 vezes maior que o valor da parcela do empréstimo")]

        [DataRow(1000, 50, 2_999.99, true, "O proponente principal possui 50 anos, sendo assim é necessário ter renda 2 vezes maior que o valor da parcela do empréstimo")]
        [DataRow(1000, 50, 1_999.99, false, "O proponente principal possui 50 anos, sendo assim é necessário ter renda 2 vezes maior que o valor da parcela do empréstimo")]
        [DataRow(1000, 60, 2_000.00, true, "O proponente principal possui 60 anos, e sua renda é 2 vezes o valor da parcela do emprestimo")]
        [DataRow(1000, 60, 5_000.00, true, "O proponente principal possui 60 anos, e sua renda é maior que 2 vezes o valor da parcela do emprestimo")]
        public void Deveria_validar_corretamente_a_renda_do_proponente_principal(double doubleLoanValue, int mainProponentAge, double doubleMainProponentIncome, bool expected, String because)
        {
            var loanValue = Convert.ToDecimal(doubleLoanValue);
            var mainProponentIncome = Convert.ToDecimal(doubleMainProponentIncome);

            var proposal = new Proposal(Guid.NewGuid(), loanValue, 1)
            {
                Proponents = new List<Proponent>
                {
                    new Proponent
                    {
                        IsMain = true,
                        Age = mainProponentAge,
                        MonthlyIncome = mainProponentIncome
                    }
                }
            };

            var isValid = _proponentIncomeValidator.Validate(proposal);

            isValid.Should().Be(expected, because: because);
        }
    }
}
