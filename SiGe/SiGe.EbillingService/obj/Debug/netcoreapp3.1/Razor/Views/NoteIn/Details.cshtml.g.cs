#pragma checksum "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3ac7450562aaaf73cd205031cb1fcc70fac9d18b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(SiGe.EbillingService.Pages.NoteIn.Views_NoteIn_Details), @"mvc.1.0.view", @"/Views/NoteIn/Details.cshtml")]
namespace SiGe.EbillingService.Pages.NoteIn
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
#line 1 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\_ViewImports.cshtml"
using SiGe.EbillingService;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ac7450562aaaf73cd205031cb1fcc70fac9d18b", @"/Views/NoteIn/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d73129199c82d3465c108f8a85c0f322abc554ff", @"/Views/_ViewImports.cshtml")]
    public class Views_NoteIn_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SiGe.NoteDetailViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", new global::Microsoft.AspNetCore.Html.HtmlString("Volver"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "NoteIn", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
   ViewData["Title"] = "Detalles"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"page-header\">\n    <div class=\"row\">\n        <div class=\"col-md-12 col-sm-12\">\n            <hr />\n            <div class=\"form-group row\">\n                <label class=\"col-sm-12 col-md-12 col-form-label\">");
#nullable restore
#line 10 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
                                                             Write(Html.DisplayNameFor(model => model.Note));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                <div class=\"col-sm-12 col-md-12\">\n                    <label class=\"col-sm-12 col-md-12 col-form-label\">");
#nullable restore
#line 12 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
                                                                 Write(Html.DisplayFor(model => model.Note));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n            </div>\n\n            <div class=\"form-group row\">\n                <label class=\"col-sm-12 col-md-12 col-form-label\">");
#nullable restore
#line 17 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
                                                             Write(Html.DisplayNameFor(model => model.NoteType));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                <div class=\"col-sm-12 col-md-12\">\n                    <label class=\"col-sm-12 col-md-12 col-form-label\">");
#nullable restore
#line 19 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
                                                                 Write(Html.DisplayFor(model => model.NoteType));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n            </div>\n\n            <div class=\"form-group row\">\n                <label class=\"col-sm-12 col-md-12 col-form-label\">");
#nullable restore
#line 24 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
                                                             Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                <div class=\"col-sm-12 col-md-12\">\n                    <label class=\"col-sm-12 col-md-12 col-form-label\">");
#nullable restore
#line 26 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
                                                                 Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
                </div>
            </div>

            <table class=""table"">
                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Descripcion</th>
                        <th>Cantidad</th>
                        <th>Precio Unitario</th>
                        <th>Precio</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 41 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
                     foreach (var item in Model.Details)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>");
#nullable restore
#line 44 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 45 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 46 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 47 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.UnitPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 48 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
           Write(Html.DisplayFor(modelItem => item.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n        </tr>\n");
#nullable restore
#line 50 "C:\Users\luis_\Documents\Visual Studio 2019\Projects\SiGe\SiGe.EbillingService\Views\NoteIn\Details.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n\n            </table>\n\n\n        </div>\n    </div>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3ac7450562aaaf73cd205031cb1fcc70fac9d18b10778", async() => {
                WriteLiteral("\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "3ac7450562aaaf73cd205031cb1fcc70fac9d18b11043", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SiGe.NoteDetailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
