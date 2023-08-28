namespace ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Add
{
    public interface IAddApplication<TEntity, TEntityVm> where TEntity : class
    {
        Task AdicionarAsync(TEntityVm model);
        Task AdicionarAsync(List<TEntityVm> collection);
    }
}
