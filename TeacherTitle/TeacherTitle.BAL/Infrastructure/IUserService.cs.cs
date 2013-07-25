using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Model;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IUserService
    {
        UserModel LogOn(string Account, string PassWord);

        UserModel GetUserByID(int ID);

        Users[] GetUsers(string keyword);

        Users[] GetAllUsers();

        List<KeyValueModel> GetAllUserName();

        ArgsHelper AddUser(Users users);

        ArgsHelper ForgetPassword(string Mail);

        ArgsHelper ResetPassword(string Mail);

        ArgsHelper ChangePassword(int U_Code, string OldPassword, string NewPassword);

        ArgsHelper ChangeUserInfo(int U_Code, string Mail, string Phone);
    }
}
