using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.BAL.Service;
using TeacherTitle.Infrastructure;
using TeacherTitle.Models;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.Controllers
{
    public class AdmController : Controller
    {
        IActivityService ActivityService { get; set; }
        IUserService UserService { get; set; }
        IBaseService BaseService { get; set; }
        IClassHourService ClassHourService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (ActivityService == null)
                ActivityService = new ActivityService();
            if (UserService == null)
                UserService = new UserService();
            if (BaseService == null)
                BaseService = new BaseService();
            if (ClassHourService == null)
                ClassHourService = new ClassHourService();
            base.Initialize(requestContext);
        }

        /// <summary>
        /// 基础数据
        /// </summary>
        private void SetRegisterUI()
        {
            var AllUserType = BaseService.GetAllUserType();
            ViewData["AllTypeList"] = Basehandle.ProduceTypeList(AllUserType);

            var AllInstitute = BaseService.GetAllInstitute();
            ViewData["AllInstituteList"] = Basehandle.ProduceInstituteList(AllInstitute);

            var AllMajor = BaseService.GetAllMajor();
            ViewData["AllMajorList"] = Basehandle.ProduceMajorList(AllMajor);

            ViewData["AllDegreeList"] = Basehandle.ProduceSelectList(Basehandle.AllDegree);

            ViewData["AllTitleList"] = Basehandle.ProduceSelectList(Basehandle.AllTitle);

        }

        /// <summary>
        ///GET 管理员主页
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Main(string U_Code)
        {
            ViewData["U_Code"] = U_Code;
            return PartialView();
        }


        /// <summary>
        /// GET 注册
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Register()
        {
            SetRegisterUI();
            return PartialView();
        }


        /// <summary>
        /// POST 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users()
                {
                    U_Account = registerModel.Account,
                    U_PassWord = registerModel.Account,
                    U_Name = registerModel.UserName,
                    I_Code = Convert.ToInt32(registerModel.Institute),
                    M_Code = Convert.ToInt32(registerModel.Major),
                    UT_Code = Convert.ToInt32(registerModel.UserType),
                    U_Degree = registerModel.Degree,
                    U_Title = registerModel.Title,
                    U_Mail = registerModel.Email,
                    U_Phone = registerModel.Phone
                };
                var args = UserService.AddUser(users);
                ViewData["result"] = args.Msg;
                return PartialView("_ResultPartial");
            }
            SetRegisterUI();
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return PartialView(registerModel);
        }

        /// <summary>
        ///GET 添加学时计算标准
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddHourCountStandard()
        {
            ViewData["AllPlaceList"] = Basehandle.ProduceSelectList(Basehandle.AllPlace);
            return PartialView();
        }

        /// <summary>
        ///POST 添加学时计算标准
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AddHourCountStandard(ClaHourCountStandardModels claHourCountStandardModels)
        {
            if (ModelState.IsValid)
            {
                TeachingActivity TeachingActivity = new DAL.DB.TeachingActivity()
                {
                    TA_Form = claHourCountStandardModels.TA_Form,
                    TA_Way = claHourCountStandardModels.TA_Way,
                    TA_Material = claHourCountStandardModels.TA_Material,
                    TA_ClassHour = claHourCountStandardModels.TA_ClassHour,
                    TA_PlaceKey = Convert.ToInt32(claHourCountStandardModels.TA_PlaceKey),
                    TA_PlaceValue = claHourCountStandardModels.TA_PlaceValue
                };

                var args = ActivityService.AddActivity(TeachingActivity);
                ViewData["result"] = args.Msg;
                return PartialView("_ResultPartial");
            }
            ViewData["AllPlaceList"] = Basehandle.ProduceSelectList(Basehandle.AllPlace);
            return PartialView();
        }

        /// <summary>
        ///GET 添加教学活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddActivity()
        {
            ViewData["AllActForm"] = Basehandle.ProduceSelectList(ActivityService.GetAllActForm());
            return PartialView();
        }


        /// <summary>
        ///POST 添加教学活动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult AddActivity(AddActivityModels addActivityModels, UploadModels uploadModels)
        {
            if (ModelState.IsValid)
            {
                ActivityPlan activityPlan = new ActivityPlan()
                {
                    TA_Code = Convert.ToInt32(addActivityModels.ActForm),
                    AP_Theme = addActivityModels.ActTheme,
                    AP_Speaker = addActivityModels.ActSpeaker,
                    AP_StartTime = addActivityModels.ActStartTime,
                    AP_EndTime = addActivityModels.ActEndTime,
                    AP_Place = addActivityModels.ActPlace,
                    AP_Limit = Convert.ToInt32(addActivityModels.ActLimit),
                    AP_Left = Convert.ToInt32(addActivityModels.ActLimit),
                    AP_Candidate = Convert.ToInt32(addActivityModels.ActCandidate),
                    AP_CandidateLeft = Convert.ToInt32(addActivityModels.ActCandidate),
                    AP_ReleaseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    AP_StatusKey = 1,
                    AP_StatusValue = "可报名"
                };
                var args = ActivityService.AddActivityPlan(activityPlan);
                if (args.Flag)//成功添加活动计划后添加附件
                {
                    for (int i = 0; i < uploadModels.FileName.Length; i++)
                    {
                        ActivityAttachment activityAttachment = new ActivityAttachment()
                        {
                            AP_Code = activityPlan.AP_Code,
                            AA_Name = uploadModels.FileName[i],
                            AA_Path = uploadModels.SaveName[i]
                        };
                        args = ActivityService.AddActivityAttachment(activityAttachment);
                    }
                }

                ViewData["result"] = args.Msg;
                return PartialView("_ResultPartial");
            }
            else
            {
                ViewData["AllActForm"] = Basehandle.ProduceSelectList(ActivityService.GetAllActForm());
                return PartialView(addActivityModels);
            }
        }


        /// <summary>
        /// GET 教师活动报名列表
        /// </summary>
        /// <returns></returns>
        public PartialViewResult CheckTeacherApply()
        {
            ViewData["AllTeacherName"] = Basehandle.ProduceSelectList(UserService.GetAllUserName());
            return PartialView();
        }

        /// <summary>
        /// GET 教师活动报名列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckTeacherApply(SearchClassHourModelsl model)
        {
            return RedirectToAction("ApplyList", "Activity", new
            {
                Teacher = model.Teacher,
                ActivityForm = model.ActivityForm,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            });
        }



        /// <summary>
        ///GET 获取还在报名状态的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Activiting()
        {
            var result = ActivityService.GetActiveActivityPlan();
            ViewData["ActiveList"] = result;
            return PartialView();
        }


        /// <summary>
        /// POST 结束报名
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult Activiting(string AP_Code)
        {
            var args = ActivityService.EndSignUp(Convert.ToInt32(AP_Code));
            var result = ActivityService.GetActiveActivityPlan();
            ViewData["ActiveList"] = result;
            return PartialView();
        }

        /// <summary>
        /// GET 获取已结束的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Activitied()
        {
            var result = ActivityService.GetEndActivityPlan();
            ViewData["ActiveList"] = result;
            return PartialView();
        }



    }
}
