using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.DAL.DAO
{
    public class ClassHourDAO
    {
        /// <summary>
        /// 授予老师学时
        /// </summary>
        /// <param name="classHourSum"></param>
        /// <returns></returns>
        public ArgsHelper AddClassHourSum(ClassHourSum classHourSum)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                try
                {
                    db.ClassHourSum.AddObject(classHourSum);
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
        /// 根据ASU_Code查询教师获取的学时
        /// </summary>
        /// <returns></returns>
        public ClassHourSum GetClaHourInfoByASU_Code(int ASUCode)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ClassHourSum.FirstOrDefault(x => x.ASU_Code == ASUCode);
            }

        }

        /// <summary>
        /// 根据报名编号修改获得的学时
        /// </summary>
        /// <param name="ASUCode"></param>
        /// <returns></returns>
        public ArgsHelper AlterClaHourInfo(int ASUCode, int ClassHour)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ClassHourSum.FirstOrDefault(x => x.ASU_Code == ASUCode);
                try
                {
                    result.CH_GetHour = ClassHour;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message.ToString());
                }
                return new ArgsHelper();
            }
        }





    }
}
