using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using Telerik.Web.UI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JIRAWorkLogAPP
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Message msg = new Message();
            //string msg = HttpClientHelper.GetSingleDataFromApi<Message>("api/2/issue/TRX-3295/worklog/");
            if (! IsPostBack)
            {
                //lbl.Text = "Tradex work log from 2016-05-23";

            }
        }


        protected void DoSearch_Click(object sender, EventArgs e)
        {
            RadGrid1.Rebind();
        }

        protected void RadGrid1_NeedDataSource1(object sender, GridNeedDataSourceEventArgs e)
        {
            DateTime dtStart = Convert.ToDateTime(fromDate.SelectedDate);
            DateTime dtEnd = Convert.ToDateTime(toDate.SelectedDate);
            DateTime dtStartSearch;
            DateTime dtEndSearch;

            if (fromDate.SelectedDate ==null)
            {
                dtStart = new DateTime(2016, 6, 20);
                dtStartSearch= new DateTime(2016, 6, 20);
            }
            else
            {
                dtStart = Convert.ToDateTime(fromDate.SelectedDate);
                dtStartSearch = Convert.ToDateTime(fromDate.SelectedDate);
                dtStartSearch = dtStart.AddDays(-1);
            }

            if (toDate.SelectedDate == null)
            {
                dtEnd = DateTime.Now.AddDays(1);
                dtEndSearch = DateTime.Now;
            }
            else
            {
                dtEnd = Convert.ToDateTime(toDate.SelectedDate);
                dtEnd = dtEnd.AddDays(1);
            }

            string dateQuery = "worklogDate%20>%20%27" + dtStartSearch.ToString("yyyy-MM-dd") + "%27%20and%20worklogDate%20<%20" + dtEnd.ToString("yyyy-MM-dd");
            //worklogDate%20>%20%272016-05-23%27%20and%20worklogDate%20<%20endOfDay()
            var resultH = HttpClientHelper.PostDataToApi("api/2/search?fields=worklog,summary&jql=filter=27470%20and%20" + dateQuery +"%20");

            JIRASearchResult searchResult = JsonConvert.DeserializeObject<JIRASearchResult>(resultH.Result);

            List<Issue> issues = searchResult.issues;
                       
                        //var query3 = from Issue i in issues
            //             where i.fields.worklog.worklogs.Any(w => w.author.emailAddress == "henrik.florin@advent.com")
            //             select i.fields.worklog.worklogs;

            //var query4 = from Issue i in issues
            //             where i.fields.worklog.worklogs.Any(w => w.author.emailAddress == "henrik.florin@advent.com")
            //             select i.fields.worklog.worklogs.ToArray();

            var query5 = from Issue i in issues
                         select i.fields.worklog.worklogs;

            List<Worklog2> worklist = new List<Worklog2>();
            List<UserLogInfo> logs = new List<UserLogInfo>();
            UserLogInfo log;

            string timeZoneStr = "";
            DateTime convertDate;

            switch (region.SelectedValue)
            {
                case "BJ":
                    timeZoneStr = "China Standard Time";
                    break;
                case "SF":
                    timeZoneStr = "Pacific Standard Time";
                    break;
                case "OS":
                    timeZoneStr = "Central Europe Standard Time";
                    break;
            }

            TimeZoneInfo zoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneStr);


                    
            foreach (Issue i in issues)
            {
                if (i.fields.worklog.worklogs.Count == 20)
                {
                    string strQuery = "api/2/issue/" + i.id + "/worklog";
                    var resultOverLoad = HttpClientHelper.PostDataToApi(strQuery);

                    Worklog overloadResult = JsonConvert.DeserializeObject<Worklog>(resultOverLoad.Result);

                    foreach (Worklog2 w in overloadResult.worklogs)
                    {
                        convertDate = TimeZoneInfo.ConvertTime(Convert.ToDateTime(w.started), TimeZoneInfo.Local, zoneInfo);

                        if (Convert.ToDateTime(convertDate).CompareTo(dtStart) >= 0 && Convert.ToDateTime(convertDate).CompareTo(dtEnd) <= 0)
                        {
                            log = new UserLogInfo();
                            log.UserName = w.author.displayName;
                            log.LogHour = Convert.ToInt32(w.timeSpentSeconds) / 3600;
                            log.LogDate = Convert.ToDateTime(w.created);
                            log.JIRAID = i.key;
                            log.JIRASummary = i.fields.summary;
                            logs.Add(log);
                        }
                    }
                }
                else
                {

                    foreach (Worklog2 w in i.fields.worklog.worklogs)
                    {
                        convertDate = TimeZoneInfo.ConvertTime(Convert.ToDateTime(w.started), TimeZoneInfo.Local, zoneInfo);

                        if (Convert.ToDateTime(convertDate).CompareTo(dtStart) >= 0 && Convert.ToDateTime(convertDate).CompareTo(dtEnd) <= 0)
                        {
                            log = new UserLogInfo();
                            log.UserName = w.author.displayName;
                            log.LogHour = Convert.ToDouble( Convert.ToDouble( w.timeSpentSeconds / 3600.0).ToString("0.0"));
                            log.LogDate = Convert.ToDateTime( w.created);
                            log.JIRAID = i.key;
                            log.JIRASummary = i.fields.summary;
                            logs.Add(log);
                        }
                    }
                }
                   
            }


            //var list = query5.ToList();
            

            //foreach (List<Worklog2> item in list )
            //{
            //    foreach(Worklog2 i in item)
            //    {
            //        log = new UserLogInfo();
            //        log.UserName = i.author.displayName;
            //        log.LogHour = i.timeSpent;
            //        log.LogDate = i.started;
                    
            //        logs.Add(log);
            //    }
            //}

          

          
           
                

            RadGrid1.DataSource = logs;  
        }

        public class UserLogInfo
        {
            public string UserName { get; set; }
            public double LogHour { get; set; }
            public DateTime LogDate { get; set; }
            public string JIRAID { get; set; }
            public string JIRASummary { get; set; }
        }


        public class UserLoginin
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class Message
        {
            public string info { get; set; }
        }

        protected void region_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadGrid1.Rebind();
        }
    }
}