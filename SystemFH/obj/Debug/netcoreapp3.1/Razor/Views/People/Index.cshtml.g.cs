#pragma checksum "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\People\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "488a61a67c5a61d0debe755e107ce367a5d3a511"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_People_Index), @"mvc.1.0.view", @"/Views/People/Index.cshtml")]
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
#line 1 "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\_ViewImports.cshtml"
using SystemFH;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\_ViewImports.cshtml"
using SystemFH.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"488a61a67c5a61d0debe755e107ce367a5d3a511", @"/Views/People/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26f4e4bf7878e2a711b2142f1e203edff8aed020", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_People_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PaginationViewModel<Person>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("font-size: 25px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PartialIndexPeople", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\People\Index.cshtml"
  
    ViewData["Title"] = "Pessoas";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"col-md card border border-primary\">\r\n    <div class=\"card-header\">\r\n        <h1>Pessoas / ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "488a61a67c5a61d0debe755e107ce367a5d3a5114540", async() => {
                WriteLiteral("Nova");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h1>    \r\n    </div>\r\n    <div id=\"container\" style=\"margin:8px\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "488a61a67c5a61d0debe755e107ce367a5d3a5115858", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 17 "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\People\Index.cshtml"
      
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
    <script>
        function initialFunction() {
            $(""#checkCentral"").click(function () {
                //Verifica se o botão que possui o id checkCentral foi pressionado
                //Encontra todas as tags input que possuem a propriedade name = checkSelect
                //Troca a propriedade checked pelo valor que o botão id checkCentral possui

                $(""input[name='checkSelect']"").prop('checked', $(this).prop('checked'));

                if ($(this).prop('checked')) {
                    $(""#botaoDeletar"").show();
                }
                else {
                    $(""#botaoDeletar"").hide();
                }
            });

            $(""input[name='checkSelect']"").click(function () {
                var idsSelecionados = $(""input[name='checkSelect']:checked"").length;

                if ($(this).is(':checked') || idsSelecionados > 0) {
                    $(""#botaoDeletar"").show(); //style=""display:initial""
                }
              ");
                WriteLiteral(@"  else {
                    $(""#botaoDeletar"").hide(); //style=""display:none""
                    $(""checkCentral"").prop('checked', false);
                }

                if (idsSelecionados < $(""input[name='checkSelect']"").length) {
                    $(""#checkCentral"").prop('checked', false);
                }
                else {
                    $(""#checkCentral"").prop('checked', true);
                }
            });

            $(""#botaoDeletar"").click(function () {
                var idsSelecionados = $(""input[name='checkSelect']:checked"").map(function () {
                    return $(this).val();
                }).get();
                //Map: é como um foreach para arrays
                //Gera uma informação nova

                //Via HTML
                if (idsSelecionados.length > 0) {
                    if (confirm(""Você tem certeza de que deseja deletar este(s) registro(s)?"")) {
                        $.ajax({
                            url: '");
#nullable restore
#line 68 "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\People\Index.cshtml"
                             Write(Url.Action("MultipleDelete", "People"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                            type: 'POST',
                            data: { ids: idsSelecionados }, //Passa na URl as informações
                            success: function () {
                                location.reload()
                            },
                            error: function () {
                                alert(""Um erro ocorreu ao tentar deletar estes(s) registro(s)."")
                            }
                        });
                    }
                }
                else {
                    alert(""Por favor, selecione pelo menos um item para ser deletado"")
                }
            });

            $(""#pageSizeSelect"").on(""change"", function () {
                var pageSize = $(this).val();
                changePageSize(pageSize);
            });
        }

        $(document).ready(function () {
            initialFunction();
        });

        function refreshTable(pageNumber, pageSize) {
            $.ajax({
     ");
                WriteLiteral("           url: \'");
#nullable restore
#line 97 "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\People\Index.cshtml"
                 Write(Url.Action("PartialIndex", "People"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                type: 'GET',
                data: { pageNumber: pageNumber, pageSize: pageSize },
                success: function (data) {
                    $(""#container"").html(data);
                    initialFunction();
                },
                error: function () {
                    alert(""Um erro ocorreu ao tentar atualizar a tabela"")
                }
            });
        }

        function changePageSize(pageSize) {
            $.ajax({
                url: '");
#nullable restore
#line 112 "C:\Users\rodri\source\repos\SystemFH\SystemFH\Views\People\Index.cshtml"
                 Write(Url.Action("PartialIndex", "People"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                type: 'GET',
                data: { pageSize: pageSize },
                success: function (data) {
                    $(""#container"").html(data);
                    initialFunction();
                },
                error: function () {
                    alert(""Um erro ocorreu ao tentar atualizar a tabela"")
                }
            });
        }
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PaginationViewModel<Person>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
