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
        /// 查询教师参加的活动
        /// </summary>
        /// <param name="TeacherCode">名字</param>
        /// <param name="isTeacherCode">TeacherName是否为教师编号</param>
        /// <param name="ActFormCode">活动形式</param>
        /// <param name="Start">开始时间</param>
        /// <param name="End">结束时间</param>
        /// <param name="IsGetClassHour">是否获取学时</param>
        /// <returns></returns>
        public ActAndClaHourModel[] GetActivityInfo(string TeacherName, bool isTeacherCode, string ActFormName, string Start, string End, bool IsGetClassHour)
        {
            ActAndClaHourModel[] list = activityDAO.GetAllActSignUp();
            List<ActAndClaHourModel> newList = new List<ActAndClaHourModel>();
            //删选出已获得学时的活动
            for (int i = 0; i < list.Length; i++)
            {
                var classHour = GetClaHourInfoByASU_Code(Convert.ToInt32(list[i].SignUpCode));
                if (IsGetClassHour)
                {
                    if (classHour != null)//有学时
                    {
                        if (classHour.CH_GetHour != null)
                        {
                            list[i].ClassHour = classHour.CH_GetHour.ToString();
                            list[i].GetTime = classHour.CH_GetTime;
                            newList.Add(list[i]);
                        }
                    }
                }
                else//无学时
                {
                    if (classHour != null)
                    {
                        if (classHour.CH_GetHour == null)
                            newList.Add(list[i]);
                    }
                    else
                        newList.Add(list[i]);
                }
            }

            var arraTiaojian = new string[4] { TeacherName, ActFormName, Start, End };
            for (int i = 0; i < 4; i++)
            {
                if (!string.IsNullOrEmpty(arraTiaojian[i]))
                {
                    switch (i)
                    {
                        case 0://用户名
                            {
                                if (isTeacherCode)
                                    newList = newList.Where(x => x.UserCode.Key == TeacherName).ToList();
                                else
                                    newList = newList.Where(x => x.UserCode.Value == TeacherName).ToList();
                            }
                            break;
                        case 1: //活动形式
                            {
                                newList = newList.Where(x => x.ActForm.Value == ActFormName).ToList();
                            };
                            break;
                        case 2://开始时间
                            {
                                newList = newList.Where(x => DateTime.Parse(x.ActStart) > DateTime.Parse(Start)).ToList();
                            };
                            break;
                        case 3://结束时间
                            {
                                newList = newList.Where(x => DateTime.Parse(x.ActEnd) < DateTime.Parse(End).AddDays(1)).ToList();
                            };
                            break;
                    }
                }
            }
            return newList.ToArray();
        }

        /// <summary>
        /// 获取教师参加的活动
        /// </summary>
        /// <param name="TeacherName"></param>
        /// <param name="IsTeacherCode">TeacherName是否为教师编号</param>
        /// <param name="ActFormName"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="IsGetClassHour">是否获取学时</param>
        /// <returns></returns>
        public ActAndClaHourModel[] GetTeacherJoinActivity(string TeacherName, bool IsTeacherCode, string ActFormName, string Start, string End, bool IsGetClassHour)
        {
            var list = GetActivityInfo(TeacherName, IsTeacherCode, ActFormName, Start, End, IsGetClassHour);
            return list.Where(x => x.ActStatus.Key == "1").ToArray();
        }

        /// <summary>
        /// 获取教师申请的活动
        /// </summary>
        /// <param name="TeacherName"></param>
        /// <param name="IsTeacherCode">TeacherName是否为教师编号</param>
        /// <param name="ActFormName"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="IsGetClassHour">是否获取学时</param>
        /// <returns></returns>
        public ActAndClaHourModel[] GetTeacherApplyActivity(string TeacherName, bool IsTeacherCode, string ActFormName, string Start, string End, bool IsGetClassHour)
        {
            var list = GetActivityInfo(TeacherName, IsTeacherCode, ActFormName, Start, End, IsGetClassHour);
            return list.Where(x => x.ActStatus.Key == "2").ToArray();
        }


    }
}
