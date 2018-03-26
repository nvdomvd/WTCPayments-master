using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WBPayments_Logic
{
    public class PaymentManager: IPaymentManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PaymentManager));
        private readonly IPaymentDao paymentDao;
        private readonly ISettingsManager settingsManager;

        public PaymentManager (IPaymentDao pay, ISettingsManager settings)
        {
            paymentDao = pay;
            settingsManager = settings;
        }

        public ReturnObject<int> CreatePayment(CreatePaymentObject createPaymentObject, DateTime date)
        {
            //create the object to return later
            ReturnObject<int> response = new ReturnObject<int>(0);
            try {
                if (String.IsNullOrWhiteSpace(createPaymentObject.InvoiceNumber) || createPaymentObject.Amount <= 0)
                {
                    log.InfoFormat("invoice {0} or amount {1} are empty", createPaymentObject.InvoiceNumber, createPaymentObject.Amount);
                    response.addErrorMsg(Properties.Settings.Default.errorMessageMisingInfo);
                    response.Code = ReturnObject<int>.RETURN_CODE_BAD_REQUEST;
                    return response;
                }

                //check if the entities are in the db
                Gateway gateway = paymentDao.GetGateway(createPaymentObject.GatewayId);
                Currency currency = paymentDao.GetCurrencyById(createPaymentObject.CurrencyId);
                Client client = paymentDao.GetClientById(createPaymentObject.ClientId);
            
                //of one of them are missing exit with message
                if (gateway == null || currency == null || client == null)
                {
                    log.InfoFormat("gateway {0} currency {1} or client {2} are null", gateway, currency, client);
                    response.addErrorMsg(Properties.Settings.Default.errorMessageMisingData);
                    response.Code = ReturnObject<int>.RETURN_CODE_BAD_REQUEST;
                    return response;
                }

                PaymentStatus statusNew = paymentDao.GetPaymentStatusById(Constants.PAYMENT_STATUS_NEW_ID);
                if (statusNew == null)
                {
                    //if cannot found status exit with error message
                    log.ErrorFormat("cound not found PaymentStatus {0}", Constants.PAYMENT_STATUS_NEW_ID);
                    response.addErrorMsg(Properties.Settings.Default.errorMessageInternalError);
                    response.Code = ReturnObject<int>.RETURN_CODE_INTERNAL_ERROR;
                    return response;
                }

                //check if invoice exist in db
                Invoice invoice = paymentDao.GetInvoiceByNumber(createPaymentObject.InvoiceNumber);
                if (invoice != null)
                {
                    //read payment settings
                    int maxPaymentsPerInvoice;
                    if (settingsManager.GetIntSetting(Constants.PAYMENTS_INVOICE_MAX, out maxPaymentsPerInvoice))
                    {
                        //if current payments of invoice are gratear than equal max allowed, then exit with error message
                        if (paymentDao.GetApprovedPaymentsByInvoice(invoice).Count >= maxPaymentsPerInvoice && !(maxPaymentsPerInvoice == Constants.PAYMENTS_INVOICE_INFINITY))
                        {
                            log.InfoFormat("the invoice {0} have reached max payments {1}", invoice, maxPaymentsPerInvoice);
                            response.addErrorMsg(Properties.Settings.Default.errorMessageMaxPaymentsReached);
                            response.Code = ReturnObject<int>.RETURN_CODE_PRECONDITION_FAILED;
                            return response;
                        } else
                        {
                            //invoice is actually in db so I only have to create payment
                            Payment creation = paymentDao.CreatePayment(createPaymentObject.Amount, createPaymentObject.Comment, date, currency, invoice, client, gateway, statusNew);
                            response.set(creation.payment_id);
                            response.Code = ReturnObject<int>.RETURN_CODE_CREATED;
                            log.InfoFormat("the payment {0} have been created and added to the invoice {1}", creation, invoice);
                            return response;
                        }
                    } else
                    {
                        //if cannot get max payments allowed per invoice exit with error message
                        log.ErrorFormat("cound not found setting {0}", Constants.PAYMENTS_INVOICE_MAX);
                        response.addErrorMsg(Properties.Settings.Default.errorMessageInternalError);
                        response.Code = ReturnObject<int>.RETURN_CODE_INTERNAL_ERROR;
                        return response;
                    }
                } else
                {
                    // the invoice is not in the db, so have to create invoice and payment in same transaction.
                    Payment creation = paymentDao.CreatePaymentAndInvoice(createPaymentObject.InvoiceNumber, createPaymentObject.ClientId, createPaymentObject.CurrencyId, date, createPaymentObject.Amount, createPaymentObject.Comment, gateway, statusNew);
                    //Payment creation = paymentDao.CreatePayment(createPaymentObject.Amount, createPaymentObject.Comment, date, currency, invoice, client, gateway, statusNew);
                    response.set(creation.payment_id);
                    response.Code = ReturnObject<int>.RETURN_CODE_CREATED;
                    log.InfoFormat("the payment {0} have been created", creation);
                    return response;
                }
            //handling validation errors
            } catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                log.ErrorFormat("exception {0} was thrown when CreatePayment, message: {1} trace: {2}", e, e.Message, e.StackTrace);
                response.addWarnMsg(Properties.Settings.Default.warnFieldsNotValid);
                response.Code = ReturnObject<int>.RETURN_CODE_BAD_REQUEST;
                return response;
            } catch (Exception e)
            {
                log.ErrorFormat("exception {0} was thrown when CreatePayment, message: {1} trace: {2}", e, e.Message, e.StackTrace);
                response.addErrorMsg(Properties.Settings.Default.errorMessageInternalError);
                response.Code = ReturnObject<int>.RETURN_CODE_INTERNAL_ERROR;
                return response;
            }
        }

        public ReturnObject<int> ConfirmPayment(ConfirmPaymentObject confirmPaymentObject, DateTime date)
        {
            //create the object to return later
            ReturnObject<int> response = new ReturnObject<int>(0);

            try
            {
                //check if have all the data needed in the object
                if (String.IsNullOrWhiteSpace(confirmPaymentObject.GatewayReference) || String.IsNullOrWhiteSpace(confirmPaymentObject.GatewayResponse))
                {
                    log.InfoFormat("reference {0} or response{1} are empty", confirmPaymentObject.GatewayReference, confirmPaymentObject.GatewayResponse);
                    response.addErrorMsg(Properties.Settings.Default.errorMessageMisingInfo); 
                    response.Code = ReturnObject<int>.RETURN_CODE_BAD_REQUEST;
                    return response;
                }

                //check if the entities are in the db
                PaymentStatus status = paymentDao.GetPaymentStatusById(confirmPaymentObject.StatusId);
                Payment payment = paymentDao.GetPaymentById(confirmPaymentObject.PaymentId);
                Client client = paymentDao.GetClientById(confirmPaymentObject.ClientId);

                //of one of them are missing exit with message
                if (status == null || client == null)
                {
                    log.InfoFormat("status {0} or client {1} are null", status, client);
                    response.addErrorMsg(Properties.Settings.Default.errorMessageMisingData);
                    response.Code = ReturnObject<int>.RETURN_CODE_BAD_REQUEST;
                    return response;
                }

                //check if the payment got exist in the db, if not exit with message
                if (payment == null)
                {
                    log.InfoFormat("payment id {0} not found", confirmPaymentObject.PaymentId);
                    response.addErrorMsg(Properties.Settings.Default.errorMessagePaymentNotFound);
                    response.Code = ReturnObject<int>.RETURN_CODE_NOT_FOUND;
                    return response;
                }

                //chack that the status of the payment is the one that can be changed
                if (payment.payment_status_id != Constants.PAYMENT_STATUS_NEW_ID)
                {
                    log.WarnFormat("payment {0} has not status new", payment);
                    response.addErrorMsg(Properties.Settings.Default.errorMessageUnchangebleStatus);
                    response.Code = ReturnObject<int>.RETURN_CODE_PRECONDITION_FAILED;
                    return response;
                }

                //create the GatewayPaymentData and confirm the payment
                Payment updated = paymentDao.ConfirmPaymentAddingGatewayPaymentData(payment, status, confirmPaymentObject.GatewayResponse, confirmPaymentObject.GatewayReference, date);

                response.set(updated.payment_id);
                response.Code = ReturnObject<int>.RETURN_CODE_OK;
                log.InfoFormat("the payment {0} have been confirm with the status {1}", updated, status);
                return response;
            }
            //handling validation errors
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                log.ErrorFormat("exception {0} was thrown when ConfirmPayment, message: {1} trace: {2}", e, e.Message, e.StackTrace);
                response.addWarnMsg(Properties.Settings.Default.warnFieldsNotValid);
                response.Code = ReturnObject<int>.RETURN_CODE_BAD_REQUEST;
                return response;
            } catch (Exception e)
            {
                log.ErrorFormat("exception {0} was thrown when ConfirmPayment, message: {1} trace: {2}", e, e.Message, e.StackTrace);
                response.addErrorMsg(Properties.Settings.Default.errorMessageInternalError);
                response.Code = ReturnObject<int>.RETURN_CODE_INTERNAL_ERROR;
                return response;
            }
        }
    }
}