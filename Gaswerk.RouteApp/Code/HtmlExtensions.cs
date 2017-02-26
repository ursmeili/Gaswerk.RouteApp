// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | HtmlExtensions.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

using Gaswerk.RouteApp.Models;

using JetBrains.Annotations;

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

            var r = htmlHelper.DropDownListFor(expression, seletList);
            return r;
        }

        /// <summary>
        /// Renders a button which provides an &lt;input&gt; (type=submit) tag (target: controller Http-Post method)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedParameter.Global
        public static MvcHtmlString SubmitButton<TModel>(this HtmlHelper<TModel> htmlHelper,
                                                       ButtonType type,
                                                       string text)
        {
            var htmlAttributes = HtmlExtensions.GetButtonHtmlAttributes(type);

            var b = new TagBuilder("input");
            b.MergeAttribute("type", "submit");
            b.MergeAttribute("value", text);
            b.MergeAttributes<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            var s = new MvcHtmlString(b.ToString(TagRenderMode.Normal));
            return s;
        }

        /// <summary>
        /// Renders a button which provides an &lt;a&gt; tag (target: controller Http-Get method)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString LinkButton<TModel>(this HtmlHelper<TModel> htmlHelper,
                                                   ButtonType type,
                                                   string text,
                                                   [AspMvcAction]
                                                   string actionName,
                                                   [AspMvcController]
                                                   string controllerName=null,
                                                   object id = null)
        {
            var htmlAttributes = HtmlExtensions.GetButtonHtmlAttributes(type);
            if (id != null)
            {
                object routeValues = new { id = id };
                return htmlHelper.ActionLink(text, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
            {
                return htmlHelper.ActionLink(text, actionName, controllerName, htmlAttributes);
            }
        }

        private static object GetButtonHtmlAttributes(ButtonType type)
        {
            string c;
            switch (type)
            {
                case ButtonType.Info:
                    c = "info";
                    break;
                case ButtonType.Success:
                    c = "success";
                    break;
                case ButtonType.Danger:
                    c = "danger";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var htmlAttributes = new { @class = $"btn btn-{c}" };
            return htmlAttributes;
        }
    }

    public enum ButtonType
    {
        Info,
        Success,
        Danger

    }

}