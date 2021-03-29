using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Bakery.Middlewares
{
    public class LoggingInfo
    {
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public Dictionary<string, string> RequestHeaders { get; set; }
        public string RequestHost { get; set; }
        public string RequestProtocol { get; set; }
        public Dictionary<string, string> RequestForm { get; set; }
    }

    public static class LoggingInfoExtensions
    {
        public static LoggingInfo Error(this LoggingInfo info, Exception ex, string template, string requestMethod,
            string requestPath, int? statusCode, TimeSpan elapsed)
        {
            var message = string.Format(template, requestMethod, requestPath, statusCode, elapsed.ToString());
            info.Message = message;
            info.ExceptionMessage = ex.Message;
            info.StackTrace = ex.StackTrace;

            return info;
        }

        public static string ToJsonString(this LoggingInfo info)
        {
            return JsonSerializer.Serialize<LoggingInfo>(info);
        }
    }
    
    public class LoggingMiddleware
    {
        private const string MessageTemplate =
            "HTTP {0} {1} responded {2} in {3} ms";

        private readonly ILogger _logger;

        readonly RequestDelegate _next;


        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            _next = next;
            _logger = logger;
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var sw = Stopwatch.StartNew();
            try
            {
                await _next(httpContext);
                sw.Stop();

                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

                var logInfo = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : null;
                if (logInfo != null)
                    _logger.LogError(logInfo.Error(new Exception("Server Error"), MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed)
                        .ToJsonString());
            }
            // Never caught, because `LogException()` returns false.
            catch (Exception ex) when (LogException(httpContext, sw, ex)) { }
        }
        
        private bool LogException(HttpContext httpContext, Stopwatch sw, Exception ex)
        {
            sw.Stop();
            
            _logger.LogError(LogForErrorContext(httpContext)
                .Error(ex, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed)
                .ToJsonString());

            return false;
        }
        
        private static LoggingInfo LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;
            
            var requestHeaders = request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
            var requestHost = request.Host.Value;
            var requestProtocol = request.Protocol;

            var result = new LoggingInfo()
            {
                RequestHeaders = requestHeaders,
                RequestHost = requestHost,
                RequestProtocol = requestProtocol,
                RequestForm = request.HasFormContentType
                    ? request.Form.ToDictionary(v => v.Key, v => v.Value.ToString())
                    : new Dictionary<string, string>()
            };
            
            return result;
        }
    }
}