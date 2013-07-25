using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Routing;
using TeacherTitle.Models;
using System.Web.Security;

namespace TeacherTitle.Infrastructure
{
    public class ValidationModel
    {
        /// <summary>
        /// 验证两个输入是否一致
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
        public sealed class PropertiesMatchAttribute : ValidationAttribute
        {
            private const string _defaultErrorMessage = "'{0}' 和 '{1}' 输入不一致.";
            private readonly object _typeId = new object();

            /// <summary>
            /// 验证两个输入是否一致
            /// </summary>
            /// <param name="originalProperty"></param>
            /// <param name="confirmProperty"></param>
            public PropertiesMatchAttribute(string originalProperty, string confirmProperty)
                : base(_defaultErrorMessage)
            {
                OriginalProperty = originalProperty;
                ConfirmProperty = confirmProperty;
            }

            public string ConfirmProperty { get; private set; }

            public string OriginalProperty { get; private set; }

            public override object TypeId
            {
                get
                {
                    return _typeId;
                }
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                    OriginalProperty, ConfirmProperty);
            }

            public override bool IsValid(object value)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
                object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
                object confirmValue = ConfirmProperty;
                return Object.Equals(originalValue, confirmValue);
            }
        }

        /// <summary>
        /// 将用户ID存到cookie中
        /// </summary>
        /// <param name="userID"></param>
        public void SetUserCookie(string userID)
        {
            //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
            //    1,
            //    userID,
            //    DateTime.Now,
            //    DateTime.Now.AddMinutes(30),//登陆时间30分到期
            //    false,
            //    "");

            //string encTicket = FormsAuthentication.Encrypt(authTicket);

            //HttpContext.
        }

        ///// <summary>
        ///// 验证用户是否已登录,该方法缺点是只能是单用户登陆
        ///// </summary>
        //public sealed class LoginValidate : ActionFilterAttribute
        //{
        //    public override void OnActionExecuting(ActionExecutingContext filterContext)
        //    {
        //        var userModel = (UserModel)filterContext.HttpContext.Session["_userModel"];
        //        if (userModel == null || userModel.userModel == null)
        //        {
        //            filterContext.HttpContext.Response.Write("<script type='text/javascript'> $('#ui_dialog').dialog({modal: true,show: 'blind',hide: 'explode',resizable: false});</script>");
        //        }
        //    }
        //}


    }
}