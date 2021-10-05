using Linko.Domain;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Linko.Helper.Repository
{
    public class LoggerRepository : ILoggerRepository, IRegisterSingleton
    {
        private readonly string logFilePath = @"Files\Log.txt";
        private readonly IDapperRepository<Empty> _dapper;

        public LoggerRepository(IDapperRepository<Empty> dapper)
        {
            _dapper = dapper;
        }

        public void Write(Exception exception, string message)
        {
            string error = $"Msg={message};";

            if (exception is not null)
            {
                error += $"Ex.Msg={exception.Message};";

                if (exception.InnerException is not null)
                    error += $"InEx.Msg={exception.InnerException.Message};";
            }

            try
            {
                _dapper.RunScript($"INSERT INTO dbo.Logger " +
                    $"values ({Key.DateTimeIQ}, N'{message}', N'{error}');");
            }
            catch (Exception ex)
            {
                string message_2 = "LoggerRepository=>;WriteAsync";
                string error_2 = $"Msg={message_2}";

                if (ex is not null)
                {
                    error_2 += $"Ex.Msg={ex.Message};";

                    if (ex.InnerException is not null)
                        error_2 += $"InEx.Msg={ex.InnerException.Message};";
                }

                string content_1 = $"LogDT = {Key.DateTimeIQ} \n" +
                    $" LogMsg = {message} \n LogDesc = {error} \n\n" +
                    $"********************************************** \n\n";

                string content_2 = $"LogDT = {Key.DateTimeIQ} \n" +
                    $" LogMsg = {message_2} \n LogDesc = {error_2} \n\n" +
                    $"********************************************** \n\n";

                File.AppendAllText(logFilePath, content_1 + content_2);
            }
        }

        public async Task WriteAsync(Exception? exception, string message)
        {
            string error = $"Msg={message};";

            if (exception is not null)
            {
                error += $"Ex.Msg={exception.Message};";

                if (exception.InnerException is not null)
                    error += $"InEx.Msg={exception.InnerException.Message};";
            }

            try
            {
                await _dapper.RunScriptAsync($"INSERT INTO dbo.Logger " +
                    $"values ({Key.DateTimeIQ}, N'{message}', N'{error}');");
            }
            catch (Exception ex)
            {
                string message_2 = "LoggerRepository=>;WriteAsync";
                string error_2 = $"Msg={message_2}";

                if (ex is not null)
                {
                    error_2 += $"Ex.Msg={ex.Message};";

                    if (ex.InnerException is not null)
                        error_2 += $"InEx.Msg={ex.InnerException.Message};";
                }

                string content_1 = $"LogDT = {Key.DateTimeIQ} \n" +
                    $" LogMsg = {message} \n LogDesc = {error} \n\n" +
                    $"********************************************** \n\n";

                string content_2 = $"LogDT = {Key.DateTimeIQ} \n" +
                    $" LogMsg = {message_2} \n LogDesc = {error_2} \n\n" +
                    $"********************************************** \n\n";

                await File.AppendAllTextAsync(logFilePath, content_1 + content_2);
            }
        }
    }
}

