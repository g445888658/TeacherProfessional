using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Model;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IUserService
    {
        UserModel LogOn(string Account, string PassWord);
    }
}
