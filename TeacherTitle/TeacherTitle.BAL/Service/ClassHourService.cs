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
        public SignUpModels[] GetSignUpByAPCode(int apcode, bool isPersonApply, bool isEnd)
        {
            return classHourDAO.GetSignUpByAPCode(apcode, isPersonApply, isEnd);
        }

        /// <summary>
        /// 获取报名信息(正式)
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        public SignUpModels[] GetSignUpZhenShiByAPCode(int apcode, bool isPersonApply, bool isEnd)
        {
            return classHourDAO.GetSignUpByAPCode(apcode, isPersonApply, isEnd).Where(x => x.ASU_IsCandidateKey == "0").ToArray();
        }

        /// <summary>
        /// 获取报名信息(候补)
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        public SignUpModels[] GetSignUpHouBuByAPCode(int apcode, bool isPersonApply, bool isEnd)
        {
            return classHourDAO.GetSignUpByAPCode(apcode, isPersonApply, isEnd).Where(x => x.ASU_IsCandidateKey == "1").ToArray();
        }


        /// <summary>
        /// 查询已结束的活动
        /// </summary>
        /// <returns></returns>
        public AlreadyEndOfActModels[] GetAlreadyEndOfAct(string Teacher, string Account, string Institute, string StartTime, string EndTime)
        {
            var array = new string[5] { Teacher, Account, Institute, StartTime, EndTime };
            List<AlreadyEndOfActModels> list = new List<AlreadyEndOfActModels>();
            var allPlan = activityDAO.GetAllActivityPlan().Where(x => x.AP_StatusKey == 0).ToArray();//获取所有的已结束的活动
            for (int i = 0; i < allPlan.Length; i++)
            {
                var SignUp = GetSignUpZhenShiByAPCode(allPlan[i].AP_Code, false, true);
                var TeachingActivity = activityDAO.GetAllActivity().FirstOrDefault(x => x.TA_Code == allPlan[i].TA_Code);
                list.Add(new AlreadyEndOfActModels()
                {
                    Plan = allPlan[i],
                    teachingActivity = TeachingActivity,
                    SignUpModels = SignUp
                });
            }
            for (int i = 0; i < 5; i++)
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
                                list[j].SignUpModels = list[j].SignUpModels.Where(x => x.U_Name == array[0]).ToArray();
                            }
                            list.RemoveAll(x => x.SignUpModels.Length == 0);
                            break;
                        //根据工号筛选
                        case 1:
                            for (int j = 0; j < list.Count; j++)
                            {
                                list[j].SignUpModels = list[j].SignUpModels.Where(x => x.U_Account == array[1]).ToArray();
                            }
                            list.RemoveAll(x => x.SignUpModels.Length == 0);
                            break;
                        //根据学院筛选
                        case 2:
                            for (int j = 0; j < list.Count; j++)
                            {
                                list[j].SignUpModels = list[j].SignUpModels.Where(x => x.I_Code == array[2]).ToArray();
                            }
                            list.RemoveAll(x => x.SignUpModels.Length == 0);
                            break;
                        //根据开始时间筛选
                        case 3:
                            //var asd = DateTime.Parse(StartTime);
                            list = list.Where(x => DateTime.Parse(x.Plan.AP_StartTime).Date >= DateTime.Parse(array[3])).ToList();
                            break;
                        //根据结束时间筛选
                        case 4:
                            list = list.Where(x => DateTime.Parse(x.Plan.AP_EndTime).Date <= DateTime.Parse(array[4])).ToList();
                            break;
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 教师个人学时统计
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Account"></param>
        /// <param name="StartYear"></param>
        /// <param name="EndYear"></param>
        /// <returns></returns>
        public PerClassHourSumModels[] PerClassHourSum(string Name, string Account, string StartYear, string EndYear)
        {
            int start = Convert.ToInt32(StartYear.Split('-')[0]);
            int end = Convert.ToInt32(EndYear.Split('-')[1]);

            var list = new List<PerClassHourSumModels>();

            for (int i = 1; i < end - start + 1; i++)
            {
                var Year = (start + i - 1).ToString() + "-" + (start + i).ToString();
                var result = classHourDAO.PerClassHourSum(Year);

                if (!string.IsNullOrEmpty(Name))
                {
                    result = result.Where(x => x.Name == Name).ToArray();
                }
                if (!string.IsNullOrEmpty(Account))
                {
                    result = result.Where(x => x.Account == Account).ToArray();
                }
                if (result.Length == 0)
                {
                    return list.ToArray();
                }
                var In = result.Where(x => x.Tact.TA_PlaceKey == 0).Sum(x => x.Hour.CH_GetHour);//校内
                var Out = result.Where(x => x.Tact.TA_PlaceKey == 1).Sum(x => x.Hour.CH_GetHour);//校外
                list.Add(new PerClassHourSumModels()
                {
                    Name = result[0].Name,
                    Account = result[0].Account,
                    SchoolYear = Year,
                    SchoolIn = In.ToString(),
                    SchoolOut = Out.ToString(),
                    ClassHourSum = (In + Out).ToString()
                });
            }


            return list.ToArray();
        }


    }
}
