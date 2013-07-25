using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using TeacherTitle.Models;

namespace TeacherTitle.Infrastructure
{
    public partial class MailHelper
    {
        private struct MailHost
        {
            public String host { get; set; }
            public Int32 port { get; set; }
            public Boolean enableSsl { get; set; }
        }

        private static EmailProvider emaillProvider;

        private static void setMailProvider(String mailAddress)
        {
            mailAddress = mailAddress.ToLower();
            if (mailAddress.Contains("@163.com"))
            {
                emaillProvider = EmailProvider.网易163;
            }
            else if (mailAddress.Contains("@gmail.com"))
            {
                emaillProvider = EmailProvider.谷歌;
            }
            else if (mailAddress.Contains("@skywalk.cn"))
            {
                emaillProvider = EmailProvider.天行;
            }
            else if (mailAddress.Contains("@qq.com"))
            {
                emaillProvider = EmailProvider.腾讯;
            }
            else if (mailAddress.Contains("@126.com"))
            {
                emaillProvider = EmailProvider.网易126;
            }
            else if (mailAddress.Contains("@sina.cn"))
            {
                emaillProvider = EmailProvider.新浪;
            }
            else
            {
                emaillProvider = EmailProvider.未知;
            }
        }

        private static MailHost getSmtpMailHost()
        {
            MailHost mailHost = new MailHost();

            switch (emaillProvider)
            {
                case EmailProvider.网易163:
                    mailHost.host = "smtp.163.com";
                    mailHost.port = 25;
                    mailHost.enableSsl = false;
                    break;
                case EmailProvider.网易126:
                    mailHost.host = "smtp.126.com";
                    mailHost.port = 25;
                    mailHost.enableSsl = false;
                    break;
                case EmailProvider.谷歌:
                    mailHost.host = "smtp.gmail.com";
                    mailHost.port = 587;
                    mailHost.enableSsl = true;
                    break;
                case EmailProvider.新浪:
                    mailHost.host = "smtp.sina.cn";
                    mailHost.port = 25;
                    mailHost.enableSsl = false;
                    break;
                case EmailProvider.天行:
                    mailHost.host = "mail.skywalk.cn";
                    mailHost.port = 25;
                    mailHost.enableSsl = false;
                    break;
                case EmailProvider.腾讯:
                    mailHost.host = "smtp.qq.com";
                    mailHost.port = 25;
                    mailHost.enableSsl = false;
                    break;
                default:
                    break;
            }

            return mailHost;

        }

        private static MailHost getPopMailHost()
        {
            //var mail = Regex.Match(mailAddress, @"@\w+([-.]\w+)*\.").Value;

            MailHost mailHost = new MailHost();
            switch (emaillProvider)
            {
                case EmailProvider.网易163:
                    mailHost.host = "pop.163.com";
                    mailHost.port = 110;
                    break;
                case EmailProvider.网易126:
                    mailHost.host = "pop.126.com";
                    mailHost.port = 110;
                    break;
                case EmailProvider.谷歌:
                    mailHost.host = "pop.gmail.com";
                    mailHost.port = 993;
                    break;
                case EmailProvider.新浪:
                    mailHost.host = "pop.sina.cn";
                    mailHost.port = 110;
                    break;
                case EmailProvider.天行:
                    mailHost.host = "mail.skywalk.cn";
                    mailHost.port = 110;
                    break;
                case EmailProvider.腾讯:
                    mailHost.host = "pop.qq.com";
                    mailHost.port = 110;
                    break;
            }
            return mailHost;
        }

        private static MailHost getImapMailHost()
        {
            //var mail = Regex.Match(mailAddress, @"@\w+([-.]\w+)*\.").Value;

            MailHost mailHost = new MailHost();
            switch (emaillProvider)
            {
                case EmailProvider.网易163:
                    mailHost.host = "imap.163.com";
                    mailHost.port = 143;
                    mailHost.enableSsl = false;
                    break;
                case EmailProvider.网易126:
                    mailHost.host = "imap.126.com";
                    mailHost.port = 143;
                    mailHost.enableSsl = false;
                    break;
                case EmailProvider.谷歌:
                    mailHost.host = "imap.gmail.com";
                    mailHost.port = 993;
                    mailHost.enableSsl = true;
                    break;
                case EmailProvider.新浪:
                    mailHost.host = "imap.sina.cn";
                    mailHost.port = 143;
                    mailHost.enableSsl = false;
                    break;
                case EmailProvider.腾讯:
                    mailHost.host = "imap.qq.com";
                    mailHost.port = 143;
                    mailHost.enableSsl = false;
                    break;
            }
            return mailHost;
        }

