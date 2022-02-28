using PaymentService.Domain.Entities.Commons;
using System;

namespace PaymentService.Domain.Entities
{
    public class CreditCard : EntityBase
    {
        public int UserId { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Cvc { get; set; }
        public decimal Balance { get; set; }
    }
}
