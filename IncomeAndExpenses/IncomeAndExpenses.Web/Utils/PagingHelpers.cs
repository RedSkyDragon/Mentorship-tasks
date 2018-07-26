using IncomeAndExpenses.Web.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace IncomeAndExpenses.Web.Utils
{
    /// <summary>
    /// Helpers for creating pagination
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        /// Create html element for pagination
        /// </summary>
        /// <param name="html">The html</param>
        /// <param name="pageInfo">Information abount requested pagination</param>
        /// <param name="maxPagesAround">Number of visible pages around current</param>
        /// <param name="pageUrl">Url of Action for page</param>
        /// <returns>html of the pagination element</returns>
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