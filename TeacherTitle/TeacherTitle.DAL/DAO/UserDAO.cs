using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.DAL.DAO
{
    public class UserDAO
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        public bool LogOn(string Account, string PassWord)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                if (db.User.FirstOrDefault(x => x.U_Account == Account && x.U_PassWord == PassWord) != null)
                    return true;
                else
                    return false;
            }
        }

    }
}
