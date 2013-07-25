using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TeacherTitle.DAL.Model;

namespace TeacherTitle.Models
{
    /// <summary>
    /// 学时授予
    /// </summary>
    public class ClassHourSumModel
    {
        /// <summary>
        /// 报名编号
        /// </summary>
        [Required]
        public string SignUpCode { get; set; }

        /// <summary>
        /// 取得时间
        /// </summary>
        [Required(ErrorMessage = "请填写取得时间")]
        [Display(Name = "取得时间：")]
        public string GetTime { get; set; }

        /// <summary>
        /// 授予学时
        /// </summary>
        [Required(ErrorMessage = "请填写学时")]
        [Display(Name = "授予学时：")]
        public string ClassHour { get; set; }
    }
}