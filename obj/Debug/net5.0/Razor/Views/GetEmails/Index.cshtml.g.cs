#pragma checksum "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\GetEmails\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04bef0b3a819e167d0a5d0a7ba6c1464b31e4f7b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_GetEmails_Index), @"mvc.1.0.view", @"/Views/GetEmails/Index.cshtml")]
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
#line 1 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\_ViewImports.cshtml"
using NottiesRebuiltV3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\_ViewImports.cshtml"
using NottiesRebuiltV3.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\GetEmails\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04bef0b3a819e167d0a5d0a7ba6c1464b31e4f7b", @"/Views/GetEmails/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"836b121fb63c924efe8e50c655f842c8c0605c7e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_GetEmails_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\GetEmails\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04bef0b3a819e167d0a5d0a7ba6c1464b31e4f7b3509", async() => {
                WriteLiteral("\r\n    <section class=\"signup-section bg-light\" id=\"projects\">\r\n\r\n");
#nullable restore
#line 11 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\GetEmails\Index.cshtml"
 if (SignInManager.IsSignedIn(User) & User.IsInRole("Manager"))
                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"<div class=""d-flex container px-4 px-lg-5"">
<div class=""bg_glass row gx-0 mt-5 mb-lg-0 justify-content-center"" style=""padding:2rem"">

    <h1 class=""m-2 text-white"">Newsletter Email Addresses (Paste in gmail)</h1>
    <p class=""text-white m-2"">
        ");
#nullable restore
#line 18 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\GetEmails\Index.cshtml"
   Write(ViewBag.Emails);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n    </p>\r\n\r\n    </div>\r\n    </div>\r\n");
#nullable restore
#line 23 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\GetEmails\Index.cshtml"
}
else if (!SignInManager.IsSignedIn(User))
{

#line default
#line hidden
#nullable disable
                WriteLiteral("    <p>\r\n        You Shouldn\'t be here, your IP has being recorded, further security breaches from you will be tracked.\r\n    </p>\r\n");
#nullable restore
#line 29 "C:\Users\sivan\source\repos\NottiesRebuiltV3\Views\GetEmails\Index.cshtml"
}

#line default
#line hidden
#nullable disable
                WriteLiteral("    \r\n    </section>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<IdentityUser> SignInManager { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
