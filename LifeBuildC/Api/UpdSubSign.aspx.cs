/*
  
 用途：
   生命建造-課程報到

 流程：
   [View]SubjectCheck.aspx?id=c1 > [按鈕]報到 > [API]UpdSubSign

 API條件：
   1. C1 可以現場報到. (C1 有沒有報名都沒有差別)
   2. C2 沒有報名就不能上.
   3. C2 若有中央同工的權限可現場報名.
   
 */
using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using libLifeBuildC;
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
    public partial class UpdSubSign : System.Web.UI.Page
    {
        GoogleSheetApi Google_Sheet_Api;
        AdoInfo Ado_Info = new AdoInfo();
        ApiInfo Api_Info = new ApiInfo();
        ApiData.ApiUpdSubSign Api_Data = new ApiData.ApiUpdSubSign();

        protected void Page_Load(object sender, EventArgs e)
        {
            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    Api_Info.strJsonData = readStream.ReadToEnd();
                }
            }

            Api_Data = JsonConvert.DeserializeObject<ApiData.ApiUpdSubSign>(Api_Info.strJsonData);

            if (Api_Data != null)
            {
                ApiProcess();
            }

            Response.Write(JsonConvert.SerializeObject(Api_Data));
            Response.End();
        }

        private void ApiProcess()
        {
            try
            {
                Api_Info.GetGroupData(Api_Data.group, Api_Data.gcroup);
                switch ((Api_Data.CategoryID).ToUpper())
                {
                    case "C1":

                        Google_Sheet_Api = null;
                        Google_Sheet_Api = new GoogleSheetApi("1HCRBI2C_cVl0fH5576PEX7UULWsgcxx1sbYdRQ6FcF8", "C1報到");
                        UpdSubSignProcess();
                        Api_Data.ApiMsg = "C1 課程簽到成功";

                        break;
                    case "C2":

                        if (Api_Data.MID != "" && Ado_Info.ChcMemberSub_Temp_ADO.ChkChcMemberSub_TempByMID(int.Parse(Api_Data.MID), Api_Data.SID))
                        { //C2 不能現場報名

                            Google_Sheet_Api = null;
                            Google_Sheet_Api = new GoogleSheetApi("1bKwnh_2XTYvR1bezOnzKEeA66Kyxlj0WAsN3LcL3FBs", "C2報到");
                            UpdSubSignProcess();
                            Api_Data.ApiMsg = "C2 課程簽到成功";

                        }
                        else
                        {
                            Api_Data.IsApiError = true;
                            Api_Data.ApiMsg = "您沒有符合上 C2 的資格，請確認是否有完成 C2 報名";
                            Api_Data.GoLink = "http://changelifesys.org/MemSubQuery.aspx";
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Api_Data.IsApiError = true;
                Api_Data.ApiMsg = "請確認網路是否斷線或填寫的資料內容有誤";
            }
        }

        private void UpdSubSignProcess()
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

            string strMake = ""; //是否補課(是: 補課 ; 空值: 不用補課)
            if (Api_Data.MID.Split(',').Count() > 1 && Api_Data.MID.Split(',')[1] != "")
            { //UPDATE 報名資訊

                //INSERT LOG
                DataTable dtMem = Ado_Info.ChcMember_ADO.QueryChcMemberByMID(Api_Data.MID.Split(',')[0]);
                if (dtMem != null && dtMem.Rows.Count > 0)
                {
                    Ado_Info.ChcMember_Log_ADO.InsLogDataByChcMember_Log
                    (
                        dtMem.Rows[0]["MID"].ToString(),
                        dtMem.Rows[0]["GroupName"].ToString(),
                        dtMem.Rows[0]["GroupName"].ToString(),
                        dtMem.Rows[0]["GroupClass"].ToString(),
                        dtMem.Rows[0]["Ename"].ToString(),
                        dtMem.Rows[0]["Phone"].ToString(),
                        dtMem.Rows[0]["Gmail"].ToString(),
                        dtMem.Rows[0]["Church"].ToString(),
                        dtMem.Rows[0]["C1_Status"].ToString(),
                        dtMem.Rows[0]["C2_Status"].ToString(),
                        bool.Parse(dtMem.Rows[0]["IsC112"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC134"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC212"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC234"].ToString()),
                        bool.Parse(dtMem.Rows[0]["IsC25"].ToString()),
                        int.Parse(dtMem.Rows[0]["C1_Score"].ToString()),
                        int.Parse(dtMem.Rows[0]["C212_Score"].ToString()),
                        int.Parse(dtMem.Rows[0]["C234_Score"].ToString()),
                        dtMem.Rows[0]["witness"].ToString(),
                        bool.Parse(dtMem.Rows[0]["Iswitness"].ToString()),
                        dtMem.Rows[0]["Memo"].ToString()
                    );


                    DataTable dtMake = Ado_Info.ChcMemberSub_Temp_ADO.CheckMakeByChcMemberSub_Temp(Api_Data.MID, Api_Data.SID);
                    if (dtMake != null && dtMake.Rows.Count > 0)
                    {
                        switch (dtMake.Rows[0]["CategoryID"].ToString())
                        {
                            case "C112":

                                if (bool.Parse(dtMem.Rows[0]["IsC112"].ToString()) == false)
                                {
                                    strMake = "補課";
                                }

                                break;
                            case "C134":

                                if (bool.Parse(dtMem.Rows[0]["IsC134"].ToString()) == false)
                                {
                                    strMake = "補課";
                                }

                                break;
                            case "C212":

                                if (bool.Parse(dtMem.Rows[0]["IsC212"].ToString()) == false)
                                {
                                    strMake = "補課";
                                }

                                break;
                            case "C234":

                                if (bool.Parse(dtMem.Rows[0]["IsC234"].ToString()) == false)
                                {
                                    strMake = "補課";
                                }

                                break;
                            case "C25":

                                if (bool.Parse(dtMem.Rows[0]["IsC25"].ToString()) == false)
                                {
                                    strMake = "補課";
                                }

                                break;
                        }
                    }
                    else
                    {

                    }



                }

                //api.MID.Split(',')[0] 為會友MID流水編號
                //api.MID.Split(',')[i] 大於0以上為No流水號
                //故陣列索引從 1 開始
                for (int i = 1; i < Api_Data.MID.Split(',').Count(); i++)
                {
                    Ado_Info.ChcMemberSub_Temp_ADO.UpdChcMemberSub_TempByUpdSubSign
                    (
                        Api_Info.GroupCName, 
                        Api_Info.GroupName, 
                        Api_Info.GroupClass, 
                        Api_Data.Ename, 
                        Api_Data.Phone,
                        Api_Data.Gmail, 
                        Memo, 
                        Api_Data.MID.Split(',')[i] //No
                    );
                }
            }
            else
            { //INSERT 報名資訊, 只有C1才能現場報名

                DataTable dtSub = Ado_Info.SubjectInfo_ADO.GetSDateBySubjectJoin(Api_Data.SID, DateTime.Now.ToString("yyyy/MM/dd"));
                if (dtSub != null && dtSub.Rows.Count > 0)
                {
                    Ado_Info.ChcMemberSub_Temp_ADO.InsChcMemberSub_TempByUpdSubSignToC1
                    (
                        Api_Data.SID, 
                        Api_Data.CategoryID, 
                        Api_Info.GroupCName, 
                        Api_Info.GroupName, 
                        Api_Info.GroupClass,
                        Api_Data.Ename, 
                        Api_Data.Phone, 
                        Api_Data.Gmail, 
                        Api_Data.Church, 
                        "1", //EStatus
                        DateTime.Now.ToString("yyyy/MM/dd"), //SubDate
                        Memo, 
                        Api_Data.MID.Replace(",", "") //MID為空值
                    );
                }

            }

            Api_Data.SubDate = DateTime.Now.ToString("MM/dd");

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

            Google_Sheet_Api.AddDataByV4Sheets(
                new List<object>() {
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    Api_Data.Ename,
                    Api_Data.group_1,
                    Api_Data.group_2,
                    Api_Data.group_3,
                    Api_Data.group_4,
                    Api_Data.SubDate,
                    strMake
                }
            );

        }



    }



}