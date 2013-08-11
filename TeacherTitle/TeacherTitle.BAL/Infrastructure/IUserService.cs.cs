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
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        UserModel LogOn(string Account, string PassWord);

        /// <summary>
        /// 根据用户ID查找用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        UserModel GetUserByID(int ID);

        /// <summary>
        /// 根据关键字获取用户
        /// </summary>
        /// <returns></returns>
        Users[] GetUsers(string keyword);

        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <returns></returns>
        Users[] GetAllUsers();

        /// <summary>
        /// 获取所有用户名
        /// </summary>
        /// <returns></returns>
        List<KeyValueModel> GetAllUserName();

        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        ArgsHelper AddUser(Users users);

        /// <summary>
        /// 导入教师信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        ArgsHelper AddTeachers(string fileName);

        /// <summary>
        /// 确认邮箱是否存在
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        ArgsHelper ForgetPassword(string Mail);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        ArgsHelper ResetPassword(string Mail);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        ArgsHelper ChangePassword(int U_Code, string OldPassword, string NewPassword);

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        ArgsHelper ChangeUserInfo(int U_Code, string U_LongPhone, string U_ShortPhone, string U_Mail, string U_QQNum);

        /// <summary>
        /// 获取所有的教师
        /// </summary>
        /// <returns></returns>
        TeacherDetailModels[] GetTeacherDetail();
    }
}
