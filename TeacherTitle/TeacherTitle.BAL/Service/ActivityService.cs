using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.DAO;

namespace TeacherTitle.BAL.Service
{
    public class ActivityService : IActivityService
    {
        ActivityDAO activityDAO = new ActivityDAO();

        /// <summary>
        /// 添加教学活动
        /// </summary>
        /// <param name="teachingActivity">活动</param>
        /// <returns></returns>
        public ArgsHelper AddActivity(TeachingActivity teachingActivity)
        {
            return activityDAO.AddActivity(teachingActivity);
        }
    }
}
