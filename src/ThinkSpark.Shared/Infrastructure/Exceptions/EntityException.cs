using FluentValidation.Results;

namespace ThinkSpark.Shared.Infrastructure.Exceptions
{
    public class EntityException : Exception
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public EntityException()
        {
        }

        /// <summary>
        /// Construtor parametrizado.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        public EntityException(string message) : base(message)
        {
        }

        /// <summary>
        /// Construtor parametrizado.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        /// <param name="inner">Exceção interna ou exceção principal, início do erro.</param>
        public EntityException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Construtor parametrizado.
        /// </summary>
        /// <param name="Errors">Adiciona uma lista de exceções.</param>
        public EntityException(List<ValidationFailure> Errors)
        {

        }

        /// <summary>
        /// Construtor parametrizado.
        /// </summary>
        /// <param name="message">Mensagem de erro generalizada e/ou resumida para a lista de erros passada.</param>
        /// <param name="Errors">Adiciona uma lista de exceções.</param>
        public EntityException(string message, List<ValidationFailure> Errors)
        {
        }
    }
}
