// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | HtmlExtensions.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using Gaswerk.RouteApp.Models;

namespace Gaswerk.RouteApp.Code
{

    public static class HtmlExtensions
    {
        public static MvcHtmlString MyValidationSummary(this HtmlHelper htmlHelper)
        {
            if (!htmlHelper.ViewContext.ViewData.ModelState.IsValid)
            {
                return htmlHelper.ValidationSummary(false, null, new { @class = "alert alert-warning" });
            }

            return null;
        }

        public static MvcHtmlString MySchwierigkeitsgradDropDown<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, Schwierigkeit>> expression
            )
        {
            var value = expression.Compile().Invoke(htmlHelper.ViewData.Model);

            IEnumerable<SelectListItem> seletList = Schwierigkeit.GetAllSchwierigkeiten()
                                                                 .Select(s => new SelectListItem
                                                                 {
                                                                     Text = s.ToString(),
                                                                     Value = s.ToString(),
                                                                     Selected = value == s
                                                                 });

            var r =  htmlHelper.DropDownListFor(expression, seletList);
            return r;
        }
    }

}