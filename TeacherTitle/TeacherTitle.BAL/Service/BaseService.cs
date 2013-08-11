using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.DAO;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.BAL.Service
{
    public class BaseService : IBaseService
    {
        BaseDAO baseDAO = new BaseDAO();

        /// <summary>
        /// 获取所有的用户类型
        /// </summary>
        /// <returns></returns>
        public List<UserType> GetAllUserType()
        {
            return baseDAO.GetAllUserType();
        }

        /// <summary>
        /// 获取所有学院
        /// </summary>
        /// <returns></returns>
        public List<Institute> GetAllInstitute()
        {
            return baseDAO.GetAllInstitute();
        }

        /// <summary>
        /// 获取所有系
        /// </summary>
        /// <returns></returns>
        public List<Department> GetAllDepartment()
        {
            return baseDAO.GetAllDepartment();
        }


        /// <summary>
        /// 设置系统邮箱
        /// </summary>
        /// <param name="sysConfig"></param>
        /// <returns></returns>
        public ArgsHelper SetSysMail(SysConfig sysConfig)
        {
            return baseDAO.SetSysMail(sysConfig);
        }

        /// <summary>
        /// 获取系统邮箱配置
        /// </summary>
        /// <returns></returns>
        public SysConfig GetMailConfig()
        {
            return baseDAO.GetMailConfig();
        }

    }
}
