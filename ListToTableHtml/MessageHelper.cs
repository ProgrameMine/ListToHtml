using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ListToTableHtml
{
    public class MessageHelper<T>
    {
        private static string tableHeader = $"<table border=\"1\" cellspacing=\"1\" cellpadding=\"1\" width=\"800\"><tbody><tr>";
        private static string tableFooter = $"</tr></tbody><tbody>";

        /// <summary>
        /// 创建表格Html语句
        /// </summary>
        /// <param name="tableName">标题</param>
        /// <param name="list">泛型集合</param>
        /// <param name="showTitle">是否显示标题名</param>
        /// <returns></returns>
        public static StringBuilder CreateTable(string title, List<T> list, bool showTitle = false)
        {
            var messageList = MessageList();
            if (messageList == null || messageList.Count == 0) return null;

            StringBuilder sb = new StringBuilder();

            if (showTitle)
                sb.Append($"<p><span style =\"font-size:16px;\">" + title + "</span>");
            sb.Append(tableHeader);
            foreach (var item in messageList)
            {
                sb.Append($"<th scope=\"col\">" + item.Key + "</th>");
            }
            sb.Append(tableFooter);

            list.ForEach(n =>
            {
                sb.Append("<tr>");
                foreach (var item in messageList)
                {
                    sb.Append($"<td>" + GetValueByFile(item.Value, n) + "</td>");
                }
                sb.Append("</tr>");
            });

            sb.Append($"</tbody></table></p>");

            return sb;
        }

        #region 获取列名
        private static Dictionary<string, string> MessageList()
        {
            Type type = typeof(T);
            PropertyInfo[] pArray = type.GetProperties(); //集合属性数组

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            foreach (PropertyInfo p in pArray)
            {
                var message = GetMessageAttributeByField(p.Name);
                if (string.IsNullOrEmpty(message)) continue;
                keyValues.Add(message, p.Name);
            }

            return keyValues;
        }

        /// <summary>
        /// 获取Description标签的值
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static string GetMessageAttributeByField(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[field].Attributes;
            MessageAttribute myAttribute = (MessageAttribute)attributes[typeof(MessageAttribute)];
            if (myAttribute == null) return "";
            return myAttribute.ColName;
        }

        private static MessageAttribute GetAttributeByField(string field)
        {
            if (string.IsNullOrEmpty(field)) return null;
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[field].Attributes;
            MessageAttribute myAttribute = (MessageAttribute)attributes[typeof(MessageAttribute)];
            return myAttribute;
        }
        #endregion

        #region 获取list值
        private static string GetValueByFile(string field, T t)
        {
            if (string.IsNullOrEmpty(field)) return "";
            Type type = typeof(T);
            PropertyInfo[] pArray = type.GetProperties(); //集合属性数组

            object value = "";
            foreach (PropertyInfo p in pArray)
            {
                if (p.Name != field) continue;
                value = p.GetValue(t, null);
                if (value == null) value = "";
                break;
            }

            var str = value.ToString();
            var unit = GetAttributeByField(field)?.Unit;
            return str + unit;
        }
        #endregion
    }
}
