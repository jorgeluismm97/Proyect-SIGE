#pragma checksum "C:\Users\PC\Desktop\Proyecto\Proyecto SIGE - Codigo\SiGe\SiGe.EbillingService\Views\_ViewStart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9b1c1311c7ebf801aaf737c8d59538472054dbe1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(SiGe.EbillingService.Pages.Views__ViewStart), @"mvc.1.0.view", @"/Views/_ViewStart.cshtml")]
namespace SiGe.EbillingService.Pages
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
#line 1 "C:\Users\PC\Desktop\Proyecto\Proyecto SIGE - Codigo\SiGe\SiGe.EbillingService\Views\_ViewImports.cshtml"
using SiGe.EbillingService;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9b1c1311c7ebf801aaf737c8d59538472054dbe1", @"/Views/_ViewStart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d73129199c82d3465c108f8a85c0f322abc554ff", @"/Views/_ViewImports.cshtml")]
    public class Views__ViewStart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\PC\Desktop\Proyecto\Proyecto SIGE - Codigo\SiGe\SiGe.EbillingService\Views\_ViewStart.cshtml"
 if (User.Identity.IsAuthenticated)
{
    Layout = "_DashboardLayout";
}
else
{
    Layout = "_Layout";
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div id=""MyPopup"" class=""modal fade"" role=""dialog"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <button type=""button"" class=""close"" data-dismiss=""modal"">
                    &times;
                </button>
                <h4 class=""modal-title""></h4>
            </div>
            <div class=""modal-body"">
                <span id=""lblError""></span>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
            </div>
        </div>
    </div>
</div>

");
#nullable restore
#line 29 "C:\Users\PC\Desktop\Proyecto\Proyecto SIGE - Codigo\SiGe\SiGe.EbillingService\Views\_ViewStart.cshtml"
 if (ViewBag.Message != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<script type=\"text/javascript\">\n            window.onload = function () {\n                $(\"#lblError\").html(\'");
#nullable restore
#line 33 "C:\Users\PC\Desktop\Proyecto\Proyecto SIGE - Codigo\SiGe\SiGe.EbillingService\Views\_ViewStart.cshtml"
                                Write(Html.Raw(ViewBag.Message));

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\n                $(\"#MyPopup\").modal(\"show\");\n            };\n</script>\n");
#nullable restore
#line 37 "C:\Users\PC\Desktop\Proyecto\Proyecto SIGE - Codigo\SiGe\SiGe.EbillingService\Views\_ViewStart.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
