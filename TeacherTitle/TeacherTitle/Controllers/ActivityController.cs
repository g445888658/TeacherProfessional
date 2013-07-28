using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherTitle.DAL.DB;
using TeacherTitle.BAL.Infrastructure;
using TeacherTitle.BAL.Service;
using TeacherTitle.Models;
using TeacherTitle.Infrastructure;

namespace TeacherTitle.Controllers
{
    public class ActivityController : Controller
    {
        IActivityService ActivityService { get; set; }
        IClassHourService ClassHourService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (ActivityService == null)
                ActivityService = new ActivityService();
            if (ClassHourService == null)
                ClassHourService = new ClassHourService();
            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据关键字获取活动形式
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetActForm(string keyword)
        {
            var result = ActivityService.GetActForm(keyword);
            var jsonData = result.Select(x => new
            {
                Key = x.Key,
                Value = x.Value
            });
            return Json(jsonData);
        }

        /// <summary>
        /// GET 教学活动列表
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ActivityList()
        {
            ActivityListModels activityListModels = new Models.ActivityListModels();
            activityListModels.InActivity = ActivityService.GetInActivity();
            activityListModels.OutActivity = ActivityService.GetOutActivity();
            ViewData["activityList"] = activityListModels;
            ViewData["activityRemark"] = ActivityService.GetActivityRemark();
            return PartialView();
        }


        /// <summary>
        /// GET 申请参加活动
        /// </summary>
        /// <param name="apCode"></param>
        /// <returns></returns>
        public PartialViewResult ApplyActivity(string apCode)
        {
            var act = ActivityService.GetActivityPlansById(Convert.ToInt32(apCode));
            ViewData["ActivityDetail"] = act;
            ViewData["ActForm"] = ActivityService.GetAllActForm().FirstOrDefault(x => x.Key == act.Plan.TA_Code.ToString()).Value;
            return PartialView();
        }

        /// <summary>
        ///  文件下载
        /// </summary>
        /// <param name="AA_Code"></param>
        /// <returns></returns>
        public FileResult DownloadFile(string AA_Code)
        {
            var file = ActivityService.GetActAttachmentById(Convert.ToInt32(AA_Code));
            return File(file.AA_Path, "text/plain", file.AA_Name);
        }


        /// <summary>
        /// POST 申请参加活动
        /// </summary>
        /// <param name="apCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApplyActivity(string apCode, string _U_Code)
        {
            //已经登陆就可以申请参加活动
            if (!string.IsNullOrEmpty(_U_Code))
            {
                var flag = true;
                string msg = string.Empty;
                var result = ActivityService.GetActSignUpByUCodeAPCode(Convert.ToInt32(apCode), Convert.ToInt32(_U_Code));
                if (result != null)//已报名
                {
                    if (result.ASU_StatusKey == 0)//被剔除
                    {
                        flag = false;
                        msg = "由于你被管理员从该活动的报名表中剔除,所以不能申请";
                    }
                    else
                    {
                        flag = false;
                        msg = "已报名";
                    }
                }
                else
                {
                    var activityDeail = ActivityService.GetActivityPlanById(Convert.ToInt32(apCode));
                    if (activityDeail.AP_StatusKey == 0)//报名结束
                    {
                        flag = false;
                        msg = "报名已结束";
                    }
                    else if (activityDeail.AP_CandidateLeft == 0)//候选名额也满了的话就报不进了
                    {
                        flag = false;
                        msg = "名额已满";
                    }
                    if (flag)
                    {
                        //是否为候选名额
                        bool isCandidate = false;
                        ActivitySignUp activitySignUp = new ActivitySignUp()
                        {
                            U_Code = Convert.ToInt32(_U_Code),
                            AP_Code = Convert.ToInt32(apCode),
                            ASU_IsCandidateKey = 0,
                            ASU_IsCandidateVal = "正式",
                            ASU_StatusKey = 1,
                            ASU_StatusValue = "可用"
                        };

                        msg = "申请成功";

                        if (activityDeail.AP_Left == 0)//限制名额满了后在报名就会在候选名额里
                        {
                            isCandidate = true;
                            msg = "正式名额已满，你现在被安排在候选名额里";
                            activitySignUp.ASU_IsCandidateKey = 1;
                            activitySignUp.ASU_IsCandidateVal = "候补";
                        }
                        var args = ActivityService.TeacherSignUp(activitySignUp);//报名
                        if (args.Flag)
                            args = ActivityService.AlterTeacherSignUp(Convert.ToInt32(apCode), isCandidate);//减少一个名额
                        flag = args.Flag;

                    }
                }
                var jsonData = new
                {
                    Flag = flag,
                    Msg = msg
                };
                return Json(jsonData);
            }
            ViewData["ActivityDetail"] = ActivityService.GetActivityPlanById(Convert.ToInt32(apCode));
            ViewData["ActForm"] = ActivityService.GetAllActForm().FirstOrDefault(x => x.Key == apCode).Value;
            return PartialView();
        }


