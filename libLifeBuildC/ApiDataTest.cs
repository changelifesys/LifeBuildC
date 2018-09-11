﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLifeBuildC
{
    public class ApiDataTest
    {
        /// <summary>
        /// 課程報到
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
            public string MID = "758";
            /// <summary>
            ///  課程類別
            /// </summary>
            public string CategoryID = "C1";
            /// <summary>
            /// 課程ID
            /// </summary>
            public int SID = 2;
            /// <summary>
            /// 組別
            /// </summary>
            public string gcroup = "社青";
            /// <summary>
            /// 小組
            /// </summary>
            public string group = "CA202.信豪牧區-彥伯小組";
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
            public string Ename = "劉彥伯";
            /// <summary>
            /// 手機
            /// </summary>
            public string Phone = "0919963322";
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
        }
    }
}
