﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.DAO;
using TeacherTitle.DAL.Model;

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
        /// 添加附件
        /// </summary>
        /// <param name="activityAttachment"></param>
        /// <returns></returns>
        public ArgsHelper AddActivityAttachment(ActivityAttachment activityAttachment)
        {
            return activityDAO.AddActivityAttachment(activityAttachment);
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="activityAttachment"></param>
        /// <returns></returns>
        public ArgsHelper AddActivityMaterial(ActivityMaterial activityMaterial)
        {
            return activityDAO.AddActivityMaterial(activityMaterial);
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="AA_Code"></param>
        /// <returns></returns>
        public ActivityAttachment GetActAttachmentById(int AA_Code)
        {
            return activityDAO.GetActAttachmentById(AA_Code);
        }

        /// <summary>
        /// 获取活动备注
        /// </summary>
        /// <returns></returns>
        public ActivityRemark GetActivityRemark()
        {
            return activityDAO.GetActivityRemark();
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
        /// 根据关键字获取所教学活动形式
        /// </summary>
        /// <returns></returns>
        public List<KeyValueModel> GetActForm(string keyword)
        {
            var act = GetAllActivity();
            List<KeyValueModel> result = new List<KeyValueModel>();
            for (int i = 0; i < act.Length; i++)
            {
                if (act[i].TA_Form.Contains(keyword) || GetSpelling.GetPYStr(act[i].TA_Form).Contains(keyword))
                {
                    result.Add(new KeyValueModel
                    {
                        Key = act[i].TA_Code.ToString(),
                        Value = act[i].TA_Form
                    });
                }

            }
            return result;
        }

        /// <summary>
        /// 获取所有的教学活动形式
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
        /// 在教师申请参加活动成功后名额减1
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <param name="IsCandidate">是否为候补名额</param>
        /// <returns></returns>
        public ArgsHelper AlterTeacherSignUp(int AP_Code, bool IsCandidate)
        {
            return activityDAO.AlterTeacherSignUp(AP_Code, IsCandidate);
        }

        /// <summary>
        /// 添加教学活动(详细)
        /// </summary>
        /// <returns></returns>
        public ArgsHelper AddActivityPlan(ActivityPlan activityPlan)
        {
            return activityDAO.AddActivityPlan(activityPlan);
        }

        /// <summary>
        /// 获取教学活动（详细）
        /// </summary>
        /// <returns></returns>
        public ActivityPlan[] GetAllActivityPlan()
        {
            return activityDAO.GetAllActivityPlan();
        }

        /// <summary>
        /// 获取教学活动,有附件
        /// </summary>
        /// <returns></returns>
        public ActivityPlanModels[] GetAllActivityPlans()
        {
            return activityDAO.GetAllActivityPlans();
        }

        /// <summary>
        /// 结束报名
        /// </summary>
        /// <param name="AP_Code">活动编号</param>
        /// <returns></returns>
        public ArgsHelper EndSignUp(int AP_Code)
        {
            return activityDAO.EndSignUp(AP_Code);
        }

        /// <summary>
        /// 获取管理员发布的活动
        /// </summary>
        /// <returns></returns>
        public ActivityPlan[] GetAdmActivityPlan()
        {
            return GetAllActivityPlan().Where(x => x.AP_StatusKey != 2).ToArray();
        }

        /// <summary>
        /// 获取还在报名的活动
        /// </summary>
        /// <returns></returns>
        public ActivityPlan[] GetActiveActivityPlan()
        {
            return GetAllActivityPlan().Where(x => x.AP_StatusKey == 1).ToArray();
        }

        /// <summary>
        /// 获取结束报名的活动
        /// </summary>
        /// <returns></returns>
        public ActivityPlan[] GetEndActivityPlan()
        {
            return GetAllActivityPlan().Where(x => x.AP_StatusKey == 0).ToArray();
        }

        /// <summary>
        /// 根据活动编号获取活动
        /// </summary>
        /// <param name="apCode"></param>
        /// <returns></returns>
        public ActivityPlan GetActivityPlanById(int apCode)
        {
            return GetAllActivityPlan().FirstOrDefault(x => x.AP_Code == apCode);
        }

        /// <summary>
        /// 根据活动编号获取活动,有附件
        /// </summary>
        /// <param name="apCode"></param>
        /// <returns></returns>
        public ActivityPlanModels GetActivityPlansById(int apCode)
        {
            return GetAllActivityPlans().FirstOrDefault(x => x.Plan.AP_Code == apCode);
        }

        /// <summary>
        /// 根据用户编号获取已报名的活动
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public ActAndClaHourModel[] GetActSignUpByUCode(int userCode)
        {
            return activityDAO.GetActSignUpByUCode(userCode.ToString()).ToArray();
        }


        /// <summary>
        /// 根据用户编号和活动详细编号查询教师报名情况
        /// </summary>
        /// <param name="apCode"></param>
        /// <param name="U_Code"></param>
        /// <returns></returns>
        public ActivitySignUp GetActSignUpByUCodeAPCode(int apCode, int U_Code)
        {
            var list = activityDAO.GetAllActivitySignUp();
            return list.FirstOrDefault(x => x.U_Code == U_Code && x.AP_Code == apCode);
        }


        /// <summary>
        /// 查询是否有候选人
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public ArgsHelper IsHaveCandidate(int AP_Code)
        {
            return activityDAO.IsHaveCandidate(AP_Code);
        }

        /// <summary>
        /// 将老师剔除
        /// </summary>
        /// <param name="ASU_Code"></param>
        /// <returns></returns>
        public ArgsHelper RejectApply(int ASU_Code)
        {
            return activityDAO.RejectApply(ASU_Code);
        }


        /// <summary>
        /// 在将教师剔除后调用该方法,修改其对应的正式名额余量和候补名额余量
        /// </summary>
        /// <param name="AP_Code">活动详情编号</param>
        /// <param name="ASU_Code">报名编号</param>
        /// <param name="IsReplace">是否让候补顶替(0代表否,1代表是)</param>
        /// <returns></returns>
        public ArgsHelper AfterRejectApply(int AP_Code, int ASU_Code, int IsReplace)
        {
            return activityDAO.AfterRejectApply(AP_Code, ASU_Code, IsReplace);
        }


    }
}
