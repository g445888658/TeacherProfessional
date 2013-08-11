using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DAO;
using TeacherTitle.DAL.Model;

namespace TeacherTitle.BAL.Service
{
    public class ClassHourService : IClassHourService
    {
        ClassHourDAO classHourDAO = new ClassHourDAO();
        ActivityDAO activityDAO = new ActivityDAO();

        /// <summary>
        /// 授予老师学时
        /// </summary>
        /// <param name="classHourSum"></param>
        /// <returns></returns>
        public ArgsHelper AddClassHourSum(ClassHourSum classHourSum)
        {
            return classHourDAO.AddClassHourSum(classHourSum);
        }


        /// <summary>
        /// 根据ASU_Code查询教师获取的学时
        /// </summary>
        /// <returns></returns>
        public ClassHourSum GetClaHourInfoByASU_Code(int ASUCode)
        {
            return classHourDAO.GetClaHourInfoByASU_Code(ASUCode);
        }

        /// <summary>
        /// 根据报名编号修改获得的学时
        /// </summary>
        /// <param name="ASUCode"></param>
        /// <returns></returns>
        public ArgsHelper AlterClaHourInfo(int ASUCode, int ClassHour)
        {
            return classHourDAO.AlterClaHourInfo(ASUCode, ClassHour);
        }


        /// <summary>
        /// 根据apcode获取报名信息
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        public SignUpModels[] GetSignUpByAPCode(int apcode)
        {
            return classHourDAO.GetSignUpByAPCode(apcode);
        }

        /// <summary>
        /// 获取报名信息(正式)
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        public SignUpModels[] GetSignUpZhenShiByAPCode(int apcode)
        {
            return classHourDAO.GetSignUpByAPCode(apcode).Where(x => x.ASU_IsCandidateKey == "0").ToArray();
        }

        /// <summary>
        /// 获取报名信息(候补)
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        public SignUpModels[] GetSignUpHouBuByAPCode(int apcode)
        {
            return classHourDAO.GetSignUpByAPCode(apcode).Where(x => x.ASU_IsCandidateKey == "1").ToArray();
        }


        /// <summary>
        /// 查询已结束的活动
        /// </summary>
        /// <returns></returns>
        public AlreadyEndOfActModels[] GetAlreadyEndOfAct(string Teacher, string Account, string StartTime, string EndTime)
        {
            var array = new string[4] { Teacher, Account, StartTime, EndTime };
            List<AlreadyEndOfActModels> list = new List<AlreadyEndOfActModels>();
            var allPlan = activityDAO.GetAllActivityPlan().Where(x => x.AP_StatusKey == 0).ToArray();//获取所有的已结束的活动
            for (int i = 0; i < allPlan.Length; i++)
            {
                var SignUp = GetSignUpZhenShiByAPCode(allPlan[i].AP_Code);
                list.Add(new AlreadyEndOfActModels()
                {
                    Plan = allPlan[i],
                    SignUpModels = SignUp
                });
            }
            for (int i = 0; i < 4; i++)
            {
                if (!string.IsNullOrEmpty(array[i]))
                {
                    switch (i)
                    {
                        //根据教师名字筛选
                        case 0:
                            //List<AlreadyEndOfActModels> list0 = new List<AlreadyEndOfActModels>();
                            for (int j = 0; j < list.Count; j++)
                            {
                                list[j].SignUpModels = list[j].SignUpModels.Where(x => x.U_Name == Teacher).ToArray();
                            }
                            list.RemoveAll(x => x.SignUpModels.Length == 0);
                            break;
                        //根据工号筛选
                        case 1:
                            for (int j = 0; j < list.Count; j++)
                            {
                                list[j].SignUpModels = list[j].SignUpModels.Where(x => x.U_Account == Account).ToArray();
                            }
                            list.RemoveAll(x => x.SignUpModels.Length == 0);
                            break;
                        //根据开始时间筛选
                        case 2:
                            list = list.Where(x => DateTime.Parse(x.Plan.AP_StartTime) > DateTime.Parse(StartTime)).ToList();
                            break;
                        //根据结束时间筛选
                        case 3:
                            list = list.Where(x => DateTime.Parse(x.Plan.AP_EndTime) < DateTime.Parse(EndTime)).ToList();
                            break;
                    }
                }
            }
            return list.ToArray();
        }


    }
}
