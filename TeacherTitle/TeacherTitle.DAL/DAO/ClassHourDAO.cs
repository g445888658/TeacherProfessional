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
        public SignUpModels[] GetSignUpByAPCode(int apcode)
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
                                CH_Code = classhour == null ? null : classhour.CH_Code.ToString(),
                                CH_GetHour = classhour == null ? result[i].ActivityPlan.AP_ClassHour : classhour.CH_GetHour.ToString(),
                                CH_GetTime = classhour == null ? null : classhour.CH_GetTime.ToString()
                            });
                }
                return list.ToArray();
            }
        }




    }
}
