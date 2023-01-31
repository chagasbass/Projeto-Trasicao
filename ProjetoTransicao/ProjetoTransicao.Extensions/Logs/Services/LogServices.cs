using ProjetoTransicao.Extensions.Logs.Entities;
using Serilog;

namespace ProjetoTransicao.Extensions.Logs.Services
{
    public class LogServices : ILogServices
    {
        public LogData LogData { get; set; }

        private readonly ILogger _logger = Log.ForContext<LogServices>();

        public LogServices()
        {
            LogData = new LogData();
        }

        public void WriteLog()
        {
            _logger.Information("[LogRequisição]:{mensagem} [RequestData]:{@RequestData} [RequestQuery]:{RequestQuery} [ResponseData]:{@ResponseData}" +
               "[Timestamp]:{Timestamp}", LogData.Mensagem, LogData.RequestData, LogData.RequestQuery, LogData.ResponseData, LogData.Timestamp);

            LogData.ClearLogData();
        }

        public void WriteLogWhenRaiseExceptions()
        {
            if (LogData is not null && LogData.Exception is not null)
            {
                _logger.Error("[ExceptionType]:{Name} [ExceptionMessage]:{Message}",
                    LogData.Exception.GetType().Name, LogData.Exception.Message);

                _logger.Error($"[ExceptionStackTrace]:{LogData.Exception.StackTrace}");

                if (LogData?.Exception?.InnerException is not null)
                {
                    _logger.Error("[InnerException]:{LogData.Exception?.InnerException?.Message}");
                }

                LogData.ClearLogExceptionData();
            }
        }

        public void CreateStructuredLog(LogData logData) => LogData = logData;

        public void WriteMessage(string mensagem) => _logger.Information($"{mensagem} - {LogData.Timestamp}");

        public void WriteErrorLog()
        {
            _logger.Error("[LogRequisição]:{mensagem} [RequestData]:{@RequestData}   [RequestQuery]:{RequestQuery}" +
                "[Method]:{RequestMethod} [Path]:{RequestUri} [RequestTraceId]:{TraceId} " +
                "[ResponseData]:{@ResponseData} [ResponseStatusCode]:{@ResponseStatusCode}",
                LogData.Mensagem, LogData.RequestData, LogData.RequestQuery, LogData.RequestMethod, LogData.RequestUri,
                LogData.TraceId, LogData.ResponseData, LogData.ResponseStatusCode);

            LogData.ClearLogData();
        }

        public void WriteLogFromResiliences()
        {
            _logger.Information("[LogRequisição]:{mensagem} [RequestUri]:{RequestUri} [ResponseStatusCode]:{ResponseStatusCode}  [RequestData]:{@RequestData}  [RequestQuery]:{RequestQuery} [ResponseData]:{@ResponseData}" +
                 "[Timestamp]:{Timestamp}", LogData.Mensagem, LogData.RequestUri, LogData.ResponseStatusCode, LogData.RequestData, LogData.RequestQuery, LogData.ResponseData, LogData.Timestamp);

            LogData.ClearLogData();
        }
    }
}