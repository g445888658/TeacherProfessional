using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TeacherTitle.DAL.DB;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.Model;


namespace TeacherTitle.Models
{
    public class AddActivityModels
    {
        /// <summary>
        /// 活动形式
        /// </summary>
        [Required(ErrorMessage = "请填写活动形式")]
        [Display(Name = "活动形式")]
        public string ActForm { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "请填写活动主题")]
        [Display(Name = "活动主题")]
        public string ActTheme { get; set; }

        /// <summary>
        /// 主讲人
        /// </summary>
        [Display(Name = "主讲人")]
        public string ActSpeaker { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "请填写开始时间")]
        [Display(Name = "开始时间")]
        public string ActStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "请填写结束时间")]
        [Display(Name = "结束时间")]
        public string ActEndTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "请填写学时")]
        [Display(Name = "学时")]
        public string ActClassHour { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        [Required(ErrorMessage = "请填写活动地点")]
        [Display(Name = "活动地点")]
        public string ActPlace { get; set; }

        /// <summary>
        /// 限制人数
        /// </summary>
        //[Required(ErrorMessage = "请填写限制人数")]
        [Display(Name = "限制人数")]
        public string ActLimit { get; set; }

        /// <summary>
        /// 候选人数
        /// </summary>
        //[Required(ErrorMessage = "请填写候选人数")]
        [Display(Name = "候选人数")]
        public string ActCandidate { get; set; }
    }


    /// <summary>
    /// 所有的学时计算标准
    /// </summary>
    public class ActivityListModels
    {
        /// <summary>
        /// 校内
        /// </summary>
        public TeachingActivity[] InActivity { get; set; }

        /// <summary>
        /// 校外
        /// </summary>
        public TeachingActivity[] OutActivity { get; set; }
    }

    /// <summary>
    /// 学时计算标准
    /// </summary>
    public class ClaHourCountStandardModels
    {
        /// <summary>
        /// 活动形式
        /// </summary>
        [Required(ErrorMessage = "请填写活动形式")]
        [Display(Name = "活动形式：")]
        public string TA_Form { get; set; }

        /// <summary>
        /// 组织方式
        /// </summary>
        [Required(ErrorMessage = "请填写组织方式")]
        [Display(Name = "组织方式：")]
        public string TA_Way { get; set; }

        /// <summary>
        /// 呈现材料
        /// </summary>
        [Required(ErrorMessage = "请填写呈现材料")]
        [Display(Name = "呈现材料：")]
        public string TA_Material { get; set; }


        /// <summary>
        /// 学时
        /// </summary>
        [Required(ErrorMessage = "请填写学时计算")]
        [Display(Name = "学时计算：")]
        public string TA_ClassHour { get; set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        [Required(ErrorMessage = "请选择活动地点")]
        [Display(Name = "活动地点：")]
        public string TA_PlaceKey { get; set; }

        /// <summary>
        /// 活动地点值
        /// </summary>
        [Required(ErrorMessage = "请填写活动地点")]
        [Display(Name = "活动地点：")]
        public string TA_PlaceValue { get; set; }
    }

   

}