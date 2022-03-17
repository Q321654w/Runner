namespace Features.Common
{
    public interface IFactory<T>
    {
        T Create();
    }
}