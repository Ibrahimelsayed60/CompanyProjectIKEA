using LinkDev.IKEA.DAL.Entities.Emails;
using System.Net.Mail;
using System.Net;

namespace LinkDev.IKEA.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("ibrahimelsayed3015@gmail.com", "jiqqqtzeiyeufhoh");
			Client.Send("ibrahimelsayed3015@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
