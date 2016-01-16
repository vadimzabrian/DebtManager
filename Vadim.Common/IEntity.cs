namespace Vadim.Common
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
