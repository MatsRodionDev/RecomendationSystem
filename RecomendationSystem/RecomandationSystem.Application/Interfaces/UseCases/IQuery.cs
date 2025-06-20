namespace RecomandationSystem.Application.Interfaces.UseCases
{
    public interface IQuery<TResponse>;
    public interface ICachedQuery<TResponse> 
    { 
        public string Key { get; }
    }
}
