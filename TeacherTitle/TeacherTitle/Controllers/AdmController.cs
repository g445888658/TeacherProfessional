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
        IExcelService ExcelService { get; set; }

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
            if (ExcelService == null)
                ExcelService = new ExcelService();
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
        /// GET Excel导入教师
        /// </summary>
        /// <returns></returns>
        public PartialViewResult RegisterByExcel()
        {
            return PartialView();
        }

        /// <summary>
        /// POST Excel导入教师
        /// </summary>
        /// <returns></returns>
        [HttpPost]
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
        /// 教师个人学时汇总
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherClassHourSum()
        {
            ViewData["SearchYear"] = Basehandle.ProduceSelectList(BaseService.GetAllSchoolYear());
            return PartialView();
        }

        /// <summary>
        /// 教师个人学时汇总
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult TeacherClassHourSumDetail(PerSearchClassHourModelsl model)
        {
            if (ModelState.IsValid)
            {
                var result = ClassHourService.PerClassHourSum(model.Teacher, model.Account, model.StartYear, model.EndYear);
                ViewData["ClassHourResult"] = result;
            }
            ViewData["Teacher"] = model.Teacher;
            ViewData["Account"] = model.Account;
            ViewData["StartYear"] = model.StartYear;
            ViewData["EndYear"] = model.EndYear;
            return PartialView();
        }


        public FileResult ExportClassHourSumDetail(string Teacher, string Account, string StartYear, string EndYear)
        {
            var result = ClassHourService.PerClassHourSum(Teacher, Account, StartYear, EndYear);

            string filePath = Server.MapPath("~/ExcelFile/PersonalSummary.xls");
            var args = ExcelService.creatExcelFilePerson(filePath, result);

            return File(args.Msg, "application/-excel", "PersonalSummary.xls");
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
            ViewData["Year"] = Basehandle.ProduceSelectList(Basehandle.GetSchoolYear());
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
                    AP_SchoolYear = addActivityModels.ActSchoolYear,
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
                ViewData["Year"] = Basehandle.ProduceSelectList(Basehandle.GetSchoolYear());
                return PartialView(addActivityModels);
            }
        }




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
            ViewData["AllSigUpZhenShi"] = ClassHourService.GetSignUpZhenShiByAPCode(detail.Plan.AP_Code, false, false);
            ViewData["AllSigUpHouBu"] = ClassHourService.GetSignUpHouBuByAPCode(detail.Plan.AP_Code, false, false);
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
            var activity = ActivityService.GetActivityPlanById(Convert.ToInt32(AP_Code));
            string msg = string.Empty;
            bool flag = false;
            if (activity.AP_Left == 0)
                msg = "名额已满,操作无效";
            else
            {
                var args = ActivityService.ChooseCandidate(Convert.ToInt32(AP_Code), Convert.ToInt32(User_Code));
                flag = args.Flag;
                msg = args.Msg;
            }
            var jsonData = new
            {
                Flsg = flag,
                Msg = msg
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
            ViewData["AllSigUpZhenShi"] = ClassHourService.GetSignUpZhenShiByAPCode(Convert.ToInt32(AP_Code), false, true);
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
        public JsonResult EndAlter(string AP_Code, string[] ASU_Code)
        {
            //先要验证是否已保存
            bool flag = true;
            string msg = "提交成功";
            for (int i = 0; i < ASU_Code.Length; i++)
            {
                var classHour = ClassHourService.GetClaHourInfoByASU_Code(Convert.ToInt32(ASU_Code[i]));
                if (classHour == null)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
                ActivityService.AlterAP_Status(Convert.ToInt32(AP_Code), 0, "结束");
            else
                msg = "请先保存";

            var jsonData = new
            {
                Flag = flag,
                Msg = msg
            };
            return Json(jsonData);
        }


        /// <summary>
        /// 已结束的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AlreadyEndOfAct()
        {
            ViewData["AllInstitute"] = Basehandle.ProduceInstituteList(BaseService.GetAllInstitute());
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
            ViewData["resultList"] = ClassHourService.GetAlreadyEndOfAct(model.Teacher, model.Account, model.Institute, model.StartTime, model.EndTime);
            return PartialView();
        }

        /// <summary>
        /// 导出已结束的活动学时统计
        /// </summary>
        /// <param name="Teacher"></param>
        /// <param name="Account"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public FileResult ExportEndAct(string Teacher, string Account, string Institute, string StartTime, string EndTime)
        {
            var result = ClassHourService.GetAlreadyEndOfAct(Teacher, Account, Institute, StartTime, EndTime);
            string filePath = Server.MapPath("~/ExcelFile/InstituteHourSummary.xls");

            var args = ExcelService.creatExcelFileInstitute(filePath, result, null, StartTime, EndTime);

            return File(args.Msg, "text/plain", "InstituteHourSummary.xls");
        }

        /// <summary>
        ///GET 教师申请列表
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherPerApply()
        {
            var list = ActivityService.GetSignUp(0, true, false, 2);
            ViewData["list"] = list;
            return PartialView();
        }

        /// <summary>
        ///POST 教师申请列表(保存)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TeacherPerApply(string ASU_Code, string ClassHour, string AP_Code)
        {
            ClassHourSum model;

            if (ClassHourService.GetClaHourInfoByASU_Code(Convert.ToInt32(ASU_Code)) != null)
            {
                var status = ActivityService.GetActivityPlanById(Convert.ToInt32(AP_Code));
                if (status.AP_StatusKey == 4)
                {
                    return Json(new
                    {
                        Flag = false,
                        Msg = "已提交,不能再操作"
                    });
                }
                else
                    ClassHourService.AlterClaHourInfo(Convert.ToInt32(ASU_Code), Convert.ToInt32(ClassHour));
            }
            else
            {
                if (!string.IsNullOrEmpty(ClassHour))
                {
                    model = new ClassHourSum()
                    {
                        ASU_Code = Convert.ToInt32(ASU_Code),
                        CH_GetTime = DateTime.Now.ToString("yyyy/MM/dd"),
                        CH_GetHour = Convert.ToInt32(ClassHour)
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
        ///POST 教师申请列表(提交)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubmitTeacherPerApply(string AP_Code)
        {
            var args = ActivityService.AlterAP_Status(Convert.ToInt32(AP_Code), 4, "申请结束");
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = "提交成功"
            };
            return Json(jsonData);
        }


        /// <summary>
        /// GET 教师申请历史
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherPerApplyFinishSearch()
        {
            ViewData["AllInstitute"] = Basehandle.ProduceInstituteList(BaseService.GetAllInstitute());
            return PartialView();
        }

        /// <summary>
        /// GET 教师申请历史
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherPerApplyFinish(SearchClassHourModelsl model)
        {
            var list = ActivityService.GetSignUp(0, true, true, 4);
            list = ActivityService.TeacherPerApplyFinishSearch(list, model.Teacher, model.Account, model.Institute, model.StartTime, model.EndTime);
            ViewData["list"] = list;
            return PartialView();
        }

        /// <summary>
        /// 导出已结束的教师申请
        /// </summary>
        /// <param name="Teacher"></param>
        /// <param name="Account"></param>
        /// <param name="Institute"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public FileResult ExportPerEndAct(string Teacher, string Account, string Institute, string StartTime, string EndTime)
        {
            var list = ActivityService.GetSignUp(0, true, true, 4);
            list = ActivityService.TeacherPerApplyFinishSearch(list, Teacher, Account, Institute, StartTime, EndTime);
            string filePath = Server.MapPath("~/ExcelFile/InstituteHourSummary.xls");
            var args = ExcelService.creatExcelFileInstitute(filePath, null, list, StartTime, EndTime);

            return File(args.Msg, "text/plain", "InstituteHourSummary.xls");
        }



    }
}
