using DataServices.Models;
using DataServices.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IO;
using MongoDB.Driver;
using System.Text;

namespace LineMessage.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public LogMiddleware(RequestDelegate next)
        {
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var contextDb = context.RequestServices.GetRequiredService<MongoDBServices>();
            var log = await LogRequestAsTextAsync(context, contextDb);
            await _next.Invoke(context);
            await LogGetResponseAsTextAsync(context, log, contextDb);
        }
        private async Task<RequestResponseLogModel> LogRequestAsTextAsync(HttpContext context, MongoDBServices mongoDBServices)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            context.Request.Body.Position = 0;
            var builderHeader = new StringBuilder(Environment.NewLine);
            foreach (var header in context.Request.Headers)
                builderHeader.AppendLine($"{header.Key}:{header.Value}");
            RequestResponseLogModel log = new RequestResponseLogModel()
            {
                method = context.Request.Method,
                ClientIp = context.Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                RequestHeader = builderHeader.ToString(),
                RequsetBody = ReadStreamInChunks(requestStream),
                Url = context.Request.GetDisplayUrl(),
                Part = context.Request.Path,
                CreateDate = DateTime.UtcNow
            };
            log = await mongoDBServices.InsertDocumentAsync<RequestResponseLogModel>(log);
            return log;
        }

        private async Task LogGetResponseAsTextAsync(HttpContext context, RequestResponseLogModel requsetLog, MongoDBServices mongoDBServices)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var builderHeader = new StringBuilder(Environment.NewLine);
            foreach (var header in context.Response.Headers)
                builderHeader.AppendLine($"{header.Key}:{header.Value}");
            var filter = Builders<RequestResponseLogModel>.Filter.Eq("Id", requsetLog.Id);
            var update = Builders<RequestResponseLogModel>.Update.Set(x => x.ResponseBody, context.Request.Method == "GET" ? "" : text)
                                                                 .Set(x => x.ResponseHeader, builderHeader.ToString())
                                                                 .Set(x => x.UpdateDate, DateTime.UtcNow)
                                                                 .Set(x => x.StatusCode, context.Response.StatusCode);
            await mongoDBServices.UpdateDocumentAsync<RequestResponseLogModel>(filter, update);
            await responseBody.CopyToAsync(originalBodyStream);
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}


