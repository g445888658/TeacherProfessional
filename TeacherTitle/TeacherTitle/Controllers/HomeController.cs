using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.BAL.Service;
using TeacherTitle.Models;
using TeacherTitle.DAL.DB;
using TeacherTitle.Infrastructure;

namespace TeacherTitle.Controllers
{
    public class HomeController : Controller
    {
        PageInfo pageInfo = new PageInfo();

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
            var list = ActivityService.GetAdmActivityPlan();

            pageInfo.TotalItems = list.Length;
            pageInfo.ItemsPerPage = 10;
            pageInfo.CurrentPage = 1;
            ViewData["PageInfo"] = pageInfo;
            var asd = list.Take(pageInfo.ItemsPerPage).ToArray();
            ViewData["Notice"] = list.Take(pageInfo.ItemsPerPage).ToArray();
            return PartialView();
        }

        /// <summary>
        /// POST 最新活动公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult LatestNotice(string page)
        {
            var list = ActivityService.GetAdmActivityPlan();
            pageInfo.TotalItems = list.Length;
            pageInfo.ItemsPerPage = 10;
            pageInfo.CurrentPage = Convert.ToInt32(page);
            ViewData["PageInfo"] = pageInfo;
            ViewData["Notice"] = list.Skip(pageInfo.ItemsPerPage * (Convert.ToInt32(page) - 1)).Take(pageInfo.ItemsPerPage).ToArray();
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

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        public ContentResult Upload(HttpPostedFileBase fileData, string U_Code)
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
            return Content(result);
        }


    }
}
