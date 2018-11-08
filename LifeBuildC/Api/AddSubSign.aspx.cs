/*
 用途：
   生命建造-課程報名

 流程：
   [View]SubjectSignUp.aspx?id=c1 > 報名 > [API]AddSubSign

 API演算法：
   1. C1 沒有審核的限制.
   2. C2 沒有通過 C1 就不能上.

 */

using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using libLifeBuildC;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Api
{
    public partial class AddSubSign : System.Web.UI.Page
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        GoogleSheetApi Google_Sheet_Api;
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();
        ApiData.ApiAddSubSign Api_Data = new ApiData.ApiAddSubSign();

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/LogConfig/AddSubSign.config")));

            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.ApiAddSubSign>(Api_Info.strJsonData);

            if (Api_Data != null)
            {
                logger.Info("StreamR=[" + Api_Info.strJsonData + "]");
                ApiProcess();
            }

            logger.Info("Recv=[" + JsonConvert.SerializeObject(Api_Data) + "]");
            logger.Info("\r\n");
            Response.Write(JsonConvert.SerializeObject(Api_Data));
            Response.End();
        }

        private void ApiProcess()
        {
            try
            {
                Api_Data.IsApiError = false;

                //小組
                Api_Info.GetGroupData(Api_Data.group, Api_Data.gcroup);

                if (Api_Data.CategoryID == "C1")
                { //C1課程沒有限制報名資格

                    Google_Sheet_Api = null;
                    Google_Sheet_Api = new GoogleSheetApi("1HCRBI2C_cVl0fH5576PEX7UULWsgcxx1sbYdRQ6FcF8", "C1報名");
                    AddSubSignProcess();
                    Api_Data.ApiMsg = "C1 課程報名成功";

                }
                else if (Api_Data.CategoryID == "C2")
                { //C2課程需上完C1才可報名

                    //DataTable dtMem = chcmember.GetChcMemberByGroup(GroupCName, GroupName, api.Ename);
                    string _MID = "0";
                    if (Api_Data.MID.IndexOf(',') > 0)
                    {
                        _MID = Api_Data.MID.Split(',')[0];
                    }

                    DataTable dtMem = Ado_Info.ChcMember_ADO.QueryChcMemberByMID(_MID);
                    if (dtMem != null && dtMem.Rows.Count > 0)
                    { //一定要是會友

                        bool IsC112 = bool.Parse(dtMem.Rows[0]["IsC112"].ToString());
                        bool IsC134 = bool.Parse(dtMem.Rows[0]["IsC134"].ToString());
                        bool IsC1God = bool.Parse(dtMem.Rows[0]["IsC1God"].ToString());

                        if ((IsC112 && IsC134) || IsC1God)
                        {
                            Google_Sheet_Api = null;
                            Google_Sheet_Api = new GoogleSheetApi("1bKwnh_2XTYvR1bezOnzKEeA66Kyxlj0WAsN3LcL3FBs", "C2報名");
                            AddSubSignProcess();
                            Api_Data.ApiMsg = "C2 課程報名成功";

                        }
                        else
                        {
                            Api_Data.IsApiError = true;
                            Api_Data.ApiMsg = "您沒有符合上 C2 的資格，請點連結自行查詢是否完成 C1 課程";
                            Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                        }


                    }
                    else
                    {
                        Api_Data.IsApiError = true;
                        Api_Data.ApiMsg = "您沒有符合上 C2 的資格，請點連結自行查詢是否完成 C1 課程";
                        Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                    }

                }


            }
            catch (Exception ex)
            {
                Api_Data.IsApiError = true;
                Api_Data.ApiMsg = "請確認網路是否斷線或填寫的資料內容有誤";
            }
        }

        private void AddSubSignProcess()
        {
            //取出資料變更的訊息
            string Memo = string.Empty;
            if (Api_Data.DataChangeMsg != null && Api_Data.DataChangeMsg.Count > 0)
            {
                foreach (string s in Api_Data.DataChangeMsg)
                {
                    Memo += s + ",";
                }
            }

            DataTable dtSub = Ado_Info.SubjectInfo_ADO.GetSubjectDateBySubjectInfo(Api_Data.SID);
            foreach (DataRow drSub in dtSub.Rows)
            {
                Api_Data.SubDate += DateTime.Parse(drSub["SDate"].ToString()).Month.ToString("00") + "/" + DateTime.Parse(drSub["SDate"].ToString()).Day.ToString("00") + ",";
            }


            if (Api_Data.MID.Split(',').Count() > 1 && Api_Data.MID.Split(',')[1] != "")
            { //UPDATE 報名資訊

                //api.MID.Split(',')[0] 為會友MID流水編號
                //陣列索引從 1 開始
                for (int i = 1; i < Api_Data.MID.Split(',').Count(); i++)
                {
                    Ado_Info.ChcMemberSub_Temp_ADO.UpdChcMemberSub_TempByNo(Api_Info.GroupCName, Api_Info.GroupName, Api_Info.GroupClass, Api_Data.Ename, Api_Data.Phone,
                                                                                         Api_Data.Gmail, Memo, Api_Data.MID.Split(',')[i]);
                }
            }
            else
            { //INSERT 報名資訊


                foreach (DataRow drSub in dtSub.Rows)
                {
                    Ado_Info.ChcMemberSub_Temp_ADO.InsChcMemberSub_Temp_2(
                        Api_Data.SID, drSub["CategoryID2"].ToString(), Api_Info.GroupCName, Api_Info.GroupName, Api_Info.GroupClass,
                        Api_Data.Ename, Api_Data.Phone, Api_Data.Gmail, Api_Data.Church, "0", DateTime.Parse(drSub["SDate"].ToString()), Memo, Api_Data.MID.Replace(",", ""), "0");
                    
                }

            }
            
            //去尾「,」號
            if (Api_Data.SubDate != "" && Api_Data.SubDate != null)
            {
                Api_Data.SubDate = Api_Data.SubDate.Substring(0, Api_Data.SubDate.Length - 1);
            }

            //google Excel 組別分類
            switch (Api_Data.gcroup)
            {
                case "家庭組弟兄":
                    Api_Data.group_1 = Api_Data.group;
                    Api_Data.group_2 = "";
                    Api_Data.group_3 = "";
                    Api_Data.group_4 = "";
                    break;
                case "家庭組姊妹":
                    Api_Data.group_1 = "";
                    Api_Data.group_2 = Api_Data.group;
                    Api_Data.group_3 = "";
                    Api_Data.group_4 = "";
                    break;
                case "社青":
                    Api_Data.group_1 = "";
                    Api_Data.group_2 = "";
                    Api_Data.group_3 = Api_Data.group;
                    Api_Data.group_4 = "";
                    break;
                case "學生":
                    Api_Data.group_1 = "";
                    Api_Data.group_2 = "";
                    Api_Data.group_3 = "";
                    Api_Data.group_4 = Api_Data.group;
                    break;
            }

            //AddDataByV4Sheets();

            Google_Sheet_Api.AddDataByV4Sheets(
                new List<object>() {
                                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                Api_Data.Ename,
                                Api_Data.group_1,
                                Api_Data.group_2,
                                Api_Data.group_3,
                                Api_Data.group_4,
                                Api_Data.SubDate
                            }
            );

        }


    }
}