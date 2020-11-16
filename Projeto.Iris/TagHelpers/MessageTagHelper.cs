using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Iris.TagHelpers
{
    public class MessageTagHelper : TagHelper
    {

        public string Text { get; set; }
        public string AlertType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(Text)) 
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("class", "alert alert-" + AlertType);
                output.Content.SetContent(Text);
            }
        }
    }
}
