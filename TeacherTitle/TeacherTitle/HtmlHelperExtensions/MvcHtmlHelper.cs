using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;
using TeacherTitle.Infrastructure;

namespace TeacherTitle.HtmlHelperExtensions
{
    /// <summary>
    /// HtmlHelper扩展
    /// </summary>
    public static class MvcHtmlHelper
    {
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="helper">表示支持在视图中呈现AJAx方案中的HTMl</param>
        /// <param name="pageInfo">要显示的信息</param>
        /// <param name="pageUrl"></param>
        /// <param name="ajaxOptions">表示用于在MVC应用程序中运行AJAx脚本的选项设置</param>
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this AjaxHelper helper,
            PageInfo pageInfo, Func<Int32, String> pageUrl, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return PageLinksHelper(helper, pageInfo, pageUrl, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="helper">表示支持在视图中呈现AJAx方案中的HTMl</param>
        /// <param name="pageInfo">要显示的信息</param>
        /// <param name="pageUrl"></param>
        /// <param name="ajaxOptions">表示用于在MVC应用程序中运行AJAx脚本的选项设置</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this AjaxHelper helper,
            PageInfo pageInfo, Func<Int32, String> pageUrl, AjaxOptions ajaxOptions)
        {
            return PageLinksHelper(helper, pageInfo, pageUrl, ajaxOptions, null);
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="helper">表示支持在视图中呈现AJAx方案中的HTMl</param>
        /// <param name="pageInfo">要显示的信息</param>
        /// <param name="pageUrl"></param>
        /// <param name="ajaxOptions">表示用于在MVC应用程序中运行AJAx脚本的选项设置</param>
        /// <param name="htmlAttributes">html属性</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this AjaxHelper helper,
            PageInfo pageInfo, string pageUrl, object data, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return PageLinksHelper(helper, pageInfo, pageUrl, HtmlHelper.AnonymousObjectToHtmlAttributes(data), ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="helper">表示支持在视图中呈现AJAx方案中的HTMl</param>
        /// <param name="pageInfo">要显示的信息</param>
        /// <param name="pageUrl"></param>
        /// <param name="ajaxOptions">表示用于在MVC应用程序中运行AJAx脚本的选项设置</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this AjaxHelper helper,
            PageInfo pageInfo, string pageUrl, object data, AjaxOptions ajaxOptions)
        {
            return PageLinksHelper(helper, pageInfo, pageUrl, HtmlHelper.AnonymousObjectToHtmlAttributes(data), ajaxOptions, null);
        }


        private static string produceAjax(string pageUrl, string pageNum, string data, AjaxOptions ajaxOptions)
        {
            return "$.ajax({type: '" + (ajaxOptions.HttpMethod == "" ? "GET" : ajaxOptions.HttpMethod) + "'" +
                ",url: '" + pageUrl + "',data: {'page':" + pageNum + "," + data + "},success: function (data) {$('#" + ajaxOptions.UpdateTargetId + "').html(data)}});";
        }

        private static MvcHtmlString PageLinksHelper(this AjaxHelper helper,
            PageInfo pageInfo, string pageUrl, IDictionary<string, object> data, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            pageInfo.TotalItems = pageInfo.TotalItems == 0 ? 1 : pageInfo.TotalItems;

            string Data = string.Empty;

            foreach (string key in data.Keys)
            {
                Data = Data + "'" + key + "'" + ":" + data[key] + ",";
            }
            Data = Data.TrimEnd(',');

            string ajaxKeyPress = produceAjax(pageUrl, "$('#pageSkip').val()", Data, ajaxOptions);

            StringBuilder result = new StringBuilder();

            result.AppendLine("共" + pageInfo.TotalPages + "页，当前第" + pageInfo.CurrentPage + "页");

            var firstPage = new TagBuilder("a");
            firstPage.MergeAttribute("style", "cursor:pointer;");
            firstPage.InnerHtml = "首页";
            if (pageInfo.CurrentPage != 1)
            {
                firstPage.MergeAttribute("onclick", produceAjax(pageUrl, "1", Data, ajaxOptions));
            }
            else
            {
                firstPage.MergeAttribute("style", "color:Gray");
            }
            result.AppendLine(firstPage.ToString());

            var prePage = new TagBuilder("a");
            prePage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.CurrentPage > 1)
            {
                prePage.MergeAttribute("onclick", produceAjax(pageUrl, (pageInfo.CurrentPage - 1).ToString(), Data, ajaxOptions));
            }
            else
            {
                prePage.MergeAttribute("style", "color:Gray");
            }
            prePage.InnerHtml = "上一页";
            result.AppendLine(prePage.ToString());

            result.AppendLine("&nbsp;第");
            var inputPage = new TagBuilder("input");
            inputPage.MergeAttribute("id", "pageSkip");
            inputPage.MergeAttribute("type", "text");
            inputPage.MergeAttribute("value", pageInfo.CurrentPage.ToString());
            inputPage.MergeAttribute("style", "width:30px");
            inputPage.MergeAttribute("onkeyup", @"if($('#pageSkip').val().match(/^\d+$/)==null){this.value=this.value.replace(/\D/g,'')}" +
                "else{if(parseInt(this.value)>parseInt($('#hiddenTotalPages').val())){$('#pageSkip').css({ color: '#ff0011'}); }else{$('#pageSkip').css({ color: '#000000'});if($('#oldPageVal').attr('value')!=this.value){" + ajaxKeyPress + "}}}");
            result.AppendLine(inputPage.ToString());
            result.AppendLine("页&nbsp;");

            //此输入框用来存储当前页码,为了避免在输入同一页码的时候也进行查询
            var odlInput = new TagBuilder("input");
            odlInput.MergeAttribute("id", "oldPageVal");
            odlInput.MergeAttribute("type", "hidden");
            odlInput.MergeAttribute("value", pageInfo.CurrentPage.ToString());
            result.AppendLine(odlInput.ToString());


            var hideInput = new TagBuilder("input");
            hideInput.MergeAttribute("id", "hiddenTotalPages");
            hideInput.MergeAttribute("type", "hidden");
            hideInput.MergeAttribute("value", pageInfo.TotalPages.ToString());
            result.AppendLine(hideInput.ToString());

            var nextPage = new TagBuilder("a");
            nextPage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.CurrentPage < pageInfo.TotalPages)
            {
                nextPage.MergeAttribute("onclick", produceAjax(pageUrl, (pageInfo.CurrentPage + 1).ToString(), Data, ajaxOptions));
            }
            else
            {
                nextPage.MergeAttribute("style", "color:Gray");
            }
            nextPage.InnerHtml = "下一页";
            result.AppendLine(nextPage.ToString());

