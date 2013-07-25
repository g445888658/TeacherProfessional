using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.DAL.DAO;
using TeacherTitle.DAL.Model;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.BAL.Service
{
    public class UserService : IUserService
    {
        UserDAO userDAO = new UserDAO();

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public UserModel LogOn(string Account, string PassWord)
        {
            return userDAO.LogOn(Account, PassWord);
        }

        /// <summary>
        /// 根据用户ID查找用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public UserModel GetUserByID(int ID)
        {
            return userDAO.GetUserByID(ID);
        }

        /// <summary>
        /// 根据关键字获取用户
        /// </summary>
        /// <returns></returns>
        public Users[] GetUsers(string keyword)
        {
            var users = userDAO.GetAllUsers();
            List<Users> list = new List<Users>();
            //因为linq方法体内写不了其他方法，所以为了让用户名中包含某个字母的也查出来，只能这样做了
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].U_Name.Contains(keyword) || GetSpelling.GetPYStr(users[i].U_Name).Contains(keyword))
                {
                    list.Add(users[i]);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <returns></returns>
        public Users[] GetAllUsers()
        {
            return userDAO.GetAllUsers();
        }

        /// <summary>
        /// 获取所有用户名
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> GetAllUserName()
        {
            var users = GetAllUsers();
            List<KeyValueModel> list = new List<KeyValueModel>();
            for (int i = 0; i < users.Length; i++)
            {
                list.Add(new KeyValueModel()
                {
                    Key = users[i].U_Code.ToString(),
                    Value = users[i].U_Name
                });
            }
            return list;
        }


        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public ArgsHelper AddUser(Users users)
        {
            return userDAO.AddUser(users);
        }

        /// <summary>
        /// 确认邮箱是否存在
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        public ArgsHelper ForgetPassword(string Mail)
        {
            return userDAO.ForgetPassword(Mail);
        }

        /// <summary>
        /// 确认邮箱是否存在
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        public ArgsHelper ResetPassword(string Mail)
        {
            return userDAO.ResetPassword(Mail);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ArgsHelper ChangePassword(int U_Code, string OldPassword, string NewPassword)
        {
            return userDAO.ChangePassword(U_Code, OldPassword, NewPassword);
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public ArgsHelper ChangeUserInfo(int U_Code, string Mail, string Phone)
        {
            return userDAO.ChangeUserInfo(U_Code, Mail, Phone);
        }


    }
}
