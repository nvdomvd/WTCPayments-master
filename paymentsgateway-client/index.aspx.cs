using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["msg"] != null)
        {
            if (!Session["msg"].Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessage", "showMessage('" + Session["msg"] + "')", true);
               
            }
            Session["msg"] = null;
        }
        if (Session["script"] != null)
        {
            if (!Session["script"].Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "validatePay", Session["script"].ToString(), true);
                Session["script"] = "";
            }
        }

    }

    public string ConnectPayment(string trx_type, string tender, string creditcardtype, string acct, string expdate, string cvv2, string fname, string lname, string amt)
    {
        try
        {
            string credstr = "USER=" + ConfigurationManager.AppSettings["User"] + "&VENDOR=" + ConfigurationManager.AppSettings["Vendor"] + "&PARTNER=" + ConfigurationManager.AppSettings["Partner"] + "&PWD=" + ConfigurationManager.AppSettings["PWD"];
            string reqstr = "&TRXTYPE=" + trx_type + "&TENDER=" + tender + "&ACCT=" + acct + "&AMT=" + amt + "&EXPDATE=" + expdate + "&CVV2=" + cvv2 + "&CARDTYPE=" + creditcardtype + "&VERBOSITY=HIGH" + "&COMMENT1=comment1" + "&COMMENT2=comment2" + "&CUSTOM=custom" + "&INVNUM=Invoice" + "&BILLTOFIRSTNAME=" + fname + "&BILLTOLASTNAME=" + lname;
            string nvp_req = credstr + reqstr;

            HttpWebRequest payReq = (HttpWebRequest)WebRequest.Create(ConfigurationSettings.AppSettings["PayPalEndpoint"]);
            payReq.Method = "POST";
            payReq.ContentLength = nvp_req.Length;
            payReq.ContentType = "application/x-www-form-urlencoded";

            System.IO.StreamWriter sw = new System.IO.StreamWriter(payReq.GetRequestStream());
            sw.Write(nvp_req);
            sw.Close();

            HttpWebResponse payResp = (HttpWebResponse)payReq.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(payResp.GetResponseStream());
            string response = sr.ReadToEnd();
            sr.Close();

            System.Collections.Specialized.NameValueCollection dict = new System.Collections.Specialized.NameValueCollection();
            foreach (string nvp in response.Split('&'))
            {
                string[] keys = nvp.Split('=');
                dict.Add(keys[0], keys[1]);
            }

            if (dict["RESULT"] != "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessage", "showMessage('The operation was not carried out.');", true);
                
            }
            else
            {
                Session["msg"] = "The operation was successful.";
                //Session["script"] = "confirmPayment('"+ payment_id.Value + "','PayPalPro','"+ (payment_id.Value!=null && payment_id.Value!="" ? payment_id.Value : "no_reference") + "','"+ ConfigurationManager.AppSettings["StatusApprovedID"] +"')";               
                Response.Redirect("index.aspx", true);
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "showMessage", "showMessage('The operation was not carried out.')", true);
        }
        return "";
    }

    protected void btnPaymentCard_Click(object sender, EventArgs e)
    {
        try
        {
            ConnectPayment("S", "C", cardtype.Items[cardtype.SelectedIndex].Value, acct.Value, expdate.Value, cvv2.Value, fname.Value, lname.Value, amount.Value);
        }
        catch (Exception)
        {

        }
    }
}
