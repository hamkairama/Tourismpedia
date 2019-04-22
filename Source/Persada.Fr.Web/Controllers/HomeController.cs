using Newtonsoft.Json;
using Persada.Fr.CommonFunction;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Persada.Fr.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataTable dtGMap = new DataTable();
            //dtGMap = GetdtLatLong();
            List<TOURIS_TV_CATEGORY> categories = new List<TOURIS_TV_CATEGORY>();

            //slideshowRes 
            ViewBag.GetSlideshow = JsonConvert.DeserializeObject<List<TOURIS_TV_SLIDESHOW>>(ParsingObject.RequestData(null, "Slideshow", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            Session["GetSosmedList"] = JsonConvert.DeserializeObject<List<TOURIS_TV_SOSMED>>(ParsingObject.RequestData(null, "Sosmed", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            categories = JsonConvert.DeserializeObject<List<TOURIS_TV_CATEGORY>>(ParsingObject.RequestData(null, "Category", "GridBind", EnumList.IHttpMethod.Get.ToString()));

            return View(categories);
        }

        public DataTable GetdtLatLong()
        {
            string strAddress = "Jl. Pos Pengumben Raya No. 77. kebon jeruk, Jakarta Barat. DKI Jakarta, Indonesia";
            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + strAddress + "&sensor=false";
            DataTable dtGMap = new DataTable();
            DataTable dtCoordinates = new DataTable();
            WebRequest request = WebRequest.Create(url);

            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);

                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("id", typeof(int)),
                        new DataColumn("Address", typeof(string)),
                        new DataColumn("Latitude",typeof(string)),
                        new DataColumn("Longitude",typeof(string)) });
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                        dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                    }
                }
            }

            dtGMap = dtCoordinates;
            return dtGMap;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];

            return View();
        }

        public ActionResult SendEmail(TOURIS_TV_CONTACT_US contactUs, HttpPostedFileBase postedFile)
        {
            ResultStatus rs = new ResultStatus();
            MailMessage mail = new MailMessage();
            CUSTOM_MAIL customMail = new CUSTOM_MAIL();

            string[] to = { "admin@professionalis.me" };
            string[] from = { "admin@professionalis.me" };
            string[] cc = { contactUs.EMAIL_SENDER };
            string subject = "[professionalis.me] Customer Service - " + contactUs.NAME_SENDER;
            string body = contactUs.DESCRIPTION;

            string attachmentName = "";
            if (postedFile != null)
            {
                attachmentName = postedFile.FileName;
            }

            if (ModelState.IsValid)
            {
                customMail.TO = to;
                customMail.FROM = from;
                customMail.CC = cc;
                customMail.SUBJECT = subject;
                customMail.BODY = body;
                customMail.ISBODYHTML = true;
                try
                {
                    Email email = new Email();
                    mail = email.MappingEmail(customMail);
                    rs = email.SendEmail(mail, attachmentName);
                    TempData["msgSuccess"] = rs.MessageText;
                }
                catch (DataException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    rs.SetErrorStatus("Data failed to sent");
                    TempData["msgError"] = rs.MessageText;
                }
            }

            return RedirectToAction("Contact");
        }
    }
}