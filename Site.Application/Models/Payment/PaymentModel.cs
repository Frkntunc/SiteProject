using System;

namespace Site.Application.Models.Payment
{
    public class PaymentModel
    {
        public string CreditCardNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Cvc { get; set; }
        public decimal Pay { get; set; }
        public int Month { get; set; }
    }
}
