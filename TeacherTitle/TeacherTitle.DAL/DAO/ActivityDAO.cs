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
        /// 获取所有的教学活动
        /// </summary>
        /// <returns></returns>
        public TeachingActivity[] GetAllActivity()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.TeachingActivity.OrderBy(x => x.TA_PlaceKey).ToArray();
            }
        }

        /// <summary>
        /// 获取校内的活动(0)
        /// </summary>
        /// <returns></returns>
        public TeachingActivity[] GetInActivity()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.TeachingActivity.Where(x => x.TA_PlaceKey == 0).OrderBy(x => x.TA_Code).ToArray();
            }
        }

        /// <summary>
        /// 获取校外的活动(1)
        /// </summary>
        /// <returns></returns>
        public TeachingActivity[] GetOutActivity()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.TeachingActivity.Where(x => x.TA_PlaceKey == 1).OrderBy(x => x.TA_Code).ToArray();
            }
        }


        /// <summary>
        /// 添加教学活动(详细)
        /// </summary>
        /// <returns></returns>
        public ArgsHelper AddActivityPlan(ActivityPlan activityPlan)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                try
                {
                    db.ActivityPlan.AddObject(activityPlan);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message.ToString());
                }
            }
            return new ArgsHelper(true, "添加教学活动成功");
        }


        /// <summary>
        /// 教师活动报名
        /// </summary>
        /// <returns></returns>
        public ArgsHelper TeacherSignUp(ActivitySignUp activitySignUp)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                try
                {
                    db.ActivitySignUp.AddObject(activitySignUp);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message.ToString());
                }
            }
            return new ArgsHelper(true, "报名成功");
        }


    }
}
