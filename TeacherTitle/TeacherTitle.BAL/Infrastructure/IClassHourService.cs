using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Model;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IClassHourService
    {
        /// <summary>
        /// 授予老师学时
        /// </summary>
        /// <param name="classHourSum"></param>
        /// <returns></returns>
        ArgsHelper AddClassHourSum(ClassHourSum classHourSum);

        /// <summary>
        /// 根据ASU_Code查询教师获取的学时
        /// </summary>
        /// <returns></returns>
        ClassHourSum GetClaHourInfoByASU_Code(int ASUCode);

        /// <summary>
        /// 根据报名编号修改获得的学时
        /// </summary>
        /// <param name="ASUCode"></param>
        /// <returns></returns>
        ArgsHelper AlterClaHourInfo(int ASUCode, int ClassHour);

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
        ActAndClaHourModel[] GetTeacherJoinActivity(string TeacherName, bool IsTeacherCode, string ActFormName, string Start, string End, bool IsGetClassHour);

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
        ActAndClaHourModel[] GetTeacherApplyActivity(string TeacherName, bool IsTeacherCode, string ActFormName, string Start, string End, bool IsGetClassHour);
    }
}
