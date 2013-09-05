using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.DAL.Model
{
    public class SignUpModels
    {
        /// <summary>
        /// 报名编号
        /// </summary>
        public string ASU_Code { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string U_Code { get; set; }

        /// <summary>
        /// 活动计划编号
        /// </summary>
        public string AP_Code { get; set; }

        /// <summary>
        /// 活动时间
        /// </summary>
        public string ASU_Time { get; set; }

        /// <summary>
        /// 是否候补
        /// </summary>
        public string ASU_IsCandidateKey { get; set; }

        /// <summary>
        /// 是否候补值
        /// </summary>
        public string ASU_IsCandidateVal { get; set; }

        /// <summary>
        /// 职工号
        /// </summary>
        public string U_Account { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string U_Name { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public string U_Title { get; set; }

        /// <summary>
        /// 学院编号
        /// </summary>
        public string I_Code { get; set; }

        /// <summary>
        /// 学院
        /// </summary>
        public string Institute { get; set; }

        /// <summary>
        /// 学时统计编号
        /// </summary>
        public string CH_Code { get; set; }

        /// <summary>
        /// 学时获得时间
        /// </summary>
        public string CH_GetTime { get; set; }

        /// <summary>
        /// 学时
        /// </summary>
        public string CH_GetHour { get; set; }

        /// <summary>
        /// 活动材料
        /// </summary>
        public ActivityMaterial[] ActivityMaterial { get; set; }

    }
}
