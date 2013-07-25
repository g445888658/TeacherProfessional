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
        ArgsHelper AddClassHourSum(ClassHourSum classHourSum);

        ClassHourSum GetClaHourInfoByASU_Code(int ASUCode);

        ArgsHelper AlterClaHourInfo(int ASUCode, int ClassHour);

        ActAndClaHourModel[] GetTeacherJoinActivity(string TeacherName, string ActFormName, string Start, string End, bool IsGetClassHour);

        ActAndClaHourModel[] GetTeacherApplyActivity(string TeacherName, string ActFormName, string Start, string End, bool IsGetClassHour);
    }
}
