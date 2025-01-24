using WebProjectG.Server.domain.GebruikerFiles;

namespace WebProjectG.Server.domain.EmailObserver
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }
}
