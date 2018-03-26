using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPayments_Logic
{
    public interface IPaymentDao
    {
        List<Gateway> GetGateways();

        Gateway GetGateway(string gatewayId);

        List<Payment> GetApprovedPaymentsByInvoice(Invoice invoice);

        Invoice GetInvoiceByNumber(string number);

        Currency GetCurrencyById(string currencyId);

        Client GetClientById(string clientId);

        PaymentStatus GetPaymentStatusById(string statusId);

        Payment CreatePayment(decimal amount, string description, DateTime date_created, Currency currency, Invoice invoice, Client client, Gateway gateway, PaymentStatus status);

        Payment CreatePaymentAndInvoice(string invoiceNumber, string clientId, string currencyId, DateTime date, decimal amount, string description, Gateway gateway, PaymentStatus status);

        Payment GetPaymentById(int paymentId);

        Payment ConfirmPaymentAddingGatewayPaymentData(Payment payment, PaymentStatus status, string gatewayResponse, string gatewayReference, DateTime date);
    }
}
