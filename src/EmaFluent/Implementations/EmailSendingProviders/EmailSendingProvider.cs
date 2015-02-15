using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmptyFlow.EmaFluent.Serializers;

namespace EmptyFlow.EmaFluent.Implementations.EmailSendingProviders {

	/// <summary>
	/// Email sending provider.
	/// </summary>
	public sealed class EmailSendingProvider : IEmailSendingProvider {

		private SmtpClient m_SmtpClient = new SmtpClient ();

		/// <summary>
		/// Set settings.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public IEmailSendingProvider SetSettings ( string connectionString ) {
			Contract.Requires ( connectionString != null );
			if ( connectionString == null ) throw new ArgumentNullException ( "connectionString" );
			
			var model = EmailSettingsDeserializer.Deserialize ( connectionString );

			m_SmtpClient.EnableSsl = model.EnableSSL;
			m_SmtpClient.Host = model.Host;
			m_SmtpClient.Port = model.Port == 0 ? 25 : model.Port;
			m_SmtpClient.Timeout = model.Timeout;
			m_SmtpClient.UseDefaultCredentials = model.UseDefaultCredentials;
			if (!string.IsNullOrEmpty(model.UserLogin) && !string.IsNullOrEmpty(model.UserPassword)){
				m_SmtpClient.Credentials = new NetworkCredential ( model.UserLogin , model.UserPassword );
			}

			return this;
		}

		/// <summary>
		/// Enable SSL.
		/// </summary>
		public IEmailSendingProvider EnableSSL () {
			m_SmtpClient.EnableSsl = true;
			return this;
		}

		/// <summary>
		/// Disable SSL.
		/// </summary>
		public IEmailSendingProvider DisableSSL () {
			m_SmtpClient.EnableSsl = false;
			return this;
		}

		/// <summary>
		/// Host.
		/// </summary>
		/// <param name="host">Host.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmailSendingProvider Host ( string host ) {
			Contract.Requires ( host != null );
			if ( host == null ) throw new ArgumentNullException ( "host" );

			m_SmtpClient.Host = host;
			return this;
		}

		/// <summary>
		/// Port.
		/// </summary>
		/// <param name="port">Port.</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public IEmailSendingProvider Port ( int port ) {
			Contract.Requires ( port > 0 );
			Contract.Requires ( port <= 65535 );
			if ( port < 1 ) throw new ArgumentOutOfRangeException ( "port" );
			if ( port > 65535 ) throw new ArgumentOutOfRangeException ( "port" );

			m_SmtpClient.Port = port;
			return this;
		}

		/// <summary>
		/// Timeout.
		/// </summary>
		/// <param name="timeout">Timeout.</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public IEmailSendingProvider Timeout ( int timeout ) {
			Contract.Requires ( timeout > -1 );
			if ( timeout < 0 ) throw new ArgumentOutOfRangeException ( "timeout" );

			m_SmtpClient.Timeout = timeout;
			return this;
		}

		/// <summary>
		/// Set network credentials.
		/// </summary>
		/// <param name="login">Login.</param>
		/// <param name="password">Password.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmailSendingProvider SetNetworkCredential ( string login , string password ) {
			Contract.Requires ( login != null );
			Contract.Requires ( password != null );
			if ( login == null ) throw new ArgumentNullException ( "login" );
			if ( password == null ) throw new ArgumentNullException ( "password" );

			m_SmtpClient.Credentials = new NetworkCredential ( login , password );
			return this;
		}

		/// <summary>
		/// Use default credentials.
		/// </summary>
		public IEmailSendingProvider UseDefaultCredentials () {
			m_SmtpClient.UseDefaultCredentials = true;
			return this;
		}

		/// <summary>
		/// Send mail.
		/// </summary>
		/// <param name="emaFluent">EmaFluent.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public void Send ( IEmaFluent emaFluent ) {
			Contract.Requires ( emaFluent != null );
			if ( emaFluent == null ) throw new ArgumentNullException ( "emaFluent" );

			m_SmtpClient.Send ( emaFluent.GetMailMessage () );
		}

		/// <summary>
		/// Send asynchronized.
		/// </summary>
		/// <param name="emaFluent">EmaFluent.</param>
		public Task SendAsync ( IEmaFluent emaFluent ) {
			Contract.Requires ( emaFluent != null );
			if ( emaFluent == null ) throw new ArgumentNullException ( "emaFluent" );

			var tcs = new TaskCompletionSource<bool> ();

			m_SmtpClient.SendCompleted +=
				( sender , e ) => {
					if ( e.Error != null ) {
						tcs.SetException ( e.Error );
						return;
					}

					tcs.SetResult ( true );
				};
			m_SmtpClient.SendAsync ( emaFluent.GetMailMessage () , null );

			return tcs.Task;
		}

		/// <summary>
		/// Get smtp client.
		/// </summary>
		public SmtpClient GetSmtpClient () {
			return m_SmtpClient;
		}

		/// <summary>
		/// Dispose.
		/// </summary>
		public void Dispose () {
			m_SmtpClient.Dispose ();
		}

	}

}
