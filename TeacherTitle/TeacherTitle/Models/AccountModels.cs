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
        //[StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
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
        [Required(ErrorMessage = "请填写工号")]
        [Display(Name = "工号")]
        public string Account { get; set; }

        //[StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "密码")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "确认密码")]
        //[Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        //public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "请填写姓名")]
        [Display(Name = "姓名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请选择性别")]
        [Display(Name = "性别")]
        public string UserSex { get; set; }

        [Required(ErrorMessage = "请填写出生年月")]
        [Display(Name = "出生年月")]
        public string UserBirth { get; set; }

        [Required(ErrorMessage = "请填写民族")]
        [Display(Name = "民族")]
        public string UserNation { get; set; }

        [Required(ErrorMessage = "请选择所在院系部处")]
        [Display(Name = "所在院系部处")]
        public string Institute { get; set; }

        [Required(ErrorMessage = "请选择部门(系)")]
        [Display(Name = "部门(系)")]
        public string Department { get; set; }

        [Required(ErrorMessage = "请填写来校年月")]
        [Display(Name = "来校年月")]
        public string UserSchoolTime { get; set; }

        [Required(ErrorMessage = "请填写参加工作年月")]
        [Display(Name = "参加工作年月")]
        public string UserWorkTime { get; set; }

        [Required(ErrorMessage = "请填写现从事学科")]
        [Display(Name = "现从事学科")]
        public string UserSubject { get; set; }

        [Required(ErrorMessage = "请填写从事专业")]
        [Display(Name = "从事专业")]
        public string UserMajor { get; set; }

        [Required(ErrorMessage = "请填写研究方向")]
        [Display(Name = "研究方向")]
        public string UserResearch { get; set; }

        [Required(ErrorMessage = "请填写专业技术资格")]
        [Display(Name = "专业技术资格")]
        public string UserTitle { get; set; }

        [Required(ErrorMessage = "请填写资格级别")]
        [Display(Name = "资格级别")]
        public string UserTitleLevel { get; set; }

        [Required(ErrorMessage = "请填写专业技术职务聘任时间")]
        [Display(Name = "专业技术职务聘任时间")]
        public string UserEngageTime { get; set; }

        [Required(ErrorMessage = "请填写是否双职称")]
        [Display(Name = "是否双职称")]
        public string UserIsDoubleTitle { get; set; }

        [Required(ErrorMessage = "请填写长号")]
        [Display(Name = "长号")]
        public string UserLongPhone { get; set; }

        [Display(Name = "短号")]
        public string UserShortPhone { get; set; }

        [Required(ErrorMessage = "请填写邮箱")]
        [Display(Name = "邮箱")]
        public string UserMail { get; set; }

        [Required(ErrorMessage = "请填写QQ号")]
        [Display(Name = "QQ号")]
        public string UserQQNum { get; set; }

        [Required]
        [Display(Name = "类型")]
        public string UserType { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

    }


}
