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
        /// 结束报名
        /// </summary>
        /// <param name="AP_Code">报名编号</param>
        /// <returns></returns>
        public ArgsHelper EndSignUp(int AP_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ActivityPlan.FirstOrDefault(x => x.AP_Code == AP_Code);
                result.AP_StatusKey = 0;
                result.AP_StatusValue = "报名结束";
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
        /// 获取教学活动（详细）
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
        /// 获取报名信息
        /// </summary>
        /// <returns></returns>
        public ActAndClaHourModel[] GetAllActSignUp()
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                //查询出所有可用的教师报名(非候补)
                var AllSignUp = db.ActivitySignUp.Where(x => x.ASU_StatusKey == 1 && x.ASU_IsCandidateKey == 0).ToArray();
                List<ActAndClaHourModel> list = new List<ActAndClaHourModel>();
                for (int i = 0; i < AllSignUp.Length; i++)
                {
                    list.Add(new ActAndClaHourModel()
                    {
                        SignUpCode = Convert.ToString(AllSignUp[i].ASU_Code),
                        UserCode = new KeyValueModel()
                        {
                            Key = Convert.ToString(AllSignUp[i].U_Code),
                            Value = AllSignUp[i].Users.U_Name
                        },
                        ActForm = new KeyValueModel()
                        {
                            Key = AllSignUp[i].ActivityPlan.TA_Code.ToString(),
                            Value = AllSignUp[i].ActivityPlan.TeachingActivity.TA_Form
                        },
                        APCode = AllSignUp[i].ActivityPlan.AP_Code.ToString(),
                        ActSpeaker = AllSignUp[i].ActivityPlan.AP_Speaker,
                        ActPlace = AllSignUp[i].ActivityPlan.AP_Place,
                        ActStart = AllSignUp[i].ActivityPlan.AP_StartTime,
                        ActEnd = AllSignUp[i].ActivityPlan.AP_EndTime,
                        ActReleaseTime = AllSignUp[i].ActivityPlan.AP_ReleaseTime,
                        ActTheme = AllSignUp[i].ActivityPlan.AP_Theme,
                        ActStatus = new KeyValueModel()
                        {
                            Key = AllSignUp[i].ActivityPlan.AP_StatusKey.ToString(),
                            Value = AllSignUp[i].ActivityPlan.AP_StatusValue
                        },
                        IsCandidate = new KeyValueModel()
                        {
                            Key = AllSignUp[i].ASU_IsCandidateKey.ToString(),
                            Value = AllSignUp[i].ASU_IsCandidateVal
                        }
                    });
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// 根据userCode获取报名信息
        /// </summary>
        /// <returns></returns>
        public ActAndClaHourModel[] GetActSignUpByUCode(string userCode)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return GetAllActSignUp().Where(x => x.UserCode.Key == userCode).ToArray();
            }
        }

        /// <summary>
        /// 将老师剔除
        /// </summary>
        /// <param name="ASU_Code"></param>
        /// <returns></returns>
        public ArgsHelper RejectApply(int ASU_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ActivitySignUp.FirstOrDefault(x => x.ASU_Code == ASU_Code);
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
        public ArgsHelper AfterRejectApply(int AP_Code, int ASU_Code, int IsReplace)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var activityDetail = db.ActivityPlan.FirstOrDefault(x => x.AP_Code == AP_Code);
                var signUp = db.ActivitySignUp.FirstOrDefault(x => x.ASU_Code == ASU_Code);
                if (IsReplace == 1)//让候补顶替上就选该活动的候补人员的最前面的的教师,修改其状态成为正式,然后该活动的正式名额余量不变,修改候补名额余量+1
                {
                    activityDetail.AP_CandidateLeft += 1;
                    signUp.ASU_IsCandidateKey = 0;
                    signUp.ASU_IsCandidateVal = "正式";
                }
                else//继续空着的话只要修改该活动的正式名额余量+1
                    activityDetail.AP_Left += 1;
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


    }
}
