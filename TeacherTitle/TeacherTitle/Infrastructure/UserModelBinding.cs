using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherTitle.Models;

namespace TeacherTitle.Infrastructure
{
    public class UserModelBinding : IModelBinder
    {
        public const String SessionKey = "_userModel";

        #region IModelBinder 成员

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");

            UserModel userModel = (UserModel)controllerContext.HttpContext.Session[SessionKey];
            if (userModel == null)
            {
                userModel = new UserModel();
                controllerContext.HttpContext.Session[SessionKey] = userModel;
            }

            return userModel;
        }

        #endregion
    }
}