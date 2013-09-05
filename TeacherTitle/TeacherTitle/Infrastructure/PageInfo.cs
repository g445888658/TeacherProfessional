using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherTitle.Infrastructure
{

    public class NewPageInfo
    {
        /// <summary>
        /// 总共行数
        /// </summary>
        public Int32 TotalItems { get; set; }

        /// <summary>
        /// 每页呈现的行数
        /// </summary>
        public Int32 ItemsPerPage { get; set; }

        /// <summary>
        /// 每次呈现的页数
        /// </summary>
        public Int32 PagesEveryTurn { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public Int32 CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public Int32 TotalPages
        {
            get { return (Int32)Math.Ceiling((Decimal)TotalItems / ItemsPerPage); }
        }

        /// <summary>
        /// 当前页的位置
        /// </summary>
        public Int32 Position
        {
            get { return CurrentPage % PagesEveryTurn; }
        }

        /// <summary>
        /// 当前页为每次呈现的页数的倍数
        /// </summary>
        public Int32 pageMultiple
        {
            get { return (Int32)Math.Ceiling((Decimal)CurrentPage / PagesEveryTurn); }
        }


    }

    /// <summary>
    /// 表格要显示的信息
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 总共行数
        /// </summary>
        public Int32 TotalItems { get; set; }

        /// <summary>
        /// 每页呈现的行数
        /// </summary>
        public Int32 ItemsPerPage { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public Int32 CurrentPage { get; set; }



        public Int32 TotalPages
        {
            get { return (Int32)Math.Ceiling((Decimal)TotalItems / ItemsPerPage); }
        }


    }
}