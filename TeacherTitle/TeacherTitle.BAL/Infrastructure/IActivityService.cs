using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Model;

namespace TeacherTitle.BAL.Infrastructure
{
    public interface IActivityService
    {
        ArgsHelper AddActivity(TeachingActivity teachingActivity);

        TeachingActivity[] GetAllActivity();

        List<KeyValueModel> GetActForm(string keyword);

        List<KeyValueModel> GetAllActForm();

        TeachingActivity[] GetInActivity();

        TeachingActivity[] GetOutActivity();

        ArgsHelper TeacherSignUp(ActivitySignUp activitySignUp);

        ArgsHelper AlterTeacherSignUp(int AP_Code, bool IsCandidate);

        ArgsHelper AddActivityPlan(ActivityPlan activityPlan);

        ActivityPlan[] GetAllActivityPlan();

        ArgsHelper EndSignUp(int AP_Code);

        ActivityPlan[] GetAdmActivityPlan();

        ActivityPlan[] GetActiveActivityPlan();

        ActivityPlan[] GetEndActivityPlan();

        ActivityPlan GetActivityPlanById(Int32 apCode);

        ActAndClaHourModel[] GetActSignUpByUCode(int userCode);

        ActivitySignUp GetActSignUpByUCodeAPCode(int apCode, int U_Code);

        ArgsHelper RejectApply(int ASU_Code);

        ArgsHelper AfterRejectApply(int AP_Code, int ASU_Code, int IsReplace);


    }
}