            var lastPage = new TagBuilder("a");
            lastPage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.TotalPages != 1)
            {
                lastPage.MergeAttribute("onclick", produceAjax(pageUrl, pageInfo.TotalPages.ToString(), Data, ajaxOptions));
            }
            else
            {
                lastPage.MergeAttribute("style", "color:Gray");
            }
            lastPage.InnerHtml = "末页";
            result.AppendLine(lastPage.ToString());

            TagBuilder div = new TagBuilder("div");
            div.MergeAttributes(htmlAttributes);
            div.InnerHtml = result.ToString();

            return MvcHtmlString.Create(div.ToString());
        }

        private static MvcHtmlString PageLinksHelper(this AjaxHelper helper,
            PageInfo pageInfo, Func<Int32, String> pageUrl, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            pageInfo.TotalItems = pageInfo.TotalItems == 0 ? 1 : pageInfo.TotalItems;
            //String ajaxClick = "Sys.Mvc.AsyncHyperlink.handleClick(this, new Sys.UI.DomEvent(event), " +
            //    "{ insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId: '" + ajaxOptions.UpdateTargetId + "', onComplete: Function.createDelegate(this, " + ajaxOptions.OnComplete + ") });";
            //var asd = pageUrl(pageInfo.CurrentPage - 1);

            String ajaxKeyPress = "$.ajax({type: '" + (ajaxOptions.HttpMethod == "" ? "GET" : ajaxOptions.HttpMethod) + "'" +
                ",url: '" + pageUrl(-1).Replace("-1", "") + "',data: {'page':$('#pageSkip').val()},success: function (data) {$('#" + ajaxOptions.UpdateTargetId + "').html(data)}});";

            //string ajaxSkip = @"if($('#pageSkip').val().match(/^\d+$/)==null){$('#pageSkip').val($('#pageSkip').val().replace(/\D/g,''))}" +
            //    "else{if(parseInt($('#pageSkip').val())>parseInt($('#hiddenTotalPages').val())){$('#pageSkip').css({ color: '#ff0011'}); }else{$('#pageSkip').css({ color: '#000000'});" + ajaxKeyPress + "}}";

            ////文本框值变化的监听
            //String easychange = "<script type='text/javascript'>$(function(){easychange.call(document.getElementById('pageSkip'), function (value) {" + ajaxSkip + "});});</script>";

            StringBuilder result = new StringBuilder();

            //result.AppendLine(easychange);

            result.AppendLine("共" + pageInfo.TotalPages + "页，当前第" + pageInfo.CurrentPage + "页");

            var firstPage = new TagBuilder("a");
            firstPage.MergeAttribute("style", "cursor:pointer;");
            firstPage.InnerHtml = "首页";
            string a = pageUrl(0);
            if (pageInfo.CurrentPage != 1)
            {
                firstPage.MergeAttribute("href", pageUrl(1));
                firstPage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                firstPage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                firstPage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                firstPage.MergeAttribute("data-ajax", "true");
            }
            else
            {
                firstPage.MergeAttribute("style", "color:Gray");
            }
            result.AppendLine(firstPage.ToString());

            var prePage = new TagBuilder("a");
            prePage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.CurrentPage > 1)
            {
                prePage.MergeAttribute("href", pageUrl((pageInfo.CurrentPage - 1)));
                prePage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                prePage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                prePage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                prePage.MergeAttribute("data-ajax", "true");
            }
            else
            {
                prePage.MergeAttribute("style", "color:Gray");
            }
            prePage.InnerHtml = "上一页";
            result.AppendLine(prePage.ToString());

            result.AppendLine("&nbsp;第");
            var inputPage = new TagBuilder("input");
            inputPage.MergeAttribute("id", "pageSkip");
            inputPage.MergeAttribute("type", "text");
            inputPage.MergeAttribute("value", pageInfo.CurrentPage.ToString());
            inputPage.MergeAttribute("style", "width:30px");
            inputPage.MergeAttribute("onkeyup", @"if($('#pageSkip').val().match(/^\d+$/)==null){this.value=this.value.replace(/\D/g,'')}" +
                "else{if(parseInt(this.value)>parseInt($('#hiddenTotalPages').val())){$('#pageSkip').css({ color: '#ff0011'}); }else{$('#pageSkip').css({ color: '#000000'});if($('#oldPageVal').attr('value')!=this.value){" + ajaxKeyPress + "}}}");
            result.AppendLine(inputPage.ToString());
            result.AppendLine("页&nbsp;");

            //此输入框用来存储当前页码,为了避免在输入同一页码的时候也进行查询
            var odlInput = new TagBuilder("input");
            odlInput.MergeAttribute("id", "oldPageVal");
            odlInput.MergeAttribute("type", "hidden");
            odlInput.MergeAttribute("value", pageInfo.CurrentPage.ToString());
            result.AppendLine(odlInput.ToString());


            var hideInput = new TagBuilder("input");
            hideInput.MergeAttribute("id", "hiddenTotalPages");
            hideInput.MergeAttribute("type", "hidden");
            hideInput.MergeAttribute("value", pageInfo.TotalPages.ToString());
            result.AppendLine(hideInput.ToString());

            var nextPage = new TagBuilder("a");
            nextPage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.CurrentPage < pageInfo.TotalPages)
            {
                nextPage.MergeAttribute("href", pageUrl((pageInfo.CurrentPage + 1)));
                nextPage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                nextPage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                nextPage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                nextPage.MergeAttribute("data-ajax", "true");
            }
            else
            {
                nextPage.MergeAttribute("style", "color:Gray");
            }
            nextPage.InnerHtml = "下一页";
            result.AppendLine(nextPage.ToString());

            var lastPage = new TagBuilder("a");
            lastPage.MergeAttribute("style", "cursor:pointer;");
            if (pageInfo.TotalPages != 1)
            {
                lastPage.MergeAttribute("href", pageUrl(pageInfo.TotalPages));
                lastPage.MergeAttribute("data-ajax-update", "#" + ajaxOptions.UpdateTargetId);
                lastPage.MergeAttribute("data-ajax-mode", ajaxOptions.InsertionMode.ToString());
                lastPage.MergeAttribute("data-ajax-method", ajaxOptions.HttpMethod);
                lastPage.MergeAttribute("data-ajax", "true");
            }
            else
            {
                lastPage.MergeAttribute("style", "color:Gray");
            }
            lastPage.InnerHtml = "末页";
            result.AppendLine(lastPage.ToString());

            TagBuilder div = new TagBuilder("div");
            div.MergeAttributes(htmlAttributes);
            div.InnerHtml = result.ToString();

            return MvcHtmlString.Create(div.ToString());
        }
    }


}