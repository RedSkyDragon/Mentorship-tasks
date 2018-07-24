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
            int first = pageInfo.PageNumber - maxPagesAround < 1 ? 1 : pageInfo.PageNumber - maxPagesAround;
            int last = pageInfo.PageNumber + maxPagesAround > pageInfo.TotalPages ? pageInfo.TotalPages : pageInfo.PageNumber + maxPagesAround;
            for (int i = first; i <= last; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn btn-primary");
                }
                else
                {
                    tag.AddCssClass("btn btn-light");
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