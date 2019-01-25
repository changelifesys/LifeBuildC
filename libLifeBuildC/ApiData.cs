using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLifeBuildC
{
    public class ApiData
    {
        /// <summary>
        /// 課程資訊
        /// </summary>
        public class GetSUBInfo
        {
            /// <summary>
            /// 課程類別
            /// </summary>
            public string CategoryID { get; set; }
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID { get; set; }
            /// <summary>
            /// 課程名稱
            /// </summary>
            public string SubName { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg = string.Empty;
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError = false;
            /// <summary>
            /// HTML 上課資訊
            /// </summary>
            public string HtmlSubDesc { get; set; }
            /// <summary>
            /// 傳入的頁面名稱
            /// </summary>
            public string PageName = string.Empty;
        }
        /// <summary>
        /// 課程報到
        /// </summary>
        public class AddSubSign
        {
            /// <summary>
            /// 資料變更訊息
            /// </summary>
            public List<string> DataChangeMsg = new List<string>();
            /// <summary>
            /// 會友流水號
            /// </summary>
            public string MID { get; set; }
            /// <summary>
            ///  課程類別
            /// </summary>
            public string CategoryID { get; set; }
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID { get; set; }
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup { get; set; }
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 所屬牧區/小組：家庭弟兄
            /// </summary>
            public string group_1 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：家庭姊妹
            /// </summary>
            public string group_2 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：社青
            /// </summary>
            public string group_3 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：學青
            /// </summary>
            public string group_4 { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// Mail
            /// </summary>
            public string Gmail { get; set; }
            /// <summary>
            /// 所屬教會
            /// </summary>
            public string Church { get; set; }
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError = false;
            /// <summary>
            /// 上課時段
            /// </summary>
            public string SubDate { get; set; }
            /// <summary>
            /// 導向的連結
            /// </summary>
            public string GoLink = string.Empty;
        }
        /// <summary>
        /// 課程簽到
        /// </summary>
        public class ApiUpdSubSign
        {
            /// <summary>
            /// 資料變更訊息
            /// </summary>
            public List<string> DataChangeMsg = new List<string>();
            /// <summary>
            /// 會友號
            /// </summary>
            public string MID = string.Empty;
            /// <summary>
            ///  課程類別
            /// </summary>
            public string CategoryID { get; set; }
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID { get; set; }
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup { get; set; }
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 所屬牧區/小組：家庭弟兄
            /// </summary>
            public string group_1 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：家庭姊妹
            /// </summary>
            public string group_2 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：社青
            /// </summary>
            public string group_3 { get; set; }
            /// <summary>
            /// 所屬牧區/小組：學青
            /// </summary>
            public string group_4 { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// Mail
            /// </summary>
            public string Gmail = string.Empty;
            /// <summary>
            /// 所屬教會
            /// </summary>
            public string Church = string.Empty;
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError { get; set; }
            /// <summary>
            /// 上課時段
            /// </summary>
            public string SubDate = string.Empty;
            /// <summary>
            /// 導向的連結
            /// </summary>
            public string GoLink = string.Empty;
        }
        /// <summary>
        /// 檢查會友資料變更狀況
        /// </summary>
        public class ChkMember
        {
            /// <summary>
            /// 資料變更訊息
            /// </summary>
            public List<string> DataChangeMsg = new List<string>();
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 會友號
            /// </summary>
            public string MID = string.Empty;
            /// <summary>
            /// API Title
            /// </summary>
            public string ApiTitle { get; set; }
            /// <summary>
            /// 是否要秀出資料變更訊息(true: 要秀出來; false: 不要秀出來)
            /// </summary>
            public bool IsChgShow = false;
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID { get; set; }
            /// <summary>
            /// 報名表流水號
            /// </summary>
            public List<string> No = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        public class ChkUserInfo
        {
            /// <summary>
            /// 資料變更訊息
            /// </summary>
            public List<string> DataChangeMsg = new List<string>();
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 會友號
            /// </summary>
            public string MID = string.Empty;
            /// <summary>
            /// API Title
            /// </summary>
            public string ApiTitle { get; set; }
            /// <summary>
            /// 是否要秀出資料變更訊息(true: 要秀出來; false: 不要秀出來)
            /// </summary>
            public bool IsChgShow = false;
            /// <summary>
            /// 手機機碼
            /// </summary>
            public string UUID = string.Empty;
        }
        /// <summary>
        /// 個人上課查詢
        /// </summary>
        public class ApiGetMemSubData
        {
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup { get; set; }
            /// <summary>
            /// 小組
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename { get; set; }
            /// <summary>
            /// C1 一、二課(True: 通過 ; False: 沒通過)
            /// </summary>
            public bool IsC112 = false;
            /// <summary>
            /// C1 一、二課狀態訊息
            /// </summary>
            public string TxtC112 = "查無上課資料";
            /// <summary>
            /// C1 三、四課(True: 通過 ; False: 沒通過)
            /// </summary>
            public bool IsC134 = false;
            /// <summary>
            /// C1 三、四課狀態訊息
            /// </summary>
            public string TxtC134 = "查無上課資料";
            /// <summary>
            /// C1 更深經歷神(True: 通過 ; False: 沒通過)
            /// </summary>
            public bool IsC1God = false;
            /// <summary>
            /// C1 更深經歷神狀態訊息
            /// </summary>
            public string TxtC1God = "查無上課資料";
            public bool IsExpGod = false;
            public string TxtIsExpGod = "上課資料匯入中...";
            /// <summary>
            /// C2 一、二課(True: 通過 ; False: 沒通過)
            /// </summary>
            public bool IsC212 = false;
            /// <summary>
            /// C2 一、二課狀態訊息
            /// </summary>
            public string TxtIsC212 = "查無上課資料";
            /// <summary>
            /// C2 三、四課(True: 通過 ; False: 沒通過)
            /// </summary>
            public bool IsC234 = false;
            /// <summary>
            /// C2 三、四課狀態訊息
            /// </summary>
            public string TxtIsC234 = "查無上課資料";
            /// <summary>
            /// C2 第五課(True: 通過 ; False: 沒通過)
            /// </summary>
            public bool IsC25 = false;
            /// <summary>
            /// C2 第五課狀態訊息
            /// </summary>
            public string TxtIsC25 = "查無上課資料";
            public bool IsC2QT = false;
            public string TxtIsC2QT = "上課資料匯入中...";
            public bool IsC2MW = false;
            public string TxtIsC2MW = "上課資料匯入中...";
            /// <summary>
            /// C2 領袖訓練一(True: 通過 ; False: 沒通過)
            /// </summary>
            public bool IsC2L1 = false;
            /// <summary>
            /// C2 領袖訓練一狀態訊息
            /// </summary>
            public string TxtC2L1 = "查無上課資料";
            public bool IsC3N = false;
            public string TxtIsC3N = "上課資料匯入中...";
            public bool IsC3P = false;
            public string TxtIsC3P = "上課資料匯入中...";
            /// <summary>
            /// 見證繳交(True: 有繳交 ; False: 沒有繳交)
            /// </summary>
            public bool Iswitness = false;
            /// <summary>
            /// 見證繳交狀態訊息
            /// </summary>
            public string Txtwitness = "查無繳交見證";
            /// <summary>
            /// C1 考試成績
            /// </summary>
            public bool IsC1_Score = false;
            /// <summary>
            /// C1 考試成績狀態訊息
            /// </summary>
            public string TxtC1_Score = "查無考試成績";
            /// <summary>
            /// C2 一、二課考試成績
            /// </summary>
            public bool IsC212_Score = false;
            /// <summary>
            /// C2 一、二課考試成績狀態訊息
            /// </summary>
            public string TxtC212_Score = "查無考試成績";
            /// <summary>
            /// C2 三、四課考試成績
            /// </summary>
            public bool IsC234_Score = false;
            /// <summary>
            /// C2 三、四課考試成績狀態訊息
            /// </summary>
            public string TxtC234_Score = "查無考試成績";
            /// <summary>
            /// C1 通過狀態(True: 有通過 ; False: 沒有通過)
            /// </summary>
            public bool IsC1_Status = false;
            /// <summary>
            /// C1 通過狀態狀態訊息
            /// </summary>
            public string TxtC1_Status = "C1 判定不通過";
            /// <summary>
            /// C2 通過狀態(True: 有通過 ; False: 沒有通過)
            /// </summary>
            public bool IsC2_Status = false;
            /// <summary>
            /// C2 通過狀態狀態訊息
            /// </summary>
            public string TxtC2_Status = "C2 判定不通過";
            public bool IsC3_Status = false;
            public string TxtIsC3_Status = "C3 資料匯入中...";
            /// <summary>
            /// API 訊息
            /// </summary>
            public string ApiMsg { get; set; }
            /// <summary>
            /// API 有錯(true: 有錯; false: 沒有錯)
            /// </summary>
            public bool IsApiError = false;
        }
        /// <summary>
        /// 主要資料設定
        /// </summary>
        public class SetUserInfo
        {
            /// <summary>
            /// 會友流水號
            /// </summary>
            public string MID = string.Empty;
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup = string.Empty;
            /// <summary>
            /// 小組
            /// </summary>
            public string group = string.Empty;
            /// <summary>
            /// 姓名
            /// </summary>
            public string Ename = string.Empty;
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone = string.Empty;
            /// <summary>
            /// Email
            /// </summary>
            public string Gmail = string.Empty;
            /// <summary>
            /// 奉獻編號
            /// </summary>
            public string TithingNo = string.Empty;
            /// <summary>
            /// 手機機碼
            /// </summary>
            public string UUID = string.Empty;
            /// <summary>
            /// 訊息的顯示文字
            /// </summary>
            public string ApiMsg = string.Empty;
            /// <summary>
            /// Default : IsApiError = false 表示儲存成功, 沒有重大錯誤訊息
            /// </summary>
            public bool IsApiError = false;
        }
    }
}
