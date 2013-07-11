using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.DAO;

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
        /// 获取所有专业
        /// </summary>
        /// <returns></returns>
        public List<Major> GetAllMajor()
        {
            return baseDAO.GetAllMajor();
        }
    }
}
