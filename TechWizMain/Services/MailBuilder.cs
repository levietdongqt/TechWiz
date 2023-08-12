using Humanizer;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using TechWizMain.Models;

namespace TechWizMain.Services
{
	public class MailBuilder
	{
		public string headerMailOrder = "<h2>Payment Confirmation</h2>";
		public string headerMailContact = "<h2>Thank You for Your Feedback</h2>";
		public string footerMail;
		public string Name = "Huy-Tran";
		public string Company = "Plan Nest";
		public string Phone = "012345678";
		public string Email = "plannest@gmail.com";

		public MailBuilder() 
		{
			footerMail = "<p>Best regards,<br>\r\n"+ Name +"<br>\r\n" + Company +"<br>\r\n"+ Phone + "<br>\r\n" +Email +"</p>";
		}
		public string BuildMailOrders(string UserName, DateTime OrderDate, decimal Total, string DeliveryAddress)
		{
			string mail = headerMailOrder
			+ "<p>Dear " + UserName + ",</p> "
			+ "<p> We would like to express our sincere gratitude for choosing to use our services / products and for successfully completing the payment for your order. Below are the details of your payment:</p>"
			+ "<ul> "
			+ "<li><strong>Total Amount:</strong>" + Total + "</li>"    
			+"<li><strong>Payment Date:</strong>" + OrderDate.ToString() + "</li>"    
			+"<li><strong>Delivery Address:</strong>" + DeliveryAddress + "</li>"
			+ "</ul>"
			+ footerMail;
			return mail;
		}

		public string BuilderMailContact(string UserName)
		{
			string mail = headerMailContact
			+ "<p>Dear, " + UserName + "</p>"
			+ "<p>I hope this email finds you well. I wanted to take a moment to express my sincere appreciation for the feedback you provided. Your insights and thoughts are incredibly valuable to me, and I'm grateful for your time and effort in sharing your perspective.</p>\r\n    <p>Your feedback will help me improve and grow, and I truly value your honesty. Please know that your input matters to me, and I'm committed to using it constructively.</p>\r\n    <p>Once again, thank you for taking the time to provide your feedback. I'm looking forward to implementing the suggested improvements and working towards delivering a better experience.</p>"
			+ footerMail;
			return mail;

		}

	}
}
