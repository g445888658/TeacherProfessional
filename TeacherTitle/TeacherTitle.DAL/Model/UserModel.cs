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
        /// 学院
        /// </summary>
        public KeyValueModel U_Institute { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public KeyValueModel U_Major { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public KeyValueModel U_Type { get; set; }

        /// <summary>
        /// 学历学位
        /// </summary>
        public string U_Degree { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public string U_Title { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string U_Mail { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string U_Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string U_Remark { get; set; }
    }
}
