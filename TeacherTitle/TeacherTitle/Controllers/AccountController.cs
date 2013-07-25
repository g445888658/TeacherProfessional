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


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (UserService == null)
                UserService = new UserService();
            if (BaseService == null)
                BaseService = new BaseService();
            if (ActivityService == null)
                ActivityService = new ActivityService();

            base.Initialize(requestContext);
        }


        ///// <summary>
        ///// 移除cookie
        ///// </summary>
        //private void RemoveCookie()
        //{
        //    HttpCookie cookie = new HttpCookie(userCookie);
        //    cookie.Expires = DateTime.Now.AddDays(-1d);
        //    HttpContext.Response.Cookies.Add(cookie);
        //}

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
            //if (!string.IsNullOrEmpty(U_Code))
            //{
            //    UserModel userModel = new UserModel();

            //    userModel.userModel = UserService.GetUserByID(Convert.ToInt32(U_Code));

            //    return PartialView("_LogOutPartial", userModel);
            //}
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
            //FormsAuthentication.SignOut();
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
                    var sysMail = BaseService.GetMailConfig();
                    MailModel mailModel = new Models.MailModel()
                    {
                        MailFrom = new MailAddress(sysMail.SC_FieldName, "管理员"),
                        Password = sysMail.SC_FieldAttr,
                        MailTo = ValidateMail,
                        Subject = "密码重置成功",
                        Body = "恭喜你，密码重置成功，新密码为你的账号",
                        Prority = MailPriority.Normal,
                    };
                    try
                    {
                        MailHelper.SendMail(mailModel);
                    }
                    catch
                    {

                        flag = false;
                        msg = "邮件发送失败，找回密码失败";
                    }
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
        public PartialViewResult UserInfo(string U_Code, string U_Mail, string U_Phone)
        {
            var args = UserService.ChangeUserInfo(Convert.ToInt32(U_Code), U_Mail, U_Phone);
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
            return PartialView();
        }

        /// <summary>
        /// POST 用户申请活动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult UserApplyActivity(AddActivityModels addActivityModels, string U_Code)
        {
            if (ModelState.IsValid)
            {
                ActivityPlan ActivityPlan = new DAL.DB.ActivityPlan()
                {
                    TA_Code = Convert.ToInt32(addActivityModels.ActForm),
                    AP_Theme = addActivityModels.ActTheme,
                    AP_Speaker = addActivityModels.ActSpeaker,
                    AP_StartTime = addActivityModels.ActStartTime,
                    AP_EndTime = addActivityModels.ActEndTime,
                    AP_Place = addActivityModels.ActPlace,
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


    }
}
