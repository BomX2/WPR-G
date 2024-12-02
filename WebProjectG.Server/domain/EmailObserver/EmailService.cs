namespace WebProjectG.Server.domain.EmailObserver
{
    public class EmailService : IObserver
    {
        public void Update(string message)
        {
            //add logic to send emails
            Console.WriteLine($"Bevestigings mail verstuurd {message}");
        }
    }
}
