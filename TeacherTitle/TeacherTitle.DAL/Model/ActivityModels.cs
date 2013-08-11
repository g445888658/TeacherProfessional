using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;
using TeacherTitle.DAL.DB;

namespace TeacherTitle.DAL.Model
{
    /// <summary>
    /// 用于已经结束的活动
    /// </summary>
    public class AlreadyEndOfActModels
    {
        /// <summary>
        /// 活动信息
        /// </summary>
        public ActivityPlan Plan { get; set; }

        /// <summary>
        /// 报名信息(包含学时信息)
        /// </summary>
        public SignUpModels[] SignUpModels { get; set; }
    }


    public class ActivityPlanModels
    {
        /// <summary>
        /// 活动内容
        /// </summary>
        public ActivityPlan Plan { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public ActivityAttachment[] Attachment { get; set; }
    }
}
