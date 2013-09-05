using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TeacherTitle.Infrastructure;

namespace TeacherTitle.Models
{
    /// <summary>
    /// 查询教师已参加的活动并获取学时的内容
    /// </summary>
    public class SearchClassHourModelsl
    {
        /// <summary>
        /// 教师
        /// </summary>
        [Display(Name = "选择教师：")]
        public string Teacher { get; set; }

        /// <summary>
        /// 职工号
        /// </summary>
        [Display(Name = "职工号：")]
        public string Account { get; set; }

        /// <summary>
        /// 学院
        /// </summary>
        [Display(Name = "学院：")]
        public string Institute { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间：")]
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间：")]
        public string EndTime { get; set; }
    }


    /// <summary>
    /// 教师个人学时统计查询
    /// </summary>
    public class PerSearchClassHourModelsl
    {
        /// <summary>
        /// 教师
        /// </summary>
        [Display(Name = "选择教师：")]
        public string Teacher { get; set; }

        /// <summary>
        /// 职工号
        /// </summary>
        [Required]
        [Display(Name = "职工号：")]
        public string Account { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "请选择开始学年")]
        [Display(Name = "开始学年：")]
        public string StartYear { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "请选择结束学年")]
        [Display(Name = "结束学年：")]
        public string EndYear { get; set; }
    }


}