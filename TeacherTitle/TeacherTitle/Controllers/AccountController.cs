using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TeacherTitle.Infrastructure;
using TeacherTitle.Models;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.BAL.Service;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;
using System.Net.Mail;

namespace TeacherTitle.Controllers
{

    public class AccountController : Controller
    {
        IUserService UserService { get; set; }
        IBaseService BaseService { get; set; }
        IActivityService ActivityService { get; set; }
        IClassHourService ClassHourService { get; set; }
        IExcelService ExcelService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (UserService == null)
                UserService = new UserService();
            if (BaseService == null)
                BaseService = new BaseService();
            if (ActivityService == null)
                ActivityService = new ActivityService();
            if (ClassHourService == null)
                ClassHourService = new ClassHourService();
            if (ExcelService == null)
                ExcelService = new ExcelService();
            base.Initialize(requestContext);
        }


        /// <summary>
        /// 根据关键字查找教师
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetUsers(string keyword)
        {
            var users = UserService.GetUsers(keyword);

            var jsonData = users.Select(x => new
            {
                Key = x.U_Code,
                Value = x.U_Name
            });
            return Json(jsonData);
        }

        private const string userCookie = "userInfoCookie";

        /// <summary>
        /// GET 登陆
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public PartialViewResult LogOn()
        {
            return PartialView("_LogOnPartial");
        }

        /// <summary>
        /// POST 登陆
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserService.LogOn(model.UserName, model.Password);
                if (user != null)
                {
                    UserModel userModel = new UserModel();
                    userModel.userModel = user;
                    return PartialView("_LogOutPartial", userModel);
                }
                else
                {
                    ModelState.AddModelError("UserName", "提供的用户名或密码不正确");
                    return PartialView("_LogOnPartial");
                }
            }
            else
                // 如果我们进行到这一步时某个地方出错，则重新显示表单
                return PartialView("_LogOnPartial");
        }

        /// <summary>
        /// GET 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            return PartialView("_LogOnPartial");
        }

        /// <summary>
        /// GET 用户操作列表
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UserMain(string U_Code)
        {
            ViewData["U_Code"] = U_Code;
            return PartialView();
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public FileResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            byte[] bytes = vCode.CreateValidateGraphic(code);
            Session["ValidateCode"] = code;
            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// GET 找回密码
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ForgetPassword()
        {
            return PartialView();
        }

        /// <summary>
        /// GET 找回密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ForgetPassword(string ValidateMail, string ValidateCode)
        {
            var flag = true;
            var msg = "重置后的密码已发至您的邮箱";
            if (Session["ValidateCode"].ToString() != ValidateCode)
            {
                msg = "验证码错误";
                flag = false;
            }
            var args = UserService.ForgetPassword(ValidateMail);
            if (!args.Flag)
            {
                msg = "该邮箱未注册";
                flag = false;
            }
            //发送邮件
            if (flag)
            {
                args = UserService.ResetPassword(ValidateMail);
                //修改密码成功
                if (args.Flag)
                {
                    //var sysMail = BaseService.GetMailConfig();
                    //MailModel mailModel = new Models.MailModel()
                    //{
                    //    MailFrom = new MailAddress(sysMail.SC_FieldName, "管理员"),
                    //    Password = sysMail.SC_FieldAttr,
                    //    MailTo = ValidateMail,
                    //    Subject = "密码重置成功",
                    //    Body = "恭喜你，密码重置成功，新密码为你的账号",
                    //    Prority = MailPriority.Normal,
                    //};
                    //try
                    //{
                    //    MailHelper.SendMail(mailModel);
                    //}
                    //catch
                    //{

                    //    flag = false;
                    //    msg = "邮件发送失败，找回密码失败";
                    //}
                }
                else
                {
                    flag = false;
                    msg = args.Msg;
                }
            }
            var jsonData = new
            {
                Flag = flag,
                Msg = msg
            };

            return Json(jsonData);
        }

        /// <summary>
        /// GET 修改密码
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ChangePassword(string U_Code)
        {
            ViewData["U_Code"] = U_Code;
            return PartialView();
        }


        /// <summary>
        /// POST 修改密码
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult ChangePassword(ChangePasswordModel changePasswordModel, string U_Code)
        {
            if (ModelState.IsValid)
            {
                var args = UserService.ChangePassword(Convert.ToInt32(U_Code), changePasswordModel.OldPassword, changePasswordModel.NewPassword);
                ViewData["result"] = args.Msg;
                return PartialView("_ResultPartial");
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return PartialView(changePasswordModel);
        }

        /// <summary>
        /// GET 用户信息
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UserInfo(string U_Code)
        {
            UserModel userModel = new UserModel()
            {
                userModel = UserService.GetUserByID(Convert.ToInt32(U_Code))
            };
            ViewData["UserInfo"] = userModel;
            return PartialView();
        }

        /// <summary>
        /// POST 用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult UserInfo(string U_Code, string U_LongPhone, string U_ShortPhone, string U_Mail, string U_QQNum)
        {
            //未完成
            var args = UserService.ChangeUserInfo(Convert.ToInt32(U_Code), U_LongPhone, U_ShortPhone, U_Mail, U_QQNum);
            ViewData["result"] = args.Msg;
            return PartialView("_ResultPartial");
        }



        /// <summary>
        /// GET 用户申请活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UserApplyActivity(string U_Code)
        {
            ViewData["AllActForm"] = Basehandle.ProduceSelectList(ActivityService.GetAllActForm());
            ViewData["U_Code"] = U_Code;
            ViewData["Year"] = Basehandle.ProduceSelectList(Basehandle.GetSchoolYear());
            return PartialView();
        }

        /// <summary>
        /// POST 用户申请活动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult UserApplyActivity(ApplyActivityModels applyActivityModels, string U_Code, UploadModels uploadModels)
        {
            if (ModelState.IsValid)
            {
                ActivityPlan ActivityPlan = new DAL.DB.ActivityPlan()
                {
                    TA_Code = Convert.ToInt32(applyActivityModels.ActForm),
                    AP_Theme = applyActivityModels.ActTheme,
                    AP_Speaker = applyActivityModels.ActSpeaker,
                    AP_SchoolYear = applyActivityModels.ActSchoolYear,
                    AP_StartTime = applyActivityModels.ActStartTime,
                    AP_EndTime = applyActivityModels.ActEndTime,
                    AP_Place = applyActivityModels.ActPlace,
                    AP_ClassHour = applyActivityModels.ActClassHour,
                    AP_ReleaseTime = DateTime.Now.ToString(),
                    AP_StatusKey = 2,
                    AP_StatusValue = "教师申请"
                };

                var args = ActivityService.AddActivityPlan(ActivityPlan);


                if (args.Flag)
                {
                    ActivitySignUp activitySignUp = new DAL.DB.ActivitySignUp()
                    {
                        U_Code = Convert.ToInt32(U_Code),
                        AP_Code = ActivityPlan.AP_Code,
                        ASU_IsCandidateKey = 0,
                        ASU_IsCandidateVal = "正式",
                        ASU_StatusKey = 1,
                        ASU_StatusValue = "可用"
                    };
                    args = ActivityService.TeacherSignUp(activitySignUp);



                    if (uploadModels.FileName != null)
                    {
                        for (int i = 0; i < uploadModels.FileName.Length; i++)
                        {
                            ActivityMaterial activityMaterial = new ActivityMaterial()
                            {
                                ASU_Code = activitySignUp.ASU_Code,
                                AM_Name = uploadModels.FileName[i],
                                AM_SavePath = uploadModels.SaveName[i]
                            };
                            args = ActivityService.AddActivityMaterial(activityMaterial);
                        }
                    }

                    //ClassHourSum classHourSum = new ClassHourSum()
                    //{
                    //    ASU_Code = activitySignUp.ASU_Code,
                    //    CH_GetTime = DateTime.Now.ToString("yyyy/MM/dd"),
                    //    CH_GetHour = Convert.ToInt32(applyActivityModels.ActClassHour)
                    //};
                    //ClassHourService.AddClassHourSum(classHourSum);

                    ViewData["result"] = args.Msg;
                    return PartialView("_ResultPartial");
                }
                else
                {
                    ViewData["result"] = args.Msg;
                    return PartialView("_ResultPartial");
                }

            }
            ViewData["AllActForm"] = Basehandle.ProduceSelectList(ActivityService.GetAllActForm());
            return PartialView();
        }

        /// <summary>
        /// 用户上传文件
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="U_Code"></param>
        /// <returns></returns>
        public ContentResult Uploadfile(HttpPostedFileBase fileData, string U_Code, string ASU_Code)
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

            ActivityMaterial activityMaterial = new ActivityMaterial()
            {
                ASU_Code = Convert.ToInt32(ASU_Code),
                AM_Name = fileData.FileName,
                AM_SavePath = result
            };

            var args = ActivityService.AddActivityMaterial(activityMaterial);

            return Content(result);
        }


        /// <summary>
        /// 教师已报名的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AlreadySignUp(String U_Code)
        {
            var list = ActivityService.GetAlreadySignUp(Convert.ToInt32(U_Code), false);
            ViewData["list"] = list;
            return PartialView();
        }

        /// <summary>
        /// 进行中的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Ongoing(String U_Code)
        {
            var list = ActivityService.GetSignUp(Convert.ToInt32(U_Code), false, true, 3);
            ViewData["list"] = list;
            return PartialView();
        }

        /// <summary>
        /// 查询已结束的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AlreadyEndSearch()
        {
            ViewData["AllInstitute"] = Basehandle.ProduceInstituteList(BaseService.GetAllInstitute());
            return PartialView();
        }

        /// <summary>
        /// 已结束的活动
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AlreadyEnd(String U_CodePer, string StartTime, string EndTime)
        {
            var list = ActivityService.GetSignUp(Convert.ToInt32(U_CodePer), false, true, 0);
            list = ActivityService.TeacherPerApplyFinishSearch(list, null, null, null, StartTime, EndTime);
            ViewData["list"] = list;
            return PartialView();
        }

        /// <summary>
        /// 教师参加的活动导出
        /// </summary>
        /// <param name="U_CodePer"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public FileResult ExportAlreadyEnd(String U_CodePer, string StartTime, string EndTime)
        {
            var list = ActivityService.GetSignUp(Convert.ToInt32(U_CodePer), false, true, 0);
            list = ActivityService.TeacherPerApplyFinishSearch(list, null, null, null, StartTime, EndTime);
            string filePath = Server.MapPath("~/ExcelFile/InstituteHourSummary.xls");
            var args = ExcelService.creatExcelFileInstitute(filePath, null, list, StartTime, EndTime);

            return File(args.Msg, "text/plain", "InstituteHourSummary.xls");
        }

        /// <summary>
        /// 教师申请活动
        /// </summary>
        /// <param name="U_Code"></param>
        /// <returns></returns>
        public PartialViewResult TeacherApply(String U_Code)
        {
            var list = ActivityService.GetSignUp(Convert.ToInt32(U_Code), true, false, 2);
            ViewData["list"] = list;
            return PartialView();
        }

        /// <summary>
        /// 教师已审批活动导出
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherApplyFinishSearch()
        {
            return PartialView();
        }

        /// <summary>
        /// 已审批的教师申请
        /// </summary>
        /// <param name="U_Code"></param>
        /// <returns></returns>
        public PartialViewResult TeacherApplyFinish(String U_CodePerFinish, SearchClassHourModelsl model)
        {
            var list = ActivityService.GetSignUp(Convert.ToInt32(U_CodePerFinish), true, true, 4);
            ViewData["list"] = list;
            return PartialView();
        }

        /// <summary>
        /// 导出已审批的教师申请
        /// </summary>
        /// <param name="U_CodePerFinish"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public FileResult ExportApplyFinish(String U_CodePerFinish, string StartTime, string EndTime)
        {
            var list = ActivityService.GetSignUp(Convert.ToInt32(U_CodePerFinish), true, true, 4);
            list = ActivityService.TeacherPerApplyFinishSearch(list, null, null, null, StartTime, EndTime);
            string filePath = Server.MapPath("~/ExcelFile/InstituteHourSummary.xls");
            var args = ExcelService.creatExcelFileInstitute(filePath, null, list, StartTime, EndTime);

            return File(args.Msg, "text/plain", "InstituteHourSummary.xls");
        }

        /// <summary>
        /// 教师活动资料上传
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        public ContentResult UploadMaterial(HttpPostedFileBase fileData, string U_Code, string ASU_Codes)
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
            ActivityMaterial activityMaterial = new ActivityMaterial()
            {
                ASU_Code = Convert.ToInt32(ASU_Codes),
                AM_Name = fileData.FileName,
                AM_SavePath = result
            };
            var args = ActivityService.AddActivityMaterial(activityMaterial);
            return Content(args.Msg);
        }

        /// <summary>
        /// 个人学时统计查询
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PerClassHourSum()
        {
            ViewData["SearchYear"] = Basehandle.ProduceSelectList(BaseService.GetAllSchoolYear());
            return PartialView();
        }

        /// <summary>
        /// 个人学时统计查询结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PartialViewResult PerClassHourSumDetail(PerSearchClassHourModelsl model)
        {
            model.Account = UserService.GetUserByID(Convert.ToInt32(model.Account)).U_Account;

            var result = ClassHourService.PerClassHourSum(model.Teacher, model.Account, model.StartYear, model.EndYear);
            ViewData["ClassHourResult"] = result;
            return PartialView();
        }

        /// <summary>
        /// 导出个人学时统计
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="StartYear"></param>
        /// <param name="EndYear"></param>
        /// <returns></returns>
        public FileResult ExportPerClassHourSumDetail(string Account, string StartYear, string EndYear)
        {
            Account = UserService.GetUserByID(Convert.ToInt32(Account)).U_Account;
            var result = ClassHourService.PerClassHourSum(null, Account, StartYear, EndYear);

            string filePath = Server.MapPath("~/ExcelFile/PersonalSummary.xls");
            var args = ExcelService.creatExcelFilePerson(filePath, result);

            return File(args.Msg, "text/plain", "PersonalSummary.xls");
        }

    }
}
