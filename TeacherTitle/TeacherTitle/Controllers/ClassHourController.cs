using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.BAL.Service;
using TeacherTitle.Models;

namespace TeacherTitle.Controllers
{
    public class ClassHourController : Controller
    {
        IClassHourService ClassHourService { get; set; }
        IUserService UserService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (ClassHourService == null)
                ClassHourService = new ClassHourService();
            if (UserService == null)
                UserService = new UserService();
            base.Initialize(requestContext);
        }

        /// <summary>
        /// GET 根据条件查询老师已获取的学时
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SearchCriteria()
        {
            return PartialView();
        }

        /// <summary>
        /// POST 根据条件查询老师已获取的学时
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult SearchCriteria(SearchClassHourModelsl model)
        {
            var result = ClassHourService.GetTeacherJoinActivity(model.Teacher, model.ActivityForm, model.StartTime, model.EndTime, true);
            ViewData["resultList"] = result;
            return PartialView("ClassHourTable");
        }

        /// <summary>
        /// POST 修改教师获得的学时
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClassHourTable(string ASUCode, string ClassHour)
        {
            var args = ClassHourService.AlterClaHourInfo(Convert.ToInt32(ASUCode), Convert.ToInt32(ClassHour));
            string msg = string.Empty;
            if (!args.Flag)
                msg = "修改失败";
            else
                msg = "修改成功";
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = msg
            };
            return Json(jsonData);
        }




    }
}
