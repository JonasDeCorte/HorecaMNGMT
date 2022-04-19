using NLog;
using System.Text;
using System;

namespace Horeca.API.Middleware
{
    public class RequestResponseLogginMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RequestResponseLogginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);
            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;
            //Create a new memory stream...
            using var responseBody = new MemoryStream();
            //...and use that for the temporary response body
            context.Response.Body = responseBody;
            //Continue down the Middleware pipeline, eventually returning to this class
            try
            {
                await _next(context);
            }
            finally
            {
                logger.Info(
                    string.Format("Request {{URL}} -> {0} {1} {2} {3}",
                    context.Request?.Method,
                    context.Request?.Host,
                    context.Request?.Path.Value,
                    context.Request?.QueryString.Value));
                logger.Info(
                   string.Format("Request {{More info}} {{Protocol}} -> {0},  {{ContentType}} -> {1} ",
                   context.Request?.Protocol,
                   context.Request?.ContentType));
                foreach (var heather in context.Request.Headers)
                {
                    logger.Debug("HEATHER -> " + heather.Key + " " + heather.Value);
                }
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                var reqBody = await reader.ReadToEndAsync();
                logger.Info($"{{RequestBody}} -> {reqBody}");
            }
            //Format the response from the server
            var response = await FormatResponse(context.Response);
            //TODO: Save log to chosen datastore
            //Copy the contents of the new memory stream (which contains the  response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer).ConfigureAwait(false);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;
            return $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{request.Scheme}  {request.Host}{request.Path} {request.QueryString}  {bodyAsText}{Environment.NewLine}{Environment.NewLine}";
        }

        private static async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);
            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            //We need to reset the reader for the response so that the client can  read it.
            response.Body.Seek(0, SeekOrigin.Begin);
            //Return the string for the response, including the status code (e.g.  200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
    }
}