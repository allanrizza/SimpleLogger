namespace SimpleLogger.Tests
{
    public class LoggerTests
    {
        private static readonly string logDirectory = "Logs";
        private static readonly string logFilePath = Path.Combine(logDirectory, "log.txt");

        [Fact]
        public void Log_CreatesLogDirectory_WhenNotExists()
        {
            //ARRANJO
            if (Directory.Exists(logDirectory))
                Directory.Delete(logDirectory, true);

            //A��O
            Logger.Log("Testando cria��o de diret�rio");

            //AFIRMA��O
            Assert.True(Directory.Exists(logDirectory));
        }

        [Fact]
        public void Log_CreatesLogFile_WhenLogIsCalled()
        {
            //ARRANJO
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            //A��O
            Logger.Log("Testando a cria��o de arquivo de log");

            //AFIRMA��O
            Assert.True(File.Exists(logFilePath), "O arquivo de log n�o foi criado.");
        }

        [Fact]
        public void Log_WritesCorrectMessageToLogFile()
        {
            //ARRANJO
            string logMessage = "Mensagem de log de teste";
            Logger.Log(logMessage);

            //A��O
            string logContent = File.ReadAllText(logFilePath);

            //AFIRMA��O
            Assert.Contains(logMessage, logContent);
        }
    }
}