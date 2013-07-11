using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.DAL.DAO
{
    public class BaseDAO
    {
        /// <summary>
        /// 获取所有的用户类型
        /// </summary>
        /// <returns></returns>
        public List<UserType> GetAllUserType()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.UserType.ToList();
            }
        }

        /// <summary>
        /// 获取所有学院
        /// </summary>
        /// <returns></returns>
        public List<Institute> GetAllInstitute()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.Institute.ToList();
            }
        }

        /// <summary>
        /// 获取所有专业
        /// </summary>
        /// <returns></returns>
        public List<Major> GetAllMajor()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.Major.ToList();
            }
        }


    }
}
