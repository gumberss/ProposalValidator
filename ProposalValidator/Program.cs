using ProposalValidator.Application.Proposals;
using ProposalValidator.Handler.Handlers;
using System;

namespace ProposalValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler = new ProposalEventsHandler(new ProposalValidatorAppService());

            Console.Write(handler.Handle(args));
        }
    }
}
