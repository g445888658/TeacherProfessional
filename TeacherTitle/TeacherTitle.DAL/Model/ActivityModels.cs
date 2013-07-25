using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherTitle.DAL.Infrastructure;

namespace TeacherTitle.DAL.Model
{
    /// <summary>
    /// 用于查看教师活动的报名情况和学时统计
    /// </summary>
    public class ActAndClaHourModel
    {
        /// <summary>
        /// 报名编号
        /// </summary>
        public string SignUpCode { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public KeyValueModel UserCode { get; set; }


        /// <summary>
        /// 活动形式
        /// </summary>
        public KeyValueModel ActForm { get; set; }

        /// <summary>
        /// 活动详情编号
        /// </summary>
        public string APCode { get; set; }

        /// <summary>
        /// 主讲人
        /// </summary>
        public string ActSpeaker { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string ActTheme { get; set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        public string ActPlace { get; set; }

        /// <summary>
        /// 开始
        /// </summary>
        public string ActStart { get; set; }

        /// <summary>
        /// 结束
        /// </summary>
        public string ActEnd { get; set; }

        /// <summary>
        /// 该活动发布时间
        /// </summary>
        public string ActReleaseTime { get; set; }

        /// <summary>
        /// 活动状态(0结束,1报名,2教师申请)
        /// </summary>
        public KeyValueModel ActStatus { get; set; }

        /// <summary>
        /// 是否为候补
        /// </summary>
        public KeyValueModel IsCandidate { get; set; }

        /// <summary>
        /// 活动时间
        /// </summary>
        public string SUTime { get; set; }

        /// <summary>
        /// 取得时间
        /// </summary>
        public string GetTime { get; set; }

        /// <summary>
        /// 授予学时
        /// </summary>
        public string ClassHour { get; set; }
    }
}
