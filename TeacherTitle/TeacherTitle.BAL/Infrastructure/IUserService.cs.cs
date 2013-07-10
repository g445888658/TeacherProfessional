using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IUserService
    {
        bool LogOn(string Account, string PassWord);
    }
}
