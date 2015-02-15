using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EmptyFlow.EmaFluent.Implementations.EmaFluentAlternateViewContexts;

namespace EmptyFlow.EmaFluent.Implementations.EmaFluents {

	/// <summary>
	/// EmaFluent basic implementation.
	/// </summary>
	public sealed class DefaultEmaFluent : IEmaFluent {

		private MailMessage m_MailMessage = new MailMessage ();

		private IEmaFluentFactory m_EmaFluentFactory;

		private IEmailSendingProvider m_EmailSendingProvider;

		/// <summary>
		/// Get not nullable strings.
		/// </summary>
		/// <param name="strings">Strings.</param>
		/// <returns>Strings sequence.</returns>
		private IEnumerable<string> GetNotNullableStrings ( IEnumerable<string> strings ) {
			return strings
				.Where ( a => a != null )
				.ToList ();
		}

		/// <summary>
		/// Constructor injection.
		/// </summary>
		/// <param name="emaFluentFactory">Email sending provider.</param>
		public DefaultEmaFluent ( IEmaFluentFactory emaFluentFactory ) {
			m_EmaFluentFactory = emaFluentFactory;
		}

		/// <summary>
		/// Set sending settings.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public IEmaFluent SetSendingSettings ( string connectionString ) {
			m_EmailSendingProvider = m_EmaFluentFactory.CreateEmailSendingProvider ();
			m_EmailSendingProvider.SetSettings ( connectionString );

			return this;
		}

		/// <summary>
		/// Set sending settings.
		/// </summary>
		/// <param name="emailSendingProvider">Email sending provider.</param>
		public IEmaFluent SetSendingSettings ( IEmailSendingProvider emailSendingProvider ) {
			m_EmailSendingProvider = emailSendingProvider;

			return this;
		}

		/// <summary>
		/// To.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		public IEmaFluent To ( string emailAddress ) {
			Contract.Requires ( emailAddress != null );
			if ( emailAddress == null ) throw new ArgumentNullException ( "emailAddress" );