        public static void SendMail(MailModel mailModel)
        {
            #region 验证邮箱
            setMailProvider(mailModel.MailFrom.Address);
            if (emaillProvider == EmailProvider.未知)
                throw new NotImplementedException("当前系统不支持您配置的邮箱,请到个人主页修改");
            #endregion

            #region 设置收件人信息
            MailMessage mail = new MailMessage();
            mail.From = mailModel.MailFrom;
            var mailNames = mailModel.MailTo.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var displayName = String.Empty;
            var address = String.Empty;
            foreach (var name in mailNames)
            {
                mail.To.Add(new MailAddress(name));
            }
            mail.Body = mailModel.Body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = mailModel.Prority;
            mail.Subject = mailModel.Subject;
            foreach (var item in mailModel.Attachments)
                mail.Attachments.Add(item);

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            #endregion

            #region 设置发件人信息
            SmtpClient client = new SmtpClient();
            MailHost mailHost = getSmtpMailHost();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(mailModel.MailFrom.Address, mailModel.Password);
            client.Port = mailHost.port;
            client.Host = mailHost.host;
            client.EnableSsl = mailHost.enableSsl;
            #endregion

            #region 发送邮件

            try
            {
                client.Send(mail);
            }
            catch (SmtpException sex)
            {
                throw new Exception(sex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion
        }

        //public static List<OAclient.Models.MailModel> ReceiveMail(OAclient.Models.MailModel mailModel)
        //{
        //    try
        //    {
        //        #region 验证邮箱
        //        setMailProvider(mailModel.MailFrom.Address);
        //        if (emaillProvider == EmailProvider.未知)
        //            throw new NotImplementedException("当前系统不支持您配置的邮箱,请到个人主页修改");
        //        #endregion

        //        var readed = Boolean.Parse(ComVariable.EmailReaded);
        //        var lastDay = Int32.Parse(ComVariable.EmailLastDay);

        //        var mails = GetMails(mailModel);

        //        mails = (from mail in mails
        //                 where mail.InternalDate.CompareTo(DateTime.Now.AddDays(lastDay)) <= 0
        //                 select mail).ToArray();

        //        List<OAclient.Models.MailModel> mailModes = new List<OAclient.Models.MailModel>();

        //        Int32 count = 1;


        //        foreach (IMAP_FetchItem fetchItem in mails)
        //        {
        //            if (!readed)
        //            {
        //                if (fetchItem.IsNewMessage)
        //                {
        //                    mailModes.Add(new OAclient.Models.MailModel
        //                    {
        //                        Subject = fetchItem.Envelope.Subject,
        //                        MailFrom = new System.Net.Mail.MailAddress(fetchItem.Envelope.From[0].EmailAddress, fetchItem.Envelope.From[0].DisplayName),
        //                        Date = fetchItem.Envelope.Date.ToString("yyyy-MM-dd HH:mm:ss"),
        //                        UID = fetchItem.UID,
        //                        MailIndex = count
        //                    });
        //                }
        //            }
        //            else
        //            {
        //                mailModes.Add(new OAclient.Models.MailModel
        //                {
        //                    Subject = fetchItem.Envelope.Subject,
        //                    MailFrom = new System.Net.Mail.MailAddress(fetchItem.Envelope.From[0].EmailAddress, fetchItem.Envelope.From[0].DisplayName),
        //                    Date = fetchItem.Envelope.Date.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    UID = fetchItem.UID,
        //                    MailIndex = count
        //                });
        //            }
        //            count++;
        //        }

        //        return mailModes;

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //public static OAclient.Models.MailModel ReceiveMail(OAclient.Models.MailModel mailModel, Int32 UID, String mailIndex)
        //{
        //    #region 验证邮箱
        //    setMailProvider(mailModel.MailFrom.Address);
        //    if (emaillProvider == EmailProvider.未知)
        //        throw new NotImplementedException("当前系统不支持您配置的邮箱,请到个人主页修改");
        //    #endregion

        //    OAclient.Models.MailModel model = new OAclient.Models.MailModel();
        //    var mails = GetMails(mailModel, mailIndex);

        //    foreach (IMAP_FetchItem item in mails)
        //    {
        //        if (item.UID == UID)
        //        {
        //            IMAP_FetchItem fetchItem = item;
        //            Mime mime = Mime.Parse(fetchItem.MessageData);

        //            List<String> attachName = new List<string>();
        //            List<String> attachIndex = new List<string>();

        //            for (int index = 0; index < mime.Attachments.Length; index++)
        //            {
        //                attachName.Add(mime.Attachments[index].ContentType_Name);
        //                attachIndex.Add(index.ToString());
        //            }

        //            model = new OAclient.Models.MailModel
        //            {
        //                Subject = fetchItem.Envelope.Subject,
        //                Body = mime.BodyHtml,
        //                MailFrom = new System.Net.Mail.MailAddress(fetchItem.Envelope.From[0].EmailAddress, fetchItem.Envelope.From[0].DisplayName),
        //                Date = mime.MainEntity.Date.ToString("yyyy-MM-dd HH:mm:ss"),
        //                AttachName = attachName.ToArray(),
        //                AttachIndex = attachIndex.ToArray(),
        //                UID = fetchItem.UID
        //            };
        //        }
        //        else
        //        {
        //            throw new Exception("发生未知错误,读取失败,请刷新邮件列表后再试");
        //        }
        //    }
        //    return model;
        //}

    }

    public enum EmailProvider
    {
        网易163 = 0,
        网易126,
        谷歌,
        新浪,
        天行,
        腾讯,
        未知
    }
}