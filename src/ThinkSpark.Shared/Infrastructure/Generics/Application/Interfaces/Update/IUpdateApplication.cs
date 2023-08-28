namespace ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Update
{
    public interface IUpdateApplication<TEntity, TEntityVm> where TEntity : class
    {
        Task AtualizarAsync(TEntityVm model);
        Task AtualizarAsync(List<TEntityVm> collection);
    }
}
