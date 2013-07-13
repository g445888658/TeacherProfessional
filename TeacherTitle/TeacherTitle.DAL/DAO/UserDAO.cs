using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Model;
using TeacherTitle.DAL.Infrastructure;

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
        public UserModel LogOn(string Account, string PassWord)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {

                var user = db.Users.FirstOrDefault(x => x.U_Account == Account && x.U_PassWord == PassWord);
                if (user != null)
                {
                    UserModel userModel = new UserModel()
                    {
                        U_Code = user.U_Code,
                        U_Account = user.U_Account,
                        U_PassWord = user.U_PassWord,
                        U_Name = user.U_Name,
                        U_Institute = new KeyValueModel()
                        {
                            Key = Convert.ToString(user.I_Code),
                            Value = user.Institute.I_Name
                        },
                        U_Major = new KeyValueModel
                        {
                            Key = Convert.ToString(user.M_Code),
                            Value = user.Major.M_Name
                        },
                        U_Type = new KeyValueModel
                        {
                            Key = Convert.ToString(user.UT_Code),
                            Value = user.UserType.UTType
                        },
                        U_Degree = user.U_Degree,
                        U_Title = user.U_Title,
                        U_Mail = user.U_Mail,
                        U_Phone = user.U_Phone,
                        U_Remark = user.U_Remark
                    };

                    return userModel;
                }
                else
                    return null;
            }
        }

    }
}
