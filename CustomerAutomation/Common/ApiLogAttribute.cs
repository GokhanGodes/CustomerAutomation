using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerAutomation.Common
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiLogAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller != null)
            {
                Log(string.Concat(Directory.GetCurrentDirectory(), @"\ApiLogs", @"\") + DateTime.Now.ToString("yyyyMMdd") + ".txt",
                    $"Type: Request" + Environment.NewLine +
                    $"Method:{context.HttpContext.Request.Method}" + Environment.NewLine +
                    $"ApiAddress:{context.HttpContext.Request.Path.Value}" + Environment.NewLine +
                    $"Date: { DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}" + Environment.NewLine +
                    $"Request Parameters:{Environment.NewLine}{ string.Join(", ", context.ActionArguments.Select(x => x.Key + " => " + JsonSerializer.Serialize(x.Value, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping })).ToList())}"
                    + Environment.NewLine + Environment.NewLine);
            }
            return base.OnActionExecutionAsync(context, next);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log(string.Concat(Directory.GetCurrentDirectory(), @"\ApiLogs", @"\") + DateTime.Now.ToString("yyyyMMdd") + ".txt",
                $"Type: Response" + Environment.NewLine +
                $"Method:{context.HttpContext.Request.Method}" + Environment.NewLine +
                $"ApiAddress:{context.HttpContext.Request.Path.Value}" + Environment.NewLine +
                $"Date: { DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}" + Environment.NewLine +
                $"Response Status: {context.HttpContext.Response.StatusCode}" + Environment.NewLine +
                $"Response Value: {Environment.NewLine}{ (!(context.Result is null || (context.Result as ObjectResult) is null) ? JsonSerializer.Serialize((context.Result as ObjectResult).Value, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }) : string.Empty)} "
                + Environment.NewLine + Environment.NewLine);
            base.OnActionExecuted(context);
        }
        private void Log(string path, string content)
        {
            File.AppendAllText(path, content);
        }

    }
}
