using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Model;

namespace TeacherTitle.DAL.DAO
{
    public class ClassHourDAO
    {
        /// <summary>
        /// 查询教师上传的活动材料
        /// </summary>
        /// <returns></returns>
        public ActivityMaterial[] GetActivityMaterial(int ASU_Code)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                return db.ActivityMaterial.Where(x => x.ASU_Code == ASU_Code).ToArray();
            }
        }


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

        /// <summary>
        /// 根据apcode获取报名信息
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        public SignUpModels[] GetSignUpByAPCode(int apcode, bool isPersonApply, bool isEnd)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {
                var result = db.ActivitySignUp.Where(x => x.AP_Code == apcode && x.ASU_StatusKey == 1).ToArray();
                List<SignUpModels> list = new List<SignUpModels>();
                for (int i = 0; i < result.Length; i++)
                {
                    var classhour = result[i].ClassHourSum.FirstOrDefault();

                    list.Add(new SignUpModels()
                    {
                        ASU_Code = result[i].ASU_Code.ToString(),
                        U_Code = result[i].U_Code.ToString(),
                        AP_Code = result[i].AP_Code.ToString(),
                        ASU_Time = result[i].ASU_Time,
                        ASU_IsCandidateKey = result[i].ASU_IsCandidateKey.ToString(),
                        ASU_IsCandidateVal = result[i].ASU_IsCandidateVal,
                        U_Account = result[i].Users.U_Account,
                        U_Name = result[i].Users.U_Name,
                        U_Title = result[i].Users.U_Title,
                        I_Code = result[i].Users.I_Code.ToString(),
                        Institute = result[i].Users.Institute.I_Name,
                        CH_Code = classhour == null ? null : classhour.CH_Code.ToString(),
                        CH_GetHour = classhour == null ? null : classhour.CH_GetHour.ToString(),
                        CH_GetTime = classhour == null ? null : classhour.CH_GetTime.ToString()
                    });
                    if (isPersonApply)
                        list[i].CH_GetHour = result[i].ActivityPlan.AP_ClassHour;
                    else
                        list[i].CH_GetHour = null;
                    if (isEnd)
                    {
                        var result1 = result[i].ClassHourSum.FirstOrDefault();
                        if (result1 != null)
                        {
                            list[i].CH_GetHour = result1.CH_GetHour.ToString();
                        }
                    }
                    //if (classhour != null)
                    //{
                    var asu_Code = result[i].ASU_Code;
                    var material = db.ActivityMaterial.Where(x => x.ASU_Code == asu_Code).ToArray();
                    list[i].ActivityMaterial = material;
                    //}
                }
                return list.ToArray();
            }
        }



        /// <summary>
        /// 教师个人学时统计
        /// </summary>
        /// <param name="SchoolYear"></param>
        /// <returns></returns>
        public PerClassHourModels[] PerClassHourSum(string SchoolYear)
        {
            using (TTitleDBEntities db = new TTitleDBEntities())
            {

                var result = (from tact in db.TeachingActivity
                              from act in db.ActivityPlan
                              from signUp in db.ActivitySignUp
                              from hour in db.ClassHourSum
                              from teacher in db.Users
                              where tact.TA_Code == act.TA_Code && act.AP_Code == signUp.AP_Code && signUp.ASU_Code == hour.ASU_Code
                              && signUp.U_Code == teacher.U_Code && act.AP_SchoolYear == SchoolYear
                              select new PerClassHourModels
                              {
                                  Account = teacher.U_Account,
                                  Name = teacher.U_Name,
                                  Act = act,
                                  Tact = tact,
                                  Hour = hour
                              }).ToArray();


                return result;
            }
        }


    }
}
