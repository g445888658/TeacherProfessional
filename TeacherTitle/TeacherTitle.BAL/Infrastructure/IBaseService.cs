using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IBaseService
    {
        /// <summary>
        /// 获取所有的用户类型
        /// </summary>
        /// <returns></returns>
        List<UserType> GetAllUserType();

        /// <summary>
        /// 获取所有学院
        /// </summary>
        /// <returns></returns>
        List<Institute> GetAllInstitute();

        /// <summary>
        /// 获取所有系
        /// </summary>
        /// <returns></returns>
        List<Department> GetAllDepartment();

        /// <summary>
        /// 设置系统邮箱
        /// </summary>
        /// <param name="sysConfig"></param>
        /// <returns></returns>
        ArgsHelper SetSysMail(SysConfig sysConfig);

        /// <summary>
        /// 获取系统邮箱配置
        /// </summary>
        /// <returns></returns>
        SysConfig GetMailConfig();

        /// <summary>
        /// 获取所有的学年
        /// </summary>
        /// <returns></returns>
        List<String> GetAllSchoolYear();
    }
}
