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

        /// <summary>
        /// 获取所有的教学活动
        /// </summary>
        /// <returns></returns>
        public TeachingActivity[] GetAllActivity()
        {
            return activityDAO.GetAllActivity();
        }

        /// <summary>
        /// 获取所有的教学形式
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> GetAllActForm()
        {
            var act = GetAllActivity();
            List<KeyValueModel> result = new List<KeyValueModel>();
            for (int i = 0; i < act.Length; i++)
            {
                result.Add(new KeyValueModel
                {
                    Key = act[i].TA_Code.ToString(),
                    Value = act[i].TA_Form
                });
            }
            return result;
        }

        /// <summary>
        /// 获取校内的活动
        /// </summary>
        /// <returns></returns>
        public TeachingActivity[] GetInActivity()
        {
            return activityDAO.GetInActivity();
        }

        /// <summary>
        /// 获取校外的活动
        /// </summary>
        /// <returns></returns>
        public TeachingActivity[] GetOutActivity()
        {
            return activityDAO.GetOutActivity();
        }

        /// <summary>
        /// 教师活动报名
        /// </summary>
        /// <param name="activitySignUp"></param>
        /// <returns></returns>
        public ArgsHelper TeacherSignUp(ActivitySignUp activitySignUp)
        {
            return activityDAO.TeacherSignUp(activitySignUp);
        }

        /// <summary>
        /// 添加教学活动(详细)
        /// </summary>
        /// <returns></returns>
        public ArgsHelper AddActivityPlan(ActivityPlan activityPlan)
        {
            return activityDAO.AddActivityPlan(activityPlan);
        }

    }
}