        /// <summary>
        ///GET 教师活动列表
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ApplyList(SearchClassHourModelsl model)
        {
            var result = ClassHourService.GetTeacherJoinActivity(model.Teacher, false, model.ActivityForm, model.StartTime, model.EndTime, false);
            ViewData["applyList"] = result;
            return PartialView();
        }

        /// <summary>
        ///POST 教师活动列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ApplyList(ClassHourSumModel classHourSumModel)
        {
            var msg = "请填写完整";
            if (ModelState.IsValid)
            {
                var result = ClassHourService.GetClaHourInfoByASU_Code(Convert.ToInt32(classHourSumModel.SignUpCode));
                if (result != null)
                {
                    var classHour = ClassHourService.AlterClaHourInfo(Convert.ToInt32(classHourSumModel.SignUpCode), Convert.ToInt32(classHourSumModel.ClassHour));
                    if (classHour != null)
                        msg = "修改成功";
                }
                else
                {
                    ClassHourSum classHourSum = new ClassHourSum()
                    {
                        ASU_Code = Convert.ToInt32(classHourSumModel.SignUpCode),
                        CH_GetTime = classHourSumModel.GetTime,
                        CH_GetHour = Convert.ToInt32(classHourSumModel.ClassHour)
                    };
                    var args = ClassHourService.AddClassHourSum(classHourSum);
                    if (args.Flag)
                        msg = "操作成功";
                    else
                        msg = "操作失败";
                }

            }
            var jsonData = new
            {
                Msg = msg
            };
            return Json(jsonData);
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
        public JsonResult RejectApply(string SignUpCode, string AP_Code, string IsReplace)
        {
            var args = ActivityService.RejectApply(Convert.ToInt32(SignUpCode));
            args = ActivityService.AfterRejectApply(Convert.ToInt32(AP_Code), Convert.ToInt32(SignUpCode), Convert.ToInt32(IsReplace));
            var jsonData = new
            {
                Flag = args.Flag,
                Msg = args.Msg
            };
            return Json(jsonData);
        }

        /// <summary>
        /// 用户参与的活动（包括得到学时和没得到的）
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ApplyListForUser(string U_Code)
        {
            if (!string.IsNullOrEmpty(U_Code))
            {
                var result = ClassHourService.GetTeacherJoinActivity(U_Code, true, null, null, null, false);
                ViewData["applyList"] = result;
            }
            return PartialView();
        }


        /// <summary>
        /// 搜索条件
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherPerApply()
        {
            return PartialView();
        }

        /// <summary>
        /// 教师申请列表(没有取得学时)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult TeacherPerApply(SearchClassHourModelsl model)
        {
            var list = ClassHourService.GetTeacherApplyActivity(model.Teacher, false, model.ActivityForm, model.StartTime, model.EndTime, false);
            ViewData["applyList"] = list;
            return PartialView("ApplyList");
        }

        /// <summary>
        ///GET 搜索条件
        /// </summary>
        /// <returns></returns>
        public PartialViewResult TeacherPerApplyFinish()
        {
            return PartialView();
        }

        /// <summary>
        ///POST 查询教师申请的活动并且已经获取学时
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult TeacherPerApplyFinish(SearchClassHourModelsl model)
        {
            var list = ClassHourService.GetTeacherApplyActivity(model.Teacher, false, model.ActivityForm, model.StartTime, model.EndTime, true);
            ViewData["PerApplyFinishList"] = list;
            return PartialView("ApplyFinishTable");
        }

        /// <summary>
        /// 修改老师获得的学时
        /// </summary>
        /// <param name="ASUCode"></param>
        /// <param name="ClassHour"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ApplyFinishTable(string ASUCode, string ClassHour)
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
