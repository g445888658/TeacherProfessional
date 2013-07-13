using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IActivityService
    {
        ArgsHelper AddActivity(TeachingActivity teachingActivity);

        TeachingActivity[] GetAllActivity();

        List<KeyValueModel> GetAllActForm();

        TeachingActivity[] GetInActivity();

        TeachingActivity[] GetOutActivity();

        ArgsHelper TeacherSignUp(ActivitySignUp activitySignUp);

        ArgsHelper AddActivityPlan(ActivityPlan activityPlan);
    }
}
