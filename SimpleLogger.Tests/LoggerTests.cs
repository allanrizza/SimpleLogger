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

            //AÇÃO
            Logger.Log("Testando criação de diretório");

            //AFIRMAÇÃO
            Assert.True(Directory.Exists(logDirectory));
        }

        [Fact]
        public void Log_CreatesLogFile_WhenLogIsCalled()
        {
            //ARRANJO
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            //AÇÃO
            Logger.Log("Testando a criação de arquivo de log");

            //AFIRMAÇÃO
            Assert.True(File.Exists(logFilePath), "O arquivo de log não foi criado.");
        }

        [Fact]
        public void Log_WritesCorrectMessageToLogFile()
        {
            //ARRANJO
            string logMessage = "Mensagem de log de teste";
            Logger.Log(logMessage);

            //AÇÃO
            string logContent = File.ReadAllText(logFilePath);

            //AFIRMAÇÃO
            Assert.Contains(logMessage, logContent);
        }
    }
}