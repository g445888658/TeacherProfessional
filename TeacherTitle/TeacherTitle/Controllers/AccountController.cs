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

namespace TeacherTitle.Controllers
{

    public class AccountController : Controller
    {
        public IUserService UserService { get; set; }
        public IBaseService BaseService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (UserService == null)
                UserService = new UserService();
            if (BaseService == null)
                BaseService = new BaseService();
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

            List<KeyValueModel> AllTitle = new List<KeyValueModel>()
            {

            };

        }

        /// <summary>
        /// 移除cookie
        /// </summary>
        private void RemoveCookie()
        {
            HttpCookie cookie = new HttpCookie(userCookie);
            cookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Response.Cookies.Add(cookie);
        }


        private const string userCookie = "userInfoCookie";

        /// <summary>
        /// GET 登陆
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ActionResult LogOn(UserModel userModel)
        {
            if (HttpContext.Request.Cookies[userCookie] != null)
            {
                var user = UserService.LogOn(HttpContext.Request.Cookies[userCookie]["UserName"], HttpContext.Request.Cookies[userCookie]["PassWord"]);
                userModel.userModel = user;
            }
            return View();
        }


        /// <summary>
        /// POST 登陆
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserService.LogOn(model.UserName, model.Password);
                if (user != null)
                {
                    //记住我
                    if (model.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie(userCookie);
                        cookie.Values["UserName"] = user.U_Account;
                        cookie.Values["PassWord"] = user.U_PassWord;
                        cookie.Expires = DateTime.Now.AddDays(7);
                        HttpContext.Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        if (HttpContext.Request.Cookies[userCookie] != null)
                        {
                            RemoveCookie();
                        }
                    }
                    userModel.userModel = user;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("UserName", "提供的用户名或密码不正确");
                    return View(model);
                }
            }
            else
                // 如果我们进行到这一步时某个地方出错，则重新显示表单
                return View(model);
        }

        /// <summary>
        /// GET 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            RemoveCookie();

            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// GET 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            SetRegisterUI();
            return View();
        }


        /// <summary>
        /// POST 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //// 尝试注册用户
                //MembershipCreateStatus createStatus;
                //Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                //if (createStatus == MembershipCreateStatus.Success)
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                //}
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        /// <summary>
        /// GET 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }


        /// <summary>
        /// POST 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // 在某些出错情况下，ChangePassword 将引发异常，
                // 而不是返回 false。
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "当前密码不正确或新密码无效。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }


    }
}
