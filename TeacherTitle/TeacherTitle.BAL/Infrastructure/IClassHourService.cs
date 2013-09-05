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
        /// 根据apcode获取报名信息
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        SignUpModels[] GetSignUpByAPCode(int apcode, bool isPersonApply, bool isEnd);

        /// <summary>
        /// 获取报名信息(正式)
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        SignUpModels[] GetSignUpZhenShiByAPCode(int apcode, bool isPersonApply, bool isEnd);

        /// <summary>
        /// 获取报名信息(候补)
        /// </summary>
        /// <param name="apcode"></param>
        /// <returns></returns>
        SignUpModels[] GetSignUpHouBuByAPCode(int apcode, bool isPersonApply, bool isEnd);

        /// <summary>
        /// 查询已结束的活动
        /// </summary>
        /// <returns></returns>
        AlreadyEndOfActModels[] GetAlreadyEndOfAct(string Teacher, string Account, string Institute, string StartTime, string EndTime);

    
        /// <summary>
        /// 教师个人学时统计
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Account"></param>
        /// <param name="StartYear"></param>
        /// <param name="EndYear"></param>
        /// <returns></returns>
        PerClassHourSumModels[] PerClassHourSum(string Name, string Account, string StartYear, string EndYear);
    
    }
}
