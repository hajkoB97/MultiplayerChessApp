namespace ChessAppLibrary.ServerConnection.EventAggregator
{
    public interface IEventAggregator
    {
        void Subscribe(object subsriber);
        void Publish<T>(T message) where T : class;
        void Unsubscribe(object subscriber);
    }
}