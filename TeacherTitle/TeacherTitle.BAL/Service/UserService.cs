using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.DAL.DAO;
using TeacherTitle.DAL.Model;

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
    }
}
