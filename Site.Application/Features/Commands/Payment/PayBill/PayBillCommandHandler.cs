using MediatR;
using FluentValidation;
using Site.Application.Contracts.Persistence.Repositories.BillPayments;
using System.Threading;
using System.Threading.Tasks;
using Site.Domain.Enums;
using RabbitMQ.Client;
using System.Text.Json;
using Site.Domain.Dtos;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Site.Application.Contracts.Persistence.Repositories.Bills;

namespace Site.Application.Features.Commands.Payment.PayBill
{
    public class PayBillCommandHandler : IRequestHandler<PayBillCommand, string>
    {
        private readonly IBillPaymentRepository _billPaymentRepository;
        private readonly IBillRepository _billRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly PayBillValidator _validator;
        private readonly ConnectionFactory factory;
        private readonly IConnection connection;

        public PayBillCommandHandler(IBillPaymentRepository billPaymentRepository, IBillRepository billRepository, IDistributedCache distributedCache)
        {
            _billPaymentRepository = billPaymentRepository;
            _billRepository = billRepository;
            _distributedCache = distributedCache;
            _validator = new PayBillValidator();

            factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "test",
                Password = "test"
            };

            connection = factory.CreateConnection();
        }

        public async Task<string> Handle(PayBillCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            CreditCardDto creditCard = new CreditCardDto();
            creditCard.UserId = request.UserId;
            creditCard.Pay = request.Pay;
            creditCard.ExpireDate = request.ExpireDate;
            creditCard.Cvc = request.Cvc;
            creditCard.CreditCardNumber = request.CreditCardNumber;

            using (var channel = this.connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "payment",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var customerPayload = JsonSerializer.Serialize(creditCard);

                var body = Encoding.UTF8.GetBytes(customerPayload);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "payment",
                    basicProperties: null,
                    body: body
                );
            }

            var bill = _billPaymentRepository.GetBillByUserIdAndMonth(request.UserId, request.Month);

            var restOfDept = bill.TotalDept - request.Pay;

            var updateBill = await _billRepository.GetByIdAsync(bill.BillId);
            updateBill.TotalDept = updateBill.TotalDept - request.Pay;
            await _billRepository.UpdateAsync(updateBill);

            await _distributedCache.RemoveAsync("GetBill");
            await _distributedCache.RemoveAsync("GetAllBills");

            if (restOfDept == 0)
            {
                bill.TotalDept = 0;
                await _billPaymentRepository.UpdateAsync(bill);
                return $"{(MonthEnum)request.Month} ayı faturanız ödenmiştir.";
            }
            else
            {
                bill.TotalDept = restOfDept;
                bill.Electric = restOfDept / 4;
                bill.Water = restOfDept / 4;
                bill.NaturalGas = restOfDept / 4;
                bill.Dues = restOfDept / 4;

                await _billPaymentRepository.UpdateAsync(bill);
                return $"{(MonthEnum)request.Month} ayı faturanızın {request.Pay}tl kadarı ödenmiştir. Kalan borç = {restOfDept}tl.";
            }
        }
    }
}
