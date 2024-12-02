using WebProjectG.Server.domain.Gebruiker;

namespace WebProjectG.Server.domain.EmailObserver
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }
}
