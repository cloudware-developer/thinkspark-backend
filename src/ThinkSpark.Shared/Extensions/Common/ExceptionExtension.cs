namespace ThinkSpark.Shared.Extensions.Common
{
    public static class ExceptionExtension
    {
        public static Exception ExceptionHandler(this Exception exception, string methodName = "", string objectName = "")
        {
            string message = string.Empty;

            if (string.IsNullOrEmpty(methodName) && string.IsNullOrEmpty(objectName))
            {
                if (exception.InnerException != null)
                    message = $"Erro {exception.InnerException.Message}";
                else
                    message = $"Erro {exception.Message}";
            }
            else if (!string.IsNullOrEmpty(methodName) && string.IsNullOrEmpty(objectName))
            {
                if (exception.InnerException != null)
                    message = $"Erro no método {methodName}. Erro {exception.InnerException.Message}";
                else
                    message = $"Erro no método {methodName}. Erro {exception.Message}";
            }
            else if (string.IsNullOrEmpty(methodName) && !string.IsNullOrEmpty(objectName))
            {
                if (exception.InnerException != null)
                    message = $"Erro no objeto {objectName}. Erro {exception.InnerException.Message}";
                else
                    message = $"Erro no objeto {objectName}. Erro {exception.Message}";
            }
            else
            {
                if (exception.InnerException != null)
                    message = $"Erro ao executar o método {methodName} no objeto [{objectName}]. Erro {exception.InnerException.Message}";
                else
                    message = $"Erro ao executar o método {methodName} no objeto [{objectName}]. Erro {exception.Message}";
            }

            var result = new Exception(message);
            return result;
        }
    }
}
