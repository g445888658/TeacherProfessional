using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Model;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IActivityService
    {
        /// <summary>
        /// 添加教学活动
        /// </summary>
        /// <param name="teachingActivity">活动</param>
        /// <returns></returns>
        ArgsHelper AddActivity(TeachingActivity teachingActivity);

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="activityAttachment"></param>
        /// <returns></returns>
        ArgsHelper AddActivityAttachment(ActivityAttachment activityAttachment);

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="activityAttachment"></param>
        /// <returns></returns>
        ArgsHelper AddActivityMaterial(ActivityMaterial activityMaterial);

        /// <summary>
        /// 根据id获取附件
        /// </summary>
        /// <param name="AA_Code"></param>
        /// <returns></returns>
        ActivityAttachment GetActAttachmentById(int AA_Code);

        /// <summary>
        /// 获取活动备注
        /// </summary>
        /// <returns></returns>
        ActivityRemark GetActivityRemark();

        /// <summary>
        /// 获取所有的教学活动
        /// </summary>
        /// <returns></returns>
        TeachingActivity[] GetAllActivity();

        /// <summary>
        /// 根据关键字获取所教学活动形式
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<KeyValueModel> GetActForm(string keyword);

        /// <summary>
        /// 获取所有的教学活动形式
        /// </summary>
        /// <returns></returns>
        List<KeyValueModel> GetAllActForm();

        /// <summary>
        /// 获取校内的活动
        /// </summary>
        /// <returns></returns>
        TeachingActivity[] GetInActivity();

        /// <summary>
        /// 获取校外的活动
        /// </summary>
        /// <returns></returns>
        TeachingActivity[] GetOutActivity();

        /// <summary>
        /// 教师活动报名
        /// </summary>
        /// <param name="activitySignUp"></param>
        /// <returns></returns>
        ArgsHelper TeacherSignUp(ActivitySignUp activitySignUp);

        /// <summary>
        /// 在教师申请参加活动成功后名额减1
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <param name="IsCandidate"></param>
        /// <returns></returns>
        ArgsHelper AlterTeacherSignUp(int AP_Code, bool IsCandidate);

        /// <summary>
        /// 添加教学活动(详细)
        /// </summary>
        /// <param name="activityPlan"></param>
        /// <returns></returns>
        ArgsHelper AddActivityPlan(ActivityPlan activityPlan);

        /// <summary>
        /// 获取教学活动（详细）
        /// </summary>
        /// <returns></returns>
        ActivityPlan[] GetAllActivityPlan();

        /// <summary>
        /// 获取教学活动,有附件
        /// </summary>
        /// <returns></returns>
        ActivityPlanModels[] GetAllActivityPlans();

        /// <summary>
        /// 结束报名
        /// </summary>
        /// <param name="AP_Code">活动编号</param>
        /// <returns></returns>
        ArgsHelper EndSignUp(int AP_Code);

        /// <summary>
        /// 获取管理员发布的活动
        /// </summary>
        /// <returns></returns>
        ActivityPlan[] GetAdmActivityPlan();

        /// <summary>
        /// 获取还在报名的活动
        /// </summary>
        /// <returns></returns>
        ActivityPlan[] GetActiveActivityPlan();

        /// <summary>
        /// 获取结束报名的活动
        /// </summary>
        /// <returns></returns>
        ActivityPlan[] GetEndActivityPlan();

        /// <summary>
        /// 根据活动编号获取活动
        /// </summary>
        /// <param name="apCode"></param>
        /// <returns></returns>
        ActivityPlan GetActivityPlanById(Int32 apCode);

        /// <summary>
        /// 根据活动编号获取活动,有附件
        /// </summary>
        /// <param name="apCode"></param>
        /// <returns></returns>
        ActivityPlanModels GetActivityPlansById(Int32 apCode);

        /// <summary>
        /// 根据用户编号获取已报名的活动
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        ActAndClaHourModel[] GetActSignUpByUCode(int userCode);

        /// <summary>
        /// 根据用户编号和活动详细编号查询教师报名情况
        /// </summary>
        /// <param name="apCode"></param>
        /// <param name="U_Code"></param>
        /// <returns></returns>
        ActivitySignUp GetActSignUpByUCodeAPCode(int apCode, int U_Code);

        /// <summary>
        /// 查询是否有候选人
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        ArgsHelper IsHaveCandidate(int AP_Code);

        /// <summary>
        /// 将老师剔除
        /// </summary>
        /// <param name="ASU_Code"></param>
        /// <returns></returns>
        ArgsHelper RejectApply(int ASU_Code);

        /// <summary>
        /// 在将教师剔除后调用该方法,修改其对应的正式名额余量和候补名额余量
        /// </summary>
        /// <param name="AP_Code">活动详情编号</param>
        /// <param name="ASU_Code">报名编号</param>
        /// <param name="IsReplace">是否让候补顶替(0代表否,1代表是)</param>
        /// <returns></returns>
        ArgsHelper AfterRejectApply(int AP_Code, int ASU_Code, int IsReplace);


    }
}
