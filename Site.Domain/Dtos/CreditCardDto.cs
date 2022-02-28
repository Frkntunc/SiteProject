using System;

namespace Site.Domain.Dtos
{
    public class CreditCardDto
    {
        public int UserId { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Cvc { get; set; }
        public decimal Pay { get; set; }
    }
}
