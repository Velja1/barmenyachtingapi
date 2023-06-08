using BarmenYachting.Application.Emails;

namespace BarmenYachting.Api.Emails
{
    public class TestEmailSender : IEmailSender
    {
        public void Send(MessageDto message)
        {
            System.Console.WriteLine("Sending email:");
            System.Console.WriteLine("To: " + message.To);
            System.Console.WriteLine("From: " + message.From);
            System.Console.WriteLine("Title: " + message.Title);
            System.Console.WriteLine("Body: " + message.Body);
        }
    }
}
