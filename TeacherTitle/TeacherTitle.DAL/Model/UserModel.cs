using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.DAL.Model
{
    public class UserModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int U_Code { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string U_Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string U_PassWord { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string U_Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public KeyValueModel U_Sex { get; set; }

        ///<summary>
        /// 出生年月
        /// </summary>
        public string U_Birth { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string U_Nation { get; set; }

        /// <summary>
        /// 学院
        /// </summary>
        public KeyValueModel U_Institute { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public KeyValueModel U_Department { get; set; }

        /// <summary>
        /// 来校年月
        /// </summary>
        public string U_SchoolTime { get; set; }

        /// <summary>
        /// 参加工作年月
        /// </summary>
        public string U_WorkTime { get; set; }


        /// <summary>
        /// 现从事学科
        /// </summary>
        public string U_Subject { get; set; }

        /// <summary>
        /// 从事专业
        /// </summary>
        public string U_Major { get; set; }

        /// <summary>
        /// 研究方向
        /// </summary>
        public string U_Research { get; set; }

        /// <summary>
        /// 职称(专业技术资格)
        /// </summary>
        public string U_Title { get; set; }

        /// <summary>
        /// 资格级别
        /// </summary>
        public string U_TitleLevel { get; set; }

        /// <summary>
        /// 专业技术职务聘任时间
        /// </summary>
        public string U_EngageTime { get; set; }

        /// <summary>
        /// 是否双职称
        /// </summary>
        public string U_IsDoubleTitle { get; set; }

        /// <summary>
        /// 联系电话(长号)
        /// </summary>
        public string U_LongPhone { get; set; }
        /// <summary>
        /// 短号
        /// </summary>
        public string U_ShortPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string U_Mail { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        public string U_QQNum { get; set; }

        /// <summary>
        /// 权限类型编号
        /// </summary>
        public KeyValueModel U_Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string U_Remark { get; set; }
    }
}
