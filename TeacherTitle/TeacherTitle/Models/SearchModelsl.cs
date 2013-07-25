using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        /// 活动形式
        /// </summary>
        [Display(Name = "活动形式：")]
        public string ActivityForm { get; set; }

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
}