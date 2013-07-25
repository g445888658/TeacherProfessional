using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IBaseService
    {
        List<UserType> GetAllUserType();

        List<Institute> GetAllInstitute();

        List<Major> GetAllMajor();

        ArgsHelper SetSysMail(SysConfig sysConfig);

        SysConfig GetMailConfig();
    }
}
