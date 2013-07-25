using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;

namespace TeacherTitle.AjaxHelperExtensions
{
    public static class PagingHelper
    {
        //public static MvcHtmlString Paging(this AjaxHelper helper, IDictionary<string, object> pagingAttributes, IDictionary<string, object> htmlAttributes)
        //{
        //    StringBuilder str = new StringBuilder();
        //    TagBuilder div = new TagBuilder("div");
        //    IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
        //    div.MergeAttributes(htmlAttributes);
        //    str.Append(div.ToString());

        //    string js = "$(function () {" +
        //    "$('#pagingDiv').paginate({" +
        //        "count: 10," +
        //        "start: 1," +
        //        "display: 5," +
        //        "border: true," +
        //        "border_color: '#fff'," +
        //        "text_color: '#888'," +
        //        "background_color: 'white'," +
        //        "border_hover_color: '#ccc'," +
        //        "text_hover_color: 'black'," +
        //        "background_hover_color: 'white'," +
        //        "images: false," +
        //        "mouse: 'press'," +
        //        "onChange: function (page) {" +
        //        //debugger;
        //        "}" +
        //    "});" +
        //"})";
        //    str.Append(js);
        //    //div.Append("<div></div>");
        //}

    }
}