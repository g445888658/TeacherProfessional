﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;

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
        /// 获取所有系
        /// </summary>
        /// <returns></returns>
        public List<Department> GetAllDepartment()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.Department.ToList();
            }
        }

        /// <summary>
        /// 获取系统邮箱配置
        /// </summary>
        /// <returns></returns>
        public SysConfig GetMailConfig()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.SysConfig.FirstOrDefault(x => x.SC_FieldCode == 1);
            }
        }

        /// <summary>
        /// 设置系统邮箱
        /// </summary>
        /// <param name="sysConfig"></param>
        /// <returns></returns>
        public ArgsHelper SetSysMail(SysConfig sysConfig)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.SysConfig.FirstOrDefault(x => x.SC_FieldCode == 1);
                if (result != null)
                {
                    result.SC_FieldName = sysConfig.SC_FieldName;
                    result.SC_FieldAttr = sysConfig.SC_FieldAttr;
                    result.SC_FieldCode = sysConfig.SC_FieldCode;
                }
                else
                {
                    db.SysConfig.AddObject(sysConfig);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    return new ArgsHelper(ex.Message.ToString());
                }
                return new ArgsHelper(true, "设置成功");
            }
        }

        /// <summary>
        /// 获取所有的学年
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllSchoolYear()
        {
            List<string> list = new List<string>();
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ActivityPlan.AsParallel().Select(x => (new { Year = x.AP_SchoolYear })).Distinct().OrderBy(x => x.Year).ToList();

                for (int i = 0; i < result.Count; i++)
                    list.Add(result[i].Year);
            }
            return list;
        }


    }
}
