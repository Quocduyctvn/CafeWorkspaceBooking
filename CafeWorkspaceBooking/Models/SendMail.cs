using System.Net.Mail;

namespace CafeWorkspaceBooking.Models
{
	public class SendMail
	{
		public string ToEmailAddress { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
		//public Attachment Attachment { get; set; } // nếu có tệp đính kèm  
	}
}
