using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.Model;

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
            return new ArgsHelper(true, "添加成功");
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="activityAttachment"></param>
        /// <returns></returns>
        public ArgsHelper AddActivityAttachment(ActivityAttachment activityAttachment)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                try
                {
                    db.ActivityAttachment.AddObject(activityAttachment);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message);
                }
            }
            return new ArgsHelper(true, "添加成功");
        }


        /// <summary>
        /// 添加材料
        /// </summary>
        /// <param name="activityAttachment"></param>
        /// <returns></returns>
        public ArgsHelper AddActivityMaterial(ActivityMaterial activityMaterial)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                try
                {
                    db.ActivityMaterial.AddObject(activityMaterial);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message);
                }
            }
            return new ArgsHelper(true, "添加成功");
        }

        /// <summary>
        /// 查询教师上传的附件
        /// </summary>
        /// <returns></returns>
        public ActivityMaterial GetTeacherAttachmentById(int AM_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ActivityMaterial.FirstOrDefault(x => x.AM_Code == AM_Code);
            }
        }


        /// <summary>
        /// 查询活动的附件
        /// </summary>
        /// <returns></returns>
        public ActivityAttachment GetActAttachmentById(int AA_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ActivityAttachment.FirstOrDefault(x => x.AA_Code == AA_Code);
            }
        }

        /// <summary>
        /// 查询活动的附件
        /// </summary>
        /// <returns></returns>
        public ActivityAttachment[] GetActivityAttachment(int AP_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ActivityAttachment.Where(x => x.AP_Code == AP_Code).ToArray();
            }
        }

        /// <summary>
        /// 获取活动备注
        /// </summary>
        /// <returns></returns>
        public ActivityRemark GetActivityRemark()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ActivityRemark.FirstOrDefault();
            }
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
            return new ArgsHelper(true, "申请成功");
        }

        /// <summary>
        /// 在教师申请参加活动成功后名额减1
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <param name="IsCandidate">是否为候补名额</param>
        /// <returns></returns>
        public ArgsHelper AlterTeacherSignUp(int AP_Code, bool IsCandidate)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ActivityPlan.FirstOrDefault(x => x.AP_Code == AP_Code);
                if (IsCandidate)
                    result.AP_CandidateLeft -= 1;
                else
                    result.AP_Left -= 1;
                try
                {
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
        /// 获取所有的教学活动（详细）
        /// </summary>
        /// <returns></returns>
        public ActivityPlan[] GetAllActivityPlan()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ActivityPlan.OrderBy(x => x.AP_Code).ToArray();
            }
        }

        /// <summary>
        /// 获取教学活动,有附件
        /// </summary>
        /// <returns></returns>
        public ActivityPlanModels[] GetAllActivityPlans()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                List<ActivityPlanModels> list = new List<ActivityPlanModels>();
                var result = GetAllActivityPlan();
                for (int i = 0; i < result.Length; i++)
                {
                    var ap_Code = result[i].AP_Code;
                    var attachment = GetActivityAttachment(ap_Code);
                    list.Add(new ActivityPlanModels()
                    {
                        Plan = result[i],
                        Attachment = attachment
                    });
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// 获取所有的报名信息
        /// </summary>
        /// <returns></returns>
        public ActivitySignUp[] GetAllActivitySignUp()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ActivitySignUp.OrderBy(x => x.ASU_Code).ToArray();
            }
        }

        

       

        /// <summary>
        /// 查询是否有候选人
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public ArgsHelper IsHaveCandidate(int AP_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                if (db.ActivitySignUp.FirstOrDefault(x => x.AP_Code == AP_Code && x.ASU_IsCandidateKey == 1) != null)
                    return new ArgsHelper();
                else
                    return new ArgsHelper("没有候选人");
            }
        }

        /// <summary>
        /// 将老师剔除
        /// </summary>
        /// <param name="ASU_Code"></param>
        /// <returns></returns>
        public ArgsHelper RejectApply(int U_Code, int AP_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ActivitySignUp.FirstOrDefault(x => x.U_Code == U_Code && x.AP_Code == AP_Code);
                result.ASU_StatusKey = 0;
                result.ASU_StatusValue = "剔除";
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message);
                }
            }
            return new ArgsHelper(true, "剔除成功");
        }

        /// <summary>
        /// 在将教师剔除后调用该方法,修改其对应的正式名额余量和候补名额余量
        /// </summary>
        /// <param name="AP_Code">活动详情编号</param>
        /// <param name="ASU_Code">报名编号</param>
        /// <param name="IsReplace">是否让候补顶替(0代表否,1代表是)</param>
        /// <returns></returns>
        public ArgsHelper AfterRejectApply(int AP_Code, int IsReplace)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var activityDetail = db.ActivityPlan.FirstOrDefault(x => x.AP_Code == AP_Code);
                var signUp = db.ActivitySignUp.OrderBy(x => x.ASU_Code).FirstOrDefault(x => x.AP_Code == AP_Code && x.ASU_IsCandidateKey == 1);//筛选出该活动最靠前的候选人
                if (IsReplace == 1)//让候补顶替上就选该活动的候补人员的最前面的的教师,修改其状态成为正式,然后该活动的正式名额余量不变,修改候补名额余量+1
                {
                    if (activityDetail.AP_CandidateLeft != activityDetail.AP_Candidate)
                        activityDetail.AP_CandidateLeft += 1;
                    signUp.ASU_IsCandidateKey = 0;
                    signUp.ASU_IsCandidateVal = "正式";
                }
                else//继续空着的话只要修改该活动的正式名额余量+1
                {
                    if (activityDetail.AP_Left != activityDetail.AP_Limit)
                        activityDetail.AP_Left += 1;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message);
                }
            }
            return new ArgsHelper(true, "剔除成功");
        }


        /// <summary>
        /// 选择一个候补人员顶上
        /// </summary>
        /// <param name="AP_Code">活动编号</param>
        /// <param name="User_Code">候补人员编号</param>
        /// <returns></returns>
        public ArgsHelper ChooseCandidate(int AP_Code, int User_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var signUp = db.ActivitySignUp.FirstOrDefault(x => x.AP_Code == AP_Code && x.U_Code == User_Code);
                signUp.ASU_IsCandidateKey = 0;
                signUp.ASU_IsCandidateVal = "正式";
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message);
                }
                return new ArgsHelper();
            }
        }

        /// <summary>
        /// 修改活动状态
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ArgsHelper AlterAP_Status(int AP_Code, int key, string value)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ActivityPlan.AsParallel().FirstOrDefault(x => x.AP_Code == AP_Code);
                result.AP_StatusKey = key;
                result.AP_StatusValue = value;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new ArgsHelper(ex.Message);
                }
            }
            return new ArgsHelper();
        }

    }
}
