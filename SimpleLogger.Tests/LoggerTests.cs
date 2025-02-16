namespace SimpleLogger.Tests
{
    public class LoggerTests
    {
        private static readonly string logDirectory = "Logs";
        private static readonly string logFilePath = Path.Combine(logDirectory, "log.txt");
        public LoggerTests()
        {
            // Limpar o arquivo de log antes de cada teste (para garantir resultados consistentes)
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            // Limpar o diret�rio de logs (caso tenha sido criado por testes anteriores)
            if (Directory.Exists(logDirectory))
            {
                Directory.Delete(logDirectory, true);
            }
        }
        public void Dispose()
        {
        }

        [Fact]
        public void Log_CreatesLogDirectory_WhenNotExists()
        {
            // arranjo
            if(Directory.Exists(logDirectory))
                Directory.Delete(logDirectory, true);

            // a��o
            Logger.Log("Testando cria��o de diret�rio");

            // afirma��o
            Assert.True(Directory.Exists(logDirectory)); 
        }

        [Fact]
        public void Log_CreatesLogFile_WhenLogIsCalled()
        {
            //arranjo
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            //a��o
            Logger.Log("Testando a cria��o de arquivo de log");

            //afirma��o
            Assert.True(File.Exists(logFilePath), "O arquivo de log n�o foi criado.");
        }

        [Fact]
        public void Log_WritesCorrectMessageToLogFile()
        {
            //arranjo
            string logMessage = "Mensagem de log de teste";
            Logger.Log(logMessage);

            //a��o
            string logContent = File.ReadAllText(logFilePath);

            //afirma��o
            Assert.Contains(logMessage, logContent);
        }
    }
}