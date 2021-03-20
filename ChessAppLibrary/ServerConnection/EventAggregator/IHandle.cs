namespace ChessAppLibrary.ServerConnection.EventAggregator
{
    public interface IHandle<T> where T : class
    {
        void Handle(T eventArgs);
    }
}