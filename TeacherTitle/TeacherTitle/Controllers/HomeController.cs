using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.BAL.Service;
using TeacherTitle.Models;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.Controllers
{
    public class HomeController : Controller
    {
        IActivityService ActivityService { get; set; }
        IBaseService BaseService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (ActivityService == null)
                ActivityService = new ActivityService();
            if (BaseService == null)
                BaseService = new BaseService();
            base.Initialize(requestContext);
        }

        /// <summary>
        /// GET 显示首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET 显示内容
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Main()
        {
            return PartialView();
        }


        /// <summary>
        /// GET 最新活动公告
        /// </summary>
        /// <returns></returns>
        public PartialViewResult LatestNotice()
        {
            ViewData["Notice"] = ActivityService.GetAdmActivityPlan();
            return PartialView();
        }

        /// <summary>
        /// POST 最新活动公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult LatestNotice(string page)
        {
            ViewData["Notice"] = ActivityService.GetAdmActivityPlan();
            return PartialView();
        }

        /// <summary>
        /// GET 设置系统邮箱
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SysMail()
        {
            var result = BaseService.GetMailConfig();
            return PartialView(new SysMailConfigModel()
            {
                SysMail = result.SC_FieldName,
                SysMailPsw = result.SC_FieldAttr
            });
        }

        /// <summary>
        /// POST 邮箱设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult SysMail(SysMailConfigModel model)
        {
            if (ModelState.IsValid)
            {
                SysConfig sysConfig = new SysConfig()
                {
                    SC_FieldName = model.SysMail,
                    SC_FieldAttr = model.SysMailPsw,
                    SC_FieldCode = 1
                };
                var args = BaseService.SetSysMail(sysConfig);
                ViewData["result"] = args.Msg;
                return PartialView("_ResultPartial");
            }
            return PartialView();
        }

    }
}
