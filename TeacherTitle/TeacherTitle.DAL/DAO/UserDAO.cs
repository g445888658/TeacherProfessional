using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Model;
using TeacherTitle.DAL.Infrastructure;
using System.Data.Objects;

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
                        U_Department = new KeyValueModel
                        {
                            Key = Convert.ToString(user.Department),
                            Value = user.Department.D_Name
                        },
                        U_Type = new KeyValueModel
                        {
                            Key = Convert.ToString(user.UT_Code),
                            Value = user.UserType.UTType
                        },
                        U_Title = user.U_Title,
                        U_Mail = user.U_Mail,
                        U_LongPhone = user.U_LongPhone,
                        U_Remark = user.U_Remark
                    };

                    return userModel;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 根据用户ID查找用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public UserModel GetUserByID(int ID)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var user = db.Users.FirstOrDefault(x => x.U_Code == ID);
                if (user != null)
                {
                    UserModel userModel = new UserModel()
                    {
                        U_Code = user.U_Code,
                        U_Account = user.U_Account,
                        U_PassWord = user.U_PassWord,
                        U_Name = user.U_Name,
                        U_Sex = new KeyValueModel()
                        {
                            Key = user.U_Sex.ToString(),
                            Value = user.U_Sex == 1 ? "男" : "女"
                        },
                        U_Birth = user.U_Birth,
                        U_Nation = user.U_Nation,
                        U_Institute = new KeyValueModel()
                        {
                            Key = Convert.ToString(user.I_Code),
                            Value = user.Institute.I_Name
                        },
                        U_Department = new KeyValueModel
                        {
                            Key = Convert.ToString(user.D_Code),
                            Value = user.Department.D_Name
                        },
                        U_SchoolTime = user.U_SchoolTime,
                        U_WorkTime = user.U_WorkTime,
                        U_Subject = user.U_Subject,
                        U_Major = user.U_Major,
                        U_Research = user.U_Research,
                        U_Title = user.U_Title,
                        U_TitleLevel = user.U_TitleLevel,
                        U_EngageTime = user.U_EngageTime,
                        U_IsDoubleTitle = user.U_IsDoubleTitle.ToString(),
                        U_LongPhone = user.U_LongPhone,
                        U_ShortPhone = user.U_ShortPhone,
                        U_Mail = user.U_Mail,
                        U_QQNum = user.U_QQNum,
                        U_Type = new KeyValueModel
                        {
                            Key = Convert.ToString(user.UT_Code),
                            Value = user.UserType.UTType
                        },
                        U_Remark = user.U_Remark
                    };

                    return userModel;
                }
                else
                    return null;
            }
        }

        ///// <summary>
        ///// 根据关键字获取用户
        ///// </summary>
        ///// <returns></returns>
        //public Users[] GetUsers(string keyword)
        //{
        //    using (TTitleDBEntities db = new TTitleDBEntities())
        //    {
        //        return db.Users.Where(x => x.U_Name.Contains(keyword)).ToArray();
        //    }
        //}

        /// <summary>
        /// 获取所有的用户
        /// </summary>
        /// <returns></returns>
        public Users[] GetAllUsers()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.Users.OrderBy(x => x.U_Code).ToArray();
            }
        }



        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public ArgsHelper AddUser(Users users)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                try
                {
                    db.Users.AddObject(users);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message.ToString());
                }
            }
            return new ArgsHelper(true, "添加成功");
        }

        /// <summary>
        /// 确认邮箱是否存在
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        public ArgsHelper ForgetPassword(string Mail)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                if (db.Users.FirstOrDefault(x => x.U_Mail == Mail) == null)
                    return new ArgsHelper("该邮箱不存在");
            }
            return new ArgsHelper();
        }

        /// <summary>
        /// 确认邮箱是否存在
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        public ArgsHelper ResetPassword(string Mail)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var user = db.Users.FirstOrDefault(x => x.U_Mail == Mail);
                user.U_PassWord = user.U_Account;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message.ToString());
                }
            }
            return new ArgsHelper();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ArgsHelper ChangePassword(int U_Code, string OldPassword, string NewPassword)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var user = db.Users.FirstOrDefault(x => x.U_Code == U_Code && x.U_PassWord == OldPassword);
                if (user == null)
                    return new ArgsHelper("原密码错误");
                user.U_PassWord = NewPassword;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message.ToString());
                }
            }
            return new ArgsHelper(true, "修改密码成功");
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public ArgsHelper ChangeUserInfo(int U_Code, string U_LongPhone, string U_ShortPhone, string U_Mail, string U_QQNum)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var user = db.Users.FirstOrDefault(x => x.U_Code == U_Code);

                user.U_LongPhone = U_LongPhone;
                user.U_ShortPhone = U_ShortPhone;
                user.U_QQNum = U_QQNum;
                //user.U_Mail = U_Mail;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message.ToString());
                }
            }
            return new ArgsHelper(true, "修改成功");
        }

        /// <summary>
        /// 获取所有教师
        /// </summary>
        /// <returns></returns>
        public TeacherDetailModels[] GetTeacherDetail()
        {
            List<TeacherDetailModels> list = new List<TeacherDetailModels>();
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var Institute = db.Institute.OrderBy(x => x.I_Code).ToArray();
                for (int i = 0; i < Institute.Length; i++)
                {
                    var icode = Institute[i].I_Code;
                    var teacher = db.Users.Where(x => x.UT_Code == 2 && x.I_Code == icode).ToArray();
                    var name = teacher.Select(x => new KeyValueModel()
                    {
                        Key = x.U_Code.ToString(),
                        Value = x.U_Name
                    }).ToArray();
                    list.Add(new TeacherDetailModels()
                    {
                        institute = new KeyValueModel()
                        {
                            Key = Institute[i].I_Code.ToString(),
                            Value = Institute[i].I_Name
                        },
                        TeacherName = name
                    });
                }
            }
            return list.ToArray();
        }




    }
}
