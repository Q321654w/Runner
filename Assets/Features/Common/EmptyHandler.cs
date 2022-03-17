namespace Features.Common
{
    public class EmptyHandler<T> : IHandler<T>
    {
        public void Handle(T config)
        {
        }
    }
}