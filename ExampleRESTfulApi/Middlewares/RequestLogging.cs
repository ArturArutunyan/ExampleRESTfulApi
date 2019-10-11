using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ExampleRESTfulApi.Middlewares
{
    internal class RequestLogRow
    {
        public string RequestId { get; set; }
        public string Query { get; set; }
        public string Body { get; set; }
        public string Ip { get; set; }
        public string User { get; set; }
        public long Elapsed { get; set; }
        public int? Status { get; set; }
    }

    public class RequestLogging
    {
        private readonly RequestDelegate _next;
        private static object locker;
        private readonly string directoryPath;

        public RequestLogging(RequestDelegate next, string directoryPath)
        {
            this._next = next;
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            this.directoryPath = directoryPath;
            locker = new object();
        }

        public async Task InvokeAsync(HttpContext context)
        {        
            var stopwatch = Stopwatch.StartNew();
            var request = new RequestLogRow
            {
                RequestId = context.TraceIdentifier,
                Query = $"{context.Request.Method} {context.Request.Path}",
                Body = await GetRequestBody(context.Request),
                Ip = context.Connection.RemoteIpAddress.ToString()
            };

            await this._next.Invoke(context);
            request.Status = context.Response?.StatusCode;
            request.User = context.User.Identity.Name;

            stopwatch.Stop();
            request.Elapsed = stopwatch.ElapsedMilliseconds;

            var task = Task.Run(() =>
            {
                this.Logging(request);
            });
        }

        private void Logging(RequestLogRow log)
        {
            var date = DateTime.Now;
            var file = this.directoryPath + date.ToString("yyyy-MM-dd");
            var row = new StringBuilder();
            var formatFirst = " | {0}: {1}";
            var formatBody = "\n\t{0}: {1}";

            row.AppendLine();
            row.Append(date.ToString("HH:mm:ss"));
            row.AppendFormat(formatFirst, "RequestId", log.RequestId);
            row.AppendFormat(formatFirst, "Status", log.Status);
            row.AppendFormat(formatBody, "Query", log.Query);
            row.AppendFormat(formatBody, "Body", log.Body);
            row.AppendFormat(formatBody, "User", log.User);
            row.AppendFormat(formatBody, "Ip", log.Ip);
            row.AppendFormat(formatBody, "Elapsed", log.Elapsed);
            row.AppendLine();

            lock (locker)
            {
                System.IO.File.AppendAllText(file, row.ToString());
            }
        }

        private static async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;

            return bodyAsText;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        /// Ведение логов запросов. Прописывается вначале.
        /// </summary>
        /// <param name="builder">Расширяемый обьект</param>
        /// <param name="directoryPath">Пуль к директории</param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder, string directoryPath = "Logs/Requests/")
        {
            return builder.UseMiddleware<RequestLogging>(directoryPath);
        }
    }
}
