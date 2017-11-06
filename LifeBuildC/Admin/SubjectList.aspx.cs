using ADO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LifeBuildC.Admin
{
    public partial class SubjectList : System.Web.UI.Page
    {
        //上課日期
        SubjectDateADO SubjectDate = new SubjectDateADO();
        //報名資訊
        SubSignInfoADO SubSignInfo = new SubSignInfoADO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C1");
                //gvSubject.DataBind();

                HttpCookie mycookie = Request.Cookies["CategoryID"];
                if (mycookie == null)
                {
                    gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C1");
                    gvSubject.DataBind();
                }
                else
                {
                    gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate(mycookie.Value);
                    gvSubject.DataBind();
                }
            }
        }

        protected void gvSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //滑鼠移入移出效果
            if (e.Row.RowType == DataControlRowType.DataRow &&
                ((e.Row.RowState & DataControlRowState.Edit) <= 0))
            {
                switch (DataBinder.Eval(e.Row.DataItem, "CategoryID").ToString())
                {
                    case "C112":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C1 一、二課";
                        break;
                    case "C134":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C1 三、四課";
                        break;
                    case "C212":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C2 一、二課";
                        break;
                    case "C234":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C2 三、四課";
                        break;
                    case "C25":
                        ((Label)e.Row.FindControl("lblCategoryID")).Text = "C2 五課";
                        break;
                }

                switch (DataBinder.Eval(e.Row.DataItem, "CategoryID").ToString())
                {
                    case "C112":
                    case "C134":
                        ((LinkButton)e.Row.FindControl("btnEdit")).PostBackUrl = "SubjectC1Edit.aspx?id=" +
                        DataBinder.Eval(e.Row.DataItem, "SID").ToString();
                        break;

                    case "C212":
                    case "C234":
                    case "C25":
                        ((LinkButton)e.Row.FindControl("btnEdit")).PostBackUrl = "SubjectC2Edit.aspx?id=" +
                        DataBinder.Eval(e.Row.DataItem, "SID").ToString();
                        break;
                }

                //報名時間
                ((Label)e.Row.FindControl("lblSubDate")).Text = DataBinder.Eval(e.Row.DataItem, "SubStrDate").ToString() + "~" + DataBinder.Eval(e.Row.DataItem, "SubEndDate").ToString();

                if (DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "SDate").ToString()) < DateTime.UtcNow.AddHours(8))
                {
                    ((LinkButton)e.Row.FindControl("btnEdit")).Visible = false; //隱藏編輯鈕
                    ((Button)e.Row.FindControl("btnDel")).Visible = false; //隱藏刪除鈕
                }


            }
        }

        protected void rdoC1List_CheckedChanged(object sender, EventArgs e)
        {
            gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C1");
            gvSubject.DataBind();
        }

        protected void rdoC2List_CheckedChanged(object sender, EventArgs e)
        {
            gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C2");
            gvSubject.DataBind();
        }

        protected void btnAddC1_Click(object sender, EventArgs e)
        {
            //HttpCookie mycookie = new HttpCookie("CategoryID");
            //mycookie.Value = "C1";
            //mycookie.Expires = DateTime.Now.AddHours(1);
            //Response.Cookies.Add(mycookie);
            Response.Redirect("SubjectC1Add.aspx");
        }

        protected void btnAddC2_Click(object sender, EventArgs e)
        {
            //HttpCookie mycookie = new HttpCookie("CategoryID");
            //mycookie.Value = "C2";
            //mycookie.Expires = DateTime.Now.AddHours(1);
            //Response.Cookies.Add(mycookie);
            Response.Redirect("SubjectC2Add.aspx");
        }

        protected void gvSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string SID = ((Button)gvSubject.Rows[e.RowIndex].FindControl("btnDel")).Attributes["DELID"].ToString();
            string CategoryID = ((Label)gvSubject.Rows[e.RowIndex].FindControl("lblCategoryID")).Attributes["CategoryID"].ToString();

            SubjectDate.DelSubjectDate(int.Parse(SID), CategoryID);

            if (rdoC1List.Checked)
            {
                gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C1");
            }
            else
            {
                gvSubject.DataSource = SubjectDate.QueryAllBySubjectDate("C2");
            }

            gvSubject.DataBind();
        }

        protected void gvSubject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GoogleExport")
            {
                GridViewRow gv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                //工作表名稱
                TextBox txt = (TextBox)gv.FindControl("txtSheet");

                //上課名單匯出
                LinkButton lk = (LinkButton)gv.FindControl("btnExport");

                int SID = int.Parse(lk.Attributes["SID"].ToString());
                DataTable dtSID = SubSignInfo.QuerySIDBySubSignInfo(SID);
                if (dtSID.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSID.Rows)
                    {
                        SendGoogleExcel(dr, txt.Text.Trim());
                    }
                }

            }
        }

        /// <summary>
        /// 匯出Google Excel
        /// </summary>
        private void SendGoogleExcel(DataRow dr, string sheet)
        {
            string[] Scopes = { SheetsService.Scope.Spreadsheets };

            //應用程式的名字需要英文
            string ApplicationName = "Get Google SheetData with Google Sheets API";

            UserCredential credential;

            var folder = System.Web.HttpContext.Current.Server.MapPath("/App_Data/MyGoogleStorage");

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
               new ClientSecrets
               {
                   ClientId = "117990626740-rptck4cro3bpbu3u7da3t4qlr20i3rsl.apps.googleusercontent.com",
                   ClientSecret = "zcFr6UCqdX-jo29QFogCcyf1"
               },
               Scopes,
               "user",
               CancellationToken.None,
               new FileDataStore(folder)).Result;

            // Create Google Sheets API service.

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            // Define request parameters.
            String spreadsheetId = "1-uHyjD9obfb45KN_zx2P2yWf5z8EsoFWZnf1v434vXY";

            String range = sheet;

            var valueRange = new ValueRange();

            //報名時間, 姓名, 小組, 狀態
            var oblist = new List<object>() {
                dr["SID"].ToString()
            };

            valueRange.Values = new List<IList<object>> { oblist };
            valueRange.MajorDimension = "Rows"; //Rows or Columns

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            try
            {
                var appendReponse = request.Execute();
            }
            catch {
                Response.Write("<script>alert('請輸入雲端匯出的工作表名稱');</script>");
            }

        }

    }
}