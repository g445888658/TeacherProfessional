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

            var AllDepartment = BaseService.GetAllDepartment();
            ViewData["AllDepartmentList"] = Basehandle.ProduceMajorList(AllDepartment);

            ViewData["YesOrNo"] = Basehandle.ProduceSelectList(Basehandle.YesOrNo);

            ViewData["AllSexList"] = Basehandle.ProduceSelectList(Basehandle.AllSex);

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
        public ContentResult RegisterByExcel(HttpPostedFileBase fileData, string U_Code)
        {
            string result = string.Empty;
            string folder = Server.MapPath("~/FileData/") + U_Code;
            string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff");//获取时间
            string extension = System.IO.Path.GetExtension(fileData.FileName);//获取扩展名
            string newFileName = time + extension;//重组成新的文件名
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);

            fileData.SaveAs(folder + "\\" + newFileName);
            result = folder + "\\" + newFileName;
            var args = UserService.AddTeachers(result);
            return Content(args.Msg);
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
                    U_Sex = Convert.ToInt32(registerModel.UserSex),
                    U_Birth = registerModel.UserBirth,
                    U_Nation = registerModel.UserNation,
                    I_Code = Convert.ToInt32(registerModel.Institute),
                    D_Code = Convert.ToInt32(registerModel.Department),
                    U_SchoolTime = registerModel.UserSchoolTime,
                    U_WorkTime = registerModel.UserWorkTime,
                    U_Subject = registerModel.UserSubject,
                    U_Major = registerModel.UserMajor,
                    U_Research = registerModel.UserResearch,
                    U_Title = registerModel.UserTitle,
                    U_TitleLevel = registerModel.UserTitleLevel,
                    U_EngageTime = registerModel.UserEngageTime,
                    U_IsDoubleTitle = Convert.ToInt32(registerModel.UserIsDoubleTitle),
                    U_LongPhone = registerModel.UserLongPhone,
                    U_ShortPhone = registerModel.UserShortPhone,
                    U_Mail = registerModel.UserMail,
                    U_QQNum = registerModel.UserQQNum,
                    UT_Code = Convert.ToInt32(registerModel.UserType),
                    U_Remark = registerModel.Remark
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
                    AP_ClassHour = addActivityModels.ActClassHour,
                    AP_StatusKey = 1,
                    AP_StatusValue = "可报名"
                };
                var args = ActivityService.AddActivityPlan(activityPlan);
                if (args.Flag)//成功添加活动计划后添加附件
                {
                    if (uploadModels.FileName != null)
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


        ///// <summary>
        ///// GET 教师活动报名列表
        ///// </summary>
        ///// <returns></returns>
        //public PartialViewResult CheckTeacherApply()
        //{
        //    ViewData["AllTeacherName"] = Basehandle.ProduceSelectList(UserService.GetAllUserName());
        //    return PartialView();
        //}

        ///// <summary>
        ///// GET 教师活动报名列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult CheckTeacherApply(SearchClassHourModelsl model)
        //{
        //    return RedirectToAction("ApplyList", "Activity", new
        //    {
        //        Teacher = model.Teacher,
        //        ActivityForm = model.Account,
        //        StartTime = model.StartTime,
        //        EndTime = model.EndTime
        //    });
        //}



        ///// <summary>
        /////GET 获取还在报名状态的活动
        ///// </summary>
        ///// <returns></returns>
        //public PartialViewResult Activiting()
        //{
        //    var result = ActivityService.GetActiveActivityPlan();
        //    ViewData["ActiveList"] = result;
        //    return PartialView();
        //}


        ///// <summary>
        ///// POST 结束报名
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public PartialViewResult Activiting(string AP_Code)
        //{
        //    var args = ActivityService.AlterAP_Status(Convert.ToInt32(AP_Code), 3, "进行中");
        //    var result = ActivityService.GetActiveActivityPlan();
        //    ViewData["ActiveList"] = result;
        //    return PartialView();
        //}

        ///// <summary>
        ///// GET 获取已结束的活动
        ///// </summary>
        ///// <returns></returns>
        //public PartialViewResult Activitied()
        //{
        //    var result = ActivityService.GetEndActivityPlan();
        //    ViewData["ActiveList"] = result;
        //    return PartialView();
        //}

        /// <summary>
        /// GET 呈现在报名的所有活动的标题
        /// </summary>
        /// <returns></returns>
        public PartialViewResult RegistrationOfAct()
        {
            ViewData["AllActTheme"] = ActivityService.GetActivityPlan(1);
            return PartialView();
        }

        /// <summary>
        /// GET 呈现详细信息
        /// </summary>
        /// <param name="ap_Code"></param>
        /// <returns></returns>
        public PartialViewResult RegistrationOfActDetail(string ap_Code)
        {
            var detail = ActivityService.GetActivityPlansById(Convert.ToInt32(ap_Code));
            ViewData["ActDetail"] = detail;
            ViewData["AllSigUpZhenShi"] = ClassHourService.GetSignUpZhenShiByAPCode(detail.Plan.AP_Code);
            ViewData["AllSigUpHouBu"] = ClassHourService.GetSignUpHouBuByAPCode(detail.Plan.AP_Code);
            return PartialView();
        }


        /// <summary>
        ///POST 查询是否有候选人
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IsHaveCandidate(string AP_Code)
        {
            var result = ActivityService.IsHaveCandidate(Convert.ToInt32(AP_Code));
            var jsonData = new
            {
                Flag = result.Flag,
                Msg = result.Msg
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 从已报名的名单中剔除老师
        /// </summary>
        /// <param name="SignUpCode">报名编号</param>
        /// <param name="AP_Code">活动详情编号</param>
        /// <param name="IsReplace">是否让候选的老师顶替上</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RejectApply(string User_Code, string AP_Code, string IsReplace)
        {
            var args = ActivityService.RejectApply(Convert.ToInt32(User_Code), Convert.ToInt32(AP_Code));
            args = ActivityService.AfterRejectApply(Convert.ToInt32(AP_Code), Convert.ToInt32(IsReplace));
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = args.Msg
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 选择一个候补人员顶上
        /// </summary>
        /// <param name="AP_Code">活动编号</param>
        /// <param name="User_Code">候补人员的编号</param>
        /// <returns></returns>
        public JsonResult ChooseCandidate(string AP_Code, string User_Code)
        {
            var args = ActivityService.ChooseCandidate(Convert.ToInt32(AP_Code), Convert.ToInt32(User_Code));
            var jsonData = new
            {
                Flsg = args.Flag,
                Msg = args.Msg
            };
            return Json(jsonData);
        }

        /// <summary>
        /// POST 结束报名
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EndSignUp(string AP_Code)
        {
            var args = ActivityService.AlterAP_Status(Convert.ToInt32(AP_Code), 3, "进行中");
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = args.Msg
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 正在进行中的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult OngoingActivities()
        {
            ViewData["AllActTheme"] = ActivityService.GetActivityPlan(3);
            return PartialView();
        }

        /// <summary>
        /// 获取报名教师的信息
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public PartialViewResult OngoingDetail(string AP_Code)
        {
            ViewData["AllSigUpZhenShi"] = ClassHourService.GetSignUpZhenShiByAPCode(Convert.ToInt32(AP_Code));
            return PartialView();
        }

        /// <summary>
        /// 获取报名教师的信息
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult OngoingDetail(string[] ASU_Code, string[] ClassHour, string AP_code)
        {
            ClassHourSum model;
            for (int i = 0; i < ASU_Code.Length; i++)
            {
                if (ClassHourService.GetClaHourInfoByASU_Code(Convert.ToInt32(ASU_Code[i])) != null)
                {
                    var status = ActivityService.GetActivityPlanById(Convert.ToInt32(AP_code));
                    if (status.AP_StatusKey == 0)
                    {
                        return Json(new
                        {
                            Flag = false,
                            Msg = "已提交,不能再操作"
                        });
                    }
                    else
                        ClassHourService.AlterClaHourInfo(Convert.ToInt32(ASU_Code[i]), Convert.ToInt32(ClassHour[i]));
                }
                else
                {
                    model = new ClassHourSum()
                    {
                        ASU_Code = Convert.ToInt32(ASU_Code[i]),
                        CH_GetTime = DateTime.Now.ToString("yyyy/MM/dd"),
                        CH_GetHour = Convert.ToInt32(ClassHour[i])
                    };
                    var args = ClassHourService.AddClassHourSum(model);
                }
            }
            var jsonData = new
            {
                Flag = true,
                Msg = "保存成功"
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 结束修改(也就是将活动结束)
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public JsonResult EndAlter(string AP_Code)
        {
            var args = ActivityService.AlterAP_Status(Convert.ToInt32(AP_Code), 0, "结束");
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = "提交成功"
            };
            return Json(jsonData);
        }


        /// <summary>
        /// 已结束的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AlreadyEndOfAct()
        {
            return PartialView();
        }

        /// <summary>
        /// 呈现教师
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherTable()
        {
            ViewData["teacherTable"] = UserService.GetTeacherDetail();
            return PartialView();
        }

        /// <summary>
        /// 查询活动
        /// </summary>
        /// <param name="TeacherName">教师名字</param>
        /// <param name="Account">账号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public PartialViewResult ActAndTeacherList(SearchClassHourModelsl model)
        {
            ViewData["resultList"] = ClassHourService.GetAlreadyEndOfAct(model.Teacher, model.Account, model.StartTime, model.EndTime);
            return PartialView();
        }


    }
}
