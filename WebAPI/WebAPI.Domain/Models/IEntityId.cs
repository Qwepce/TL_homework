namespace WebAPI.Domain.Models;
public interface IEntityId<T> where T : struct
{
    public T Id { get; init; }
}