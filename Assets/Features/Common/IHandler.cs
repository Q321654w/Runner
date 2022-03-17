namespace Features.Common
{
    public interface IHandler<T>
    {
        void Handle(T config);
    }
}