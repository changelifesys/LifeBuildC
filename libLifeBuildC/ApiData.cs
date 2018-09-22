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
        /// 課程報到
        /// </summary>
        public class ApiAddSubSign
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
            public bool IsApiError { get; set; }
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
        public class ApiChkMember
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
        }
    }
}
