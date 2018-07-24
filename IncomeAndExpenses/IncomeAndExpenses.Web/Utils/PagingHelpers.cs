using IncomeAndExpenses.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfoViewModel pageInfo, int maxPagesAround, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder prev = new TagBuilder("a");
            if (pageInfo.PageNumber - 1 == 0)
            {
                prev.AddCssClass("disabled");
            }
            prev.MergeAttribute("href", pageUrl(pageInfo.PageNumber - 1));
            prev.InnerHtml = "&laquo;";
            prev.AddCssClass("btn btn-light");
            result.Append(prev.ToString());
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-light");
                if (i < pageInfo.PageNumber - maxPagesAround || i > pageInfo.PageNumber + maxPagesAround)
                {
                    tag.AddCssClass("display-none");
                }
                result.Append(tag.ToString());
            }
            TagBuilder next = new TagBuilder("a");
            if (pageInfo.PageNumber + 1 > pageInfo.TotalPages)
            {
                next.AddCssClass("disabled");
            }
            next.MergeAttribute("href", pageUrl(pageInfo.PageNumber + 1));
            next.InnerHtml = "&raquo;";
            next.AddCssClass("btn btn-light");
            result.Append(next.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}