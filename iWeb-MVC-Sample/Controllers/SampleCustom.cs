using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Avectra.netForum.BaseUI;
using Avectra.netForum.iWeb.Models;
using Avectra.netForum.Common;
using Avectra.netForum.Data;
using Newtonsoft.Json;
using System.Dynamic;

namespace iWeb_MVC_Sample.Controllers
{
    public class SampleCustomController : BaseController
    {
        public ActionResult BasicInfo()
        {
            DynamicPageModel model = new DynamicPageModel("00970de8-d115-4d5a-b678-4e57ba474561", "");

            using(NfDbConnection dbConn = DataUtils.GetConnection())
            {
                using (NfDbCommand dbCmd = new NfDbCommand("SELECT COUNT(*) AS [indcount] FROM co_individual", dbConn))
                {
                    dbConn.Open();
                    using (NfDbDataReader reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ViewData["ind_count"] = reader["indcount"].ToString();
                        }
                    }
                }
            }

            return base.View(model);
        }

        public JsonResult LookupID(string input)
        {
            IDictionary<string, object> queryParams = JsonConvert.DeserializeObject<ExpandoObject>(Request["params"]);
            string sql = "SELECT cst_key, cst_type FROM co_customer WHERE cst_recno=@recno";
            string formKey = string.Empty;
            string cstKey = string.Empty;
            try
            {
                using (NfDbConnection dbConn = DataUtils.GetConnection())
                {
                    using (NfDbCommand dbCmd = new NfDbCommand(sql, dbConn))
                    {
                        dbCmd.Parameters.AddWithValue("recno", queryParams["recno"]);
                        using (NfDbDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cstKey = reader["cst_key"].ToString();
                                formKey = reader["cst_type"].ToString() == "Individual" ? "b772881d-d704-40f3-92b6-09b13a50fcc9" : "f326228c-3c49-4531-b80d-d59600485557";
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return Json("An error occurred", JsonRequestBehavior.AllowGet);
            }

            return Json(new { formKey = formKey, cstKey = cstKey }, JsonRequestBehavior.AllowGet);
        }
    }
}