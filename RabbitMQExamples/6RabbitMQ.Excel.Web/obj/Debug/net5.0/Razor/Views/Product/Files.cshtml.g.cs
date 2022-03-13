#pragma checksum "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9013fe0dc5f780dc255c5d1298c5c2cdb7e99978"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Files), @"mvc.1.0.view", @"/Views/Product/Files.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\_ViewImports.cshtml"
using _6RabbitMQ.Excel.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\_ViewImports.cshtml"
using _6RabbitMQ.Excel.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9013fe0dc5f780dc255c5d1298c5c2cdb7e99978", @"/Views/Product/Files.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae27bb30ad4835488b3c697a8f058db65028a122", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Files : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<UserFile>>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
  
    ViewData["Title"] = "Files";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n       $(document).ready(function () {\r\n           var hasStartCreatingExcel = \'");
#nullable restore
#line 10 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
                                   Write(TempData["StartCreatingExcel"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
           if (hasStartCreatingExcel) {
               Swal.fire({
                   position: 'top-end',
                   icon: 'success',
                   title: 'Excel oluşturma işlemi başlamıştır. Bittiğinde bildiri alacaksınız.',
                   showConfirmButton: false,
                  timer:2500
               })
           }
       })
    </script>
");
            }
            );
            WriteLiteral("\r\n<h1>Files</h1>\r\n<hr />\r\n<table class=\"table table-striped\">\r\n    <thead>\r\n        <tr>\r\n            <th>File Name</th>\r\n            <th>Created Date</th>\r\n            <th>File Status</th>\r\n            <th>Download</th>\r\n        </tr>\r\n    </thead>\r\n");
#nullable restore
#line 35 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 38 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
           Write(item.FileName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 39 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
           Write(item.GetCreatedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 40 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
           Write(item.FileStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9013fe0dc5f780dc255c5d1298c5c2cdb7e999785716", async() => {
                WriteLiteral("DownLoad File");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1067, "~/Files/", 1067, 8, true);
#nullable restore
#line 42 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
AddHtmlAttributeValue("", 1075, item.FilePath, 1075, 14, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1098, "btn", 1098, 3, true);
            AddHtmlAttributeValue(" ", 1101, "btn-primary", 1102, 12, true);
#nullable restore
#line 42 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
AddHtmlAttributeValue(" ", 1113, item.FileStatus == FileStatus.Creating ? "disabled" : "" , 1114, 60, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 45 "C:\Users\emre.akturk\source\repos\RabbitMQExamples\6RabbitMQ.Excel.Web\Views\Product\Files.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<UserFile>> Html { get; private set; }
    }
}
#pragma warning restore 1591