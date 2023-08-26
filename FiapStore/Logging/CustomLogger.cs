namespace FiapStore.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly CustomLoggerProviderConfiguration _configuration;

        public CustomLogger(string nome, CustomLoggerProviderConfiguration configuration)
        {
            _loggerName = nome;
            _configuration = configuration;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var mensagem = string.Format($"{logLevel}: {eventId} - {formatter(state, exception)}");
            ExcreverTextoNoArquivo(mensagem);
        }

        private void ExcreverTextoNoArquivo(string mensagem)
        {
            var caminhoDoArquivo = @$"C:\Fiap\FiapStore\FiapStore\bin\Log-{DateTime.Now:yyyy-MM-dd}.txt";
            if (!File.Exists(caminhoDoArquivo))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoDoArquivo));
                File.Create(caminhoDoArquivo).Dispose();
            }

            using StreamWriter streamWriter = new StreamWriter(caminhoDoArquivo, true);
            streamWriter.WriteLine(mensagem);
            streamWriter.Close();
        }
    }
}
