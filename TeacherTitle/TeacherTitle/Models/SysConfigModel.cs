using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeacherTitle.Models
{
    public class SysMailConfigModel
    {

        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "输入的邮箱地址不合法")]
        [Required(ErrorMessage = "请填写邮箱")]
        [Display(Name = "邮箱:")]
        public string SysMail { get; set; }

        [Required(ErrorMessage = "请填写密码")]
        [Display(Name = "密码:")]
        public string SysMailPsw { get; set; }

    }
}