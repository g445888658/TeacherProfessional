using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using TeacherTitle.Infrastructure;

namespace TeacherTitle.Models
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "请输入新密码")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// 登陆
    /// </summary>
    public class LogOnModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我一周?")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// 注册
    /// </summary>
    public class RegisterModel
    {
        [Required(ErrorMessage = "请填写账号")]
        [Display(Name = "账号")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请填写密码")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "请填写用户名")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请选择学院")]
        [Display(Name = "学院")]
        public string Institute{ get; set; }

        [Required(ErrorMessage = "请选择专业")]
        [Display(Name = "专业")]
        public string Major { get; set; }

        [Required]
        [HiddenInput]
        [Display(Name = "类型")]
        public string Type { get; set; }

        [Required(ErrorMessage="请填写学历")]
        [Display(Name = "学历")]
        public string Degree { get; set; }


        [Required(ErrorMessage = "请填写职称")]
        [Display(Name = "职称")]
        public string Title { get; set; }

        [Required(ErrorMessage="请填写电子邮件地址")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮件地址")]
        public string Email { get; set; }


        [Required(ErrorMessage = "请填写联系电话")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "联系电话")]
        public string Phone { get; set; }


        [Display(Name = "备注")]
        public string Remark { get; set; }

    }


}
