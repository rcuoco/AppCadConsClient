using System;
using System.Web.UI;

namespace AppCadConsClient
{
    public class Util
    {
        public void ShowMessage(Page page,string message)
        {
            message = ReplaceCaracterMessage(message);
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            script.Append($"alert('{message.Replace("'", "\\'")}');");

            ScriptManager.RegisterClientScriptBlock(page, this.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
        }
        public string ReplaceCaracterMessage(string message)
        {
            return message.Replace('\'', ' ').Replace('\\', '|').Replace('\"', ' ').Replace("\n", "\\n").Replace("\r", "\\r").Replace("'", "\"");
        }
    }
}