using MediatR;
using Site.Application.Models.Payment;
using System;

namespace Site.Application.Features.Commands.Payment.PayBill
{
    public class PayBillCommand:IRequest<string>
    { 
        public PayBillCommand(PaymentModel paymentModel, int userId)
        {
            UserId = userId;
            Pay = paymentModel.Pay;
            Month = paymentModel.Month;
            CreditCardNumber = paymentModel.CreditCardNumber;
            ExpireDate = paymentModel.ExpireDate;
            Cvc = paymentModel.Cvc;
        }

        public int UserId { get; set; }
        public decimal Pay { get; set; }
        public int Month { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Cvc { get; set; }
    }
}
