using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace TeacherTitle.Models
{
    public class MailModel
    {
        public MailModel()
        {
            attachments = new List<Attachment>();
        }

        /// <summary>
        ///  发件人
        /// </summary>
        public MailAddress MailFrom { get; set; }

        /// <summary>
        ///  对象
        /// </summary>
        public String MailTo { get; set; }

        /// <summary>
        ///  主题
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        ///  主体
        /// </summary>
        public String Body { get; set; }

        /// <summary>
        ///  优先权
        /// </summary>
        public MailPriority Prority { get; set; }

        /// <summary>
        ///  密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        ///  发送时间
        /// </summary>
        public String Date { get; set; }

        /// <summary>
        ///  邮件编号
        /// </summary>
        public String UID { get; set; }

        /// <summary>
        ///  附件
        /// </summary>
        public List<Attachment> Attachments
        {
            get
            {
                return attachments;
            }
            set
            {
                attachments = value;
            }
        }

        private List<Attachment> attachments;
    }
}