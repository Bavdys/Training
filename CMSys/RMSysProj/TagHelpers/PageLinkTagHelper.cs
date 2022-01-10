using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RMSysProj.ViewModels;

namespace RMSysProj.TagHelpers
{
    public class PageLinkTagHelper:TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageListViewModel PageListViewModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "nav";

            TagBuilder tag = new("ul");
            tag.AddCssClass("pagination m-0");

            TagBuilder startItem = CreateTag(PageListViewModel.Page - 1,"&laquo;", urlHelper, PageListViewModel.CanPrevious);
            if (!PageListViewModel.CanPrevious)
            {
                startItem.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(startItem);

            int startPosition = !PageListViewModel.CanPrevious || PageListViewModel.TotalPages - 1 == 1 ? 1 : !PageListViewModel.CanNext ? PageListViewModel.Page - 2 : PageListViewModel.Page - 1; 
            int endPosition = !PageListViewModel.CanNext || PageListViewModel.TotalPages - 1 == 1 ? PageListViewModel.TotalPages : !PageListViewModel.CanPrevious ? PageListViewModel.Page + 2 : PageListViewModel.Page + 1;
            
            for (int i = startPosition; i <= endPosition; i++)
            {
                TagBuilder item = CreateTag(i, i.ToString(), urlHelper, i != PageListViewModel.Page);
                if (i == PageListViewModel.Page) item.AddCssClass("active");
                tag.InnerHtml.AppendHtml(item);
            }

            TagBuilder endItem = CreateTag(PageListViewModel.Page + 1, "&raquo;", urlHelper, PageListViewModel.CanNext);
            if (!PageListViewModel.CanNext)
            {
                endItem.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(endItem);

            output.Content.AppendHtml(tag);

        }

        TagBuilder CreateTag(int pageNumber, string content, IUrlHelper urlHelper , bool isLink)
        {
            TagBuilder item = new ("li");
            TagBuilder link = isLink ? new("a") : new("span");

            if (isLink)
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber});
                link.AddCssClass("link");
            }
            
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.AppendHtml(content);
            item.InnerHtml.AppendHtml(link);

            return item;
        }
    }
}