			m_MailMessage.To.Add ( new MailAddress ( emailAddress ) );
			return this;
		}

		/// <summary>
		/// To.
		/// </summary>
		/// <param name="emailAddresses">Email addresses.</param>
		public IEmaFluent To ( IEnumerable<string> emailAddresses ) {
			Contract.Requires ( emailAddresses != null );
			if ( emailAddresses == null ) throw new ArgumentNullException ( "emailAddresses" );

			foreach ( var emailAddress in GetNotNullableStrings ( emailAddresses ) ) {
				m_MailMessage.To.Add ( new MailAddress ( emailAddress ) );
			}
			return this;
		}

		/// <summary>
		/// Bcc.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		public IEmaFluent Bcc ( string emailAddress ) {
			Contract.Requires ( emailAddress != null );
			if ( emailAddress == null ) throw new ArgumentNullException ( "emailAddress" );

			m_MailMessage.Bcc.Add ( new MailAddress ( emailAddress ) );
			return this;
		}

		/// <summary>
		/// Bcc.
		/// </summary>
		/// <param name="emailAddresses">Email addresses.</param>
		public IEmaFluent Bcc ( IEnumerable<string> emailAddresses ) {
			Contract.Requires ( emailAddresses != null );
			if ( emailAddresses == null ) throw new ArgumentNullException ( "emailAddresses" );

			foreach ( var emailAddress in GetNotNullableStrings ( emailAddresses ) ) {
				m_MailMessage.Bcc.Add ( new MailAddress ( emailAddress ) );
			}
			return this;
		}

		/// <summary>
		/// CC.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		/// <returns></returns>
		public IEmaFluent Cc ( string emailAddress ) {
			Contract.Requires ( emailAddress != null );
			if ( emailAddress == null ) throw new ArgumentNullException ( "emailAddress" );

			m_MailMessage.CC.Add ( new MailAddress ( emailAddress ) );
			return this;
		}

		/// <summary>
		/// CC.
		/// </summary>
		/// <param name="emailAddresses">Email addresses.</param>
		public IEmaFluent Cc ( IEnumerable<string> emailAddresses ) {
			Contract.Requires ( emailAddresses != null );
			if ( emailAddresses == null ) throw new ArgumentNullException ( "emailAddresses" );

			foreach ( var emailAddress in GetNotNullableStrings ( emailAddresses ) ) {
				m_MailMessage.CC.Add ( new MailAddress ( emailAddress ) );
			}
			return this;
		}

		/// <summary>
		/// Reply to.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		public IEmaFluent ReplyTo ( string emailAddress ) {
			Contract.Requires ( emailAddress != null );
			if ( emailAddress == null ) throw new ArgumentNullException ( "emailAddress" );

			m_MailMessage.ReplyToList.Add ( new MailAddress ( emailAddress ) );
			return this;
		}

		/// <summary>
		/// Reply to.
		/// </summary>
		/// <param name="emailAddresses">Email addresses.</param>
		public IEmaFluent ReplyTo ( IEnumerable<string> emailAddresses ) {
			Contract.Requires ( emailAddresses != null );
			if ( emailAddresses == null ) throw new ArgumentNullException ( "emailAddresses" );

			foreach ( var emailAddress in GetNotNullableStrings ( emailAddresses ) ) {
				m_MailMessage.ReplyToList.Add ( new MailAddress ( emailAddress ) );
			}
			return this;
		}

		/// <summary>
		/// Body encoding.
		/// </summary>
		/// <param name="encoding">Encoding.</param>
		public IEmaFluent BodyEncoding ( Encoding encoding ) {
			Contract.Requires ( encoding != null );
			if ( encoding == null ) throw new ArgumentNullException ( "encoding" );

			m_MailMessage.BodyEncoding = encoding;

			return this;
		}

		/// <summary>
		/// Subject encoding.
		/// </summary>
		/// <param name="encoding">Encoding.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluent SubjectEncoding ( Encoding encoding ) {
			Contract.Requires ( encoding != null );
			if ( encoding == null ) throw new ArgumentNullException ( "encoding" );

			m_MailMessage.SubjectEncoding = encoding;

			return this;
		}

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluent Subject ( string subject ) {
			Contract.Requires ( subject != null );
			if ( subject == null ) throw new ArgumentNullException ( "subject" );

			m_MailMessage.Subject = subject;

			return this;
		}

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		/// <param name="formatArgument">Format argument.</param>
		public IEmaFluent Subject ( string subject , IEnumerable<object> formatArgument ) {
			Contract.Requires ( subject != null );
			Contract.Requires ( formatArgument != null );
			if ( subject == null ) throw new ArgumentNullException ( "subject" );
			if ( formatArgument == null ) throw new ArgumentNullException ( "formatArgument" );

			m_MailMessage.Subject = string.Format ( subject , formatArgument.ToArray() );

			return this;
		}

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		/// <param name="encoding">Subject encoding.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluent Subject ( string subject , Encoding encoding ) {
			Contract.Requires ( subject != null );
			Contract.Requires ( encoding != null );
			if ( subject == null ) throw new ArgumentNullException ( "subject" );
			if ( encoding == null ) throw new ArgumentNullException ( "encoding" );

			m_MailMessage.Subject = subject;
			m_MailMessage.SubjectEncoding = encoding;

			return this;
		}

		/// <summary>
		/// Subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		/// <param name="formatArgument">Format arguments.</param>
		/// <param name="encoding">Subject encoding.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluent Subject ( string subject , IEnumerable<object> formatArgument , Encoding encoding ) {
			Contract.Requires ( subject != null );
			Contract.Requires ( encoding != null );
			Contract.Requires ( formatArgument != null );
			if ( subject == null ) throw new ArgumentNullException ( "subject" );
			if ( encoding == null ) throw new ArgumentNullException ( "encoding" );
			if ( formatArgument == null ) throw new ArgumentNullException ( "formatArgument" );

			m_MailMessage.Subject = string.Format ( subject , formatArgument.ToArray() );
			m_MailMessage.SubjectEncoding = encoding;

			return this;
		}

		/// <summary>
		/// Body.
		/// </summary>
		/// <param name="body">Body.</param>
		/// <param name="isHtml">Is html body.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluent Body ( string body , bool isHtml = false ) {
			Contract.Requires ( body != null );
			if ( body == null ) throw new ArgumentNullException ( "body" );

			m_MailMessage.IsBodyHtml = isHtml;
			m_MailMessage.Body = body;

			return this;
		}

		/// <summary>
		/// Body.
		/// </summary>
		/// <param name="renderEngine">Render engine.</param>
		/// <param name="model">Model.</param>
		/// <param name="templateName">Template name.</param>
		/// <param name="isHtml">Is html body.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluent Body<T> ( IRenderEngine renderEngine , string templateName , T model , bool isHtml = false ) {
			Contract.Requires ( renderEngine != null );
			Contract.Requires ( templateName != null );
			if ( renderEngine == null ) throw new ArgumentNullException ( "renderEngine" );
			if ( templateName == null ) throw new ArgumentNullException ( "templateName" );

			m_MailMessage.IsBodyHtml = isHtml;
			m_MailMessage.Body = renderEngine.GetBody<T> ( model , templateName );

			return this;
		}

		/// <summary>
		/// Add attachment.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="mediaType"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public IEmaFluent AddAttachment ( Stream stream , string mediaType = "" , string name = "" ) {
			Contract.Requires ( mediaType != null );
			Contract.Requires ( name != null );
			if ( mediaType == null ) throw new ArgumentNullException ( "mediaType" );
			if ( name == null ) throw new ArgumentNullException ( "name" );

			m_MailMessage.Attachments.Add ( new Attachment ( stream , name , mediaType ) );

			return this;
		}

		/// <summary>
		/// Priority.
		/// </summary>
		/// <param name="priority">Priority.</param>
		public IEmaFluent Priority ( MailPriority priority ) {
			m_MailMessage.Priority = priority;

			return this;
		}

		/// <summary>
		/// Sender.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public IEmaFluent Sender ( string sender ) {
			Contract.Requires ( sender != null );
			if ( sender == null ) throw new ArgumentNullException ( "sender" );

			m_MailMessage.Sender = new MailAddress ( sender );

			return this;
		}

		/// <summary>
		/// Send mail.
		/// </summary>
		public void Send () {
			m_EmailSendingProvider.Send ( this );
		}

		/// <summary>
		/// Send async.
		/// </summary>
		public async Task SendAsync () {
			await m_EmailSendingProvider.SendAsync ( this );
		}

		/// <summary>
		/// Alternate view.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="isHtml">Is html.</param>
		public IEmaFluent AlternateView ( Action<IEmaFluentAlternateViewContext> context , bool isHtml ) {
			Contract.Requires ( context != null );
			if ( context == null ) throw new ArgumentNullException ( "context" );

			m_MailMessage.IsBodyHtml = isHtml;

			var contextInstance = m_EmaFluentFactory.CreateEmaFluentAlternateViewContext ();
			context ( contextInstance );
			m_MailMessage.AlternateViews.Add ( contextInstance.GetAlternateView () );

			return this;
		}

		/// <summary>
		/// Get mail message.
		/// </summary>
		public MailMessage GetMailMessage () {
			return m_MailMessage;
		}

		/// <summary>
		/// From.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		public IEmaFluent From ( string emailAddress ) {
			m_MailMessage.From = new MailAddress ( emailAddress );

			return this;
		}

		/// <summary>
		/// Set sending settings.
		/// </summary>
		/// <param name="action">Action.</param>
		public IEmaFluent SetSendingSettings ( Action<IEmailSendingProvider> action ) {
			m_EmailSendingProvider = m_EmaFluentFactory.CreateEmailSendingProvider ();

			action ( m_EmailSendingProvider );

			return this;
		}

		/// <summary>
		/// Dispose.
		/// </summary>
		public void Dispose () {
			m_EmailSendingProvider.Dispose ();
			m_MailMessage.Dispose ();
		}

	}

}
