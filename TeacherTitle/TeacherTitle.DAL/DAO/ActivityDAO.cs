using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.DAL.DAO
{
    public class ActivityDAO
    {
        /// <summary>
        /// 添加教学活动
        /// </summary>
        /// <param name="teachingActivity"></param>
        /// <returns></returns>
        public ArgsHelper AddActivity(TeachingActivity teachingActivity)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                try
                {
                    db.TeachingActivity.AddObject(teachingActivity);
                }
                catch (Exception ex)
                {

                    return new ArgsHelper(ex.Message.ToString());
                }

            }
            return new ArgsHelper();
        }



    }
}
