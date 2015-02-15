using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmptyFlow.EmaFluent {

	/// <summary>
	/// EmaFluent main interface.
	/// </summary>
	public interface IEmaFluent : IDisposable {

		/// <summary>
		/// Set settings.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		IEmaFluent SetSendingSettings ( string connectionString );

		/// <summary>
		/// Set settings.
		/// </summary>
		/// <param name="emailSendingProvider">Email sending provider.</param>
		IEmaFluent SetSendingSettings ( IEmailSendingProvider emailSendingProvider );

		/// <summary>
		/// Set settings.
		/// </summary>
		/// <param name="action">Action.</param>
		IEmaFluent SetSendingSettings ( Action<IEmailSendingProvider> action );

		/// <summary>
		/// To.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		IEmaFluent To ( string emailAddress );

		/// <summary>
		/// To.
		/// </summary>
		/// <param name="emailAddress">Email addresses.</param>
		IEmaFluent To ( IEnumerable<string> emailAddresses );

		/// <summary>
		/// From.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		IEmaFluent From ( string emailAddress );

		/// <summary>
		/// Bcc.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		IEmaFluent Bcc ( string emailAddress );

		/// <summary>
		/// Bcc.
		/// </summary>
		/// <param name="emailAddress">Email addresses.</param>
		IEmaFluent Bcc ( IEnumerable<string> emailAddresses );

		/// <summary>
		/// Cc.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		IEmaFluent Cc ( string emailAddress );

		/// <summary>
		/// Cc.
		/// </summary>
		/// <param name="emailAddress">Email addresses.</param>
		IEmaFluent Cc ( IEnumerable<string> emailAddresses );

		/// <summary>
		/// Reply to.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		IEmaFluent ReplyTo ( string emailAddress );

		/// <summary>
		/// Reply to.
		/// </summary>
		/// <param name="emailAddress">Email address sequence.</param>
		IEmaFluent ReplyTo ( IEnumerable<string> emailAddresses );

		/// <summary>
		/// Body encoding.
		/// </summary>
		/// <param name="encoding">Encoding.</param>
		IEmaFluent BodyEncoding ( Encoding encoding );

		/// <summary>
		/// Subject encoding.
		/// </summary>
		/// <param name="encoding">Encdoing.</param>
		IEmaFluent SubjectEncoding ( Encoding encoding );

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		IEmaFluent Subject ( string subject );

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		/// <param name="formatArgument">Format argument.</param>
		IEmaFluent Subject ( string subject , IEnumerable<object> formatArgument );

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		/// <param name="encoding">Subject encoding.</param>
		IEmaFluent Subject ( string subject , Encoding encoding );

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		/// <param name="formatArgument">Format arguments.</param>
		/// <param name="encoding">Subject encoding.</param>
		IEmaFluent Subject ( string subject , IEnumerable<object> formatArgument , Encoding encoding );

		/// <summary>
		/// Body.
		/// </summary>
		/// <param name="body">Body.</param>
		IEmaFluent Body ( string body , bool isHtml = false );

		/// <summary>
		/// Body.
		/// </summary>
		/// <param name="renderEngine">Render engine.</param>
		/// <param name="templateName">Template name.</param>
		/// <param name="model">Model.</param>
		IEmaFluent Body<T> ( IRenderEngine renderEngine , string templateName , T model , bool isHtml = false );

		/// <summary>
		/// Add attachment.
		/// </summary>
		/// <param name="stream">Stream.</param>
		/// <param name="mediaType">Media type.</param>
		/// <param name="name">Name.</param>
		/// <returns></returns>
		IEmaFluent AddAttachment ( Stream stream , string mediaType = "" , string name = "" );

		/// <summary>
		/// Priority.
		/// </summary>
		/// <param name="priority">Priority.</param>
		IEmaFluent Priority ( MailPriority priority );

		/// <summary>
		/// Sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		IEmaFluent Sender ( string sender );

		/// <summary>
		/// Alternate view.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="isHtml">Is html.</param>
		IEmaFluent AlternateView ( Action<IEmaFluentAlternateViewContext> context , bool isHtml );

		/// <summary>
		/// Send message.
		/// </summary>
		void Send ();

		/// <summary>
		/// Send asynchronized.
		/// </summary>
		/// <returns></returns>
		Task SendAsync ();

		/// <summary>
		/// Get mail message.
		/// </summary>
		/// <returns>Message <see cref="MailMessage"/>.</returns>
		MailMessage GetMailMessage ();

	}

}
