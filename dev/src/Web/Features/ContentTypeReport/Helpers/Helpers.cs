using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace Perficient.Web.Features.ContentTypeReport.Helpers
{
    public static class HTMLTableHelper
    {
        public static string ToHtmlTable<T>(this IEnumerable<T> enums, string tblColor = "#040A47")
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var html = new StringBuilder("<table id='tblId' width='80%' style='border: 1px solid black; border-collapse: collapse;'>");

            //Header
            html.Append("<thead style='background-color:" + tblColor + ";color:white'><tr>");
            foreach (var p in props)
                html.Append("<th>" + p.Name + "</th>");
            html.Append("</tr></thead>");

            //Body
            html.Append("<tbody>");
            foreach (var e in enums)
            {
                html.Append("<tr>");
                props.Select(s => s.GetValue(e)).ToList().ForEach(p => {
                    html.Append("<td>" + p + "</td>");
                });
                html.Append("</tr>");
            }

            html.Append("</tbody>");
            html.Append("</table>");
            return html.ToString();
        }
        public static string ToHtmlTableAndSubTable<T>(this IEnumerable<T> enums)
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var html = new StringBuilder("<table id='tblId' width='80%' style='border: 1px solid black; border-collapse: collapse;'>");
            int totalCount = 0;
            int Id = 0;
            //Header
            html.Append("<thead style='background-color:#040A47;color:white'><tr>");
            foreach (var p in props)
            {
                if (p.Name != "InstantReferences")
                {
                    html.Append("<th>" + p.Name + "</th>");
                }
            }
            html.Append("</tr></thead>");

            //Body
            html.Append("<tbody>");
            foreach (var e in enums)
            {
                html.Append("<tr>");
                totalCount = 0;
                var allProp = props.Select(s => s.GetValue(e)).ToList();
                foreach (var propValue in allProp)
                {
                    totalCount++;
                    Id = totalCount == 1 ? Convert.ToInt32(propValue) : Id;
                    if (allProp.Count == totalCount)
                    {
                        html.Append("</tr>");
                        html.Append("<tr style='display:none' id='tr" + Id + "'><td colspan='" + props.Count() + "'>" + propValue + "</td></tr>");
                    }
                    else
                    {
                        html.Append("<td>" + propValue + "</td>");
                    }
                }
            }

            html.Append("</tbody>");
            html.Append("</table>");
            return html.ToString();
        }
    }
}
