using System;
using System.Collections.Generic;
using System.Linq;

namespace WBPayments_Logic
{
    public class PaymentDao : IPaymentDao
    {
        public Gateway GetGateway(string gatewayId)
        {
            using (var db = new wbpaymentsEntities())
            {
                //get from bd the gateway which gateway_id == gatewayId
                var query = from g in db.Gateway
                            orderby g.gateway_id
                            where g.gateway_id == gatewayId
                            select g;
                return query.FirstOrDefault<Gateway>();
            }
        }

        public List<Gateway> GetGateways()
        {
            using (var db = new wbpaymentsEntities())
            {
                //get all the gateways from the db ordered by gateway_name
                var query = from g in db.Gateway
                            orderby g.gateway_name
                            select g;
                return query.ToList();
            }
        }

        public List<Payment> GetApprovedPaymentsByInvoice(Invoice invoice)
        {
            using (var db = new wbpaymentsEntities())
            {
                //return all the payments made with the same invoice_id
                var query = from p in db.Payment
                            orderby p.payment_id
                            where p.invoice_id == invoice.invoice_id && p.payment_status_id == Constants.PAYMENT_STATUS_APPROVED_ID
                            select p;
                return query.ToList();
            }
        }

        public Invoice GetInvoiceByNumber(string number)
        {
            using (var db = new wbpaymentsEntities())
            {
                //get from bd the invoice which invoice_number == number
                var query = from p in db.Invoice
                            orderby p.invoice_id
                            where p.invoice_number == number
                            select p;
                return query.FirstOrDefault<Invoice>();
            }
        }

        public Currency GetCurrencyById(string currencyId)
        {
            using (var db = new wbpaymentsEntities())
            {
                //get from bd the currency which currency_id == currencyId
                var query = from c in db.Currency
                            orderby c.currency_id
                            where c.currency_id == currencyId
                            select c;
                return query.FirstOrDefault<Currency>();
            }
        }

        public Client GetClientById(string clientId)
        {
            using (var db = new wbpaymentsEntities())
            {
                //get from bd the client which client_id == clientId
                var query = from c in db.Client
                            orderby c.client_id
                            where c.client_id == clientId
                            select c;
                return query.FirstOrDefault<Client>();
            }
        }

        public Payment GetPaymentById(int paymentId)
        {
            using (var db = new wbpaymentsEntities())
            {
                //get from bd the peymentstatus which payment_id == paymentId
                var query = from p in db.Payment
                            orderby p.payment_id
                            where p.payment_id == paymentId
                            select p;
                return query.FirstOrDefault<Payment>();
            }
        }

        public PaymentStatus GetPaymentStatusById(string statusId)
        {
            using (var db = new wbpaymentsEntities())
            {
                //get from bd the peymentstatus which payment_status_id == statusId
                var query = from ps in db.PaymentStatus
                            orderby ps.payment_status_id
                            where ps.payment_status_id == statusId
                            select ps;
                return query.FirstOrDefault<PaymentStatus>();
            }
        }

        public Payment ConfirmPaymentAddingGatewayPaymentData(Payment payment, PaymentStatus status, string gatewayResponse, string gatewayReference, DateTime date)
        {
            using (var db = new wbpaymentsEntities())
            {
                //create the gateway payment data
                GatewayPaymentData creation = new GatewayPaymentData { gateway_response = gatewayResponse, gateway_reference = gatewayReference, date_created = date, payment_id = payment.payment_id };
                db.GatewayPaymentData.Add(creation);
                //get the payment from de db
                var result = db.Payment.SingleOrDefault(p => p.payment_id == payment.payment_id);
                //update payment's status
                result.payment_status_id = status.payment_status_id;
                db.SaveChanges();
                return result;
            }
        }

        public Payment CreatePayment(decimal amount, string description, DateTime date_created, Currency currency, Invoice invoice, Client client, Gateway gateway, PaymentStatus status)
        {
            using (var db = new wbpaymentsEntities())
            {
                //create a new payment with the data from params and add it to db
                Payment creation = new Payment { amount = amount, date_created = date_created, description = description, client_id = client.client_id, payment_status_id = status.payment_status_id, currency_id = currency.currency_id, invoice_id = invoice.invoice_id, gateway_id = gateway.gateway_id };
                db.Payment.Add(creation);
                db.SaveChanges();
                return creation;
            }
        }

        public Payment CreatePaymentAndInvoice(string invoiceNumber, string clientId, string currencyId, DateTime date, decimal amount, string description, Gateway gateway, PaymentStatus status)
        {
            using (var db = new wbpaymentsEntities())
            {
                //create a new invoice with the data from params and add it to the db
                Invoice invoice = new Invoice { invoice_number = invoiceNumber, client_id = clientId, date_created = date };
                db.Invoice.Add(invoice);
                //create a new payment with the data from params and the invoice just created
                Payment creation = new Payment { amount = amount, date_created = date, description = description, client_id = clientId, payment_status_id = status.payment_status_id, currency_id = currencyId, invoice_id = invoice.invoice_id, gateway_id = gateway.gateway_id };
                db.Payment.Add(creation);
                db.SaveChanges();
                return creation;
            }
        }
    }
}