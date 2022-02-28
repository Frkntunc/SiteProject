using MediatR;
using System;

namespace PaymentService.Application.Features.Commands.CreditCards.AddCreditCard
{
    public class AddCreditCardCommand:IRequest
    {
        public int UserId { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Cvc { get; set; }
        public decimal Balance { get; set; }
    }
}
