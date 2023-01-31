using ProjetoTransicao.Extensions.Logs.Entities;

namespace ProjetoTransicao.Extensions.Logs.Services
{
    public interface ILogServices
    {
        public LogData LogData { get; set; }
        void WriteLog();
        void WriteErrorLog();
        void CreateStructuredLog(LogData logData);
        void WriteLogWhenRaiseExceptions();
        void WriteLogFromResiliences();
        void WriteMessage(string message);
    }
}