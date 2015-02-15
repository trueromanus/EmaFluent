using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmptyFlow.EmaFluent {

	/// <summary>
	/// Email sending provider.
	/// </summary>
	public interface IEmailSendingProvider : IDisposable {

		/// <summary>
		/// Set settings.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>Setup provider.</returns>
		IEmailSendingProvider SetSettings ( string connectionString );

		/// <summary>
		/// Enable SSL.
		/// </summary>
		/// <returns>Setup provider.</returns>
		IEmailSendingProvider EnableSSL ();

		/// <summary>
		/// Disable SSL.
		/// </summary>
		/// <returns>Setup provider.</returns>
		IEmailSendingProvider DisableSSL ();

		/// <summary>
		/// Host.
		/// </summary>
		/// <param name="host">Host.</param>
		/// <returns>Setup provider.</returns>
		IEmailSendingProvider Host ( string host );

		/// <summary>
		/// Port.
		/// </summary>
		/// <param name="port">Port.</param>
		/// <returns>Setup provider.</returns>
		IEmailSendingProvider Port ( int port );

		/// <summary>
		/// Timeout.
		/// </summary>
		/// <param name="timeout">Timeout.</param>
		/// <returns>Setup provider.</returns>
		IEmailSendingProvider Timeout ( int timeout );

		/// <summary>
		/// Set network credentials.
		/// </summary>
		/// <param name="login">Login.</param>
		/// <param name="password">Password.</param>
		/// <returns>Setup provider.</returns>
		IEmailSendingProvider SetNetworkCredential ( string login , string password );

		/// <summary>
		/// Use default credentials.
		/// </summary>
		IEmailSendingProvider UseDefaultCredentials ();

		/// <summary>
		/// Send.
		/// </summary>
		/// <param name="emaFluent">Emafluent.</param>
		void Send ( IEmaFluent emaFluent );

		/// <summary>
		/// Send asynchronized.
		/// </summary>
		/// <param name="emaFluent">Emafluent.</param>
		Task SendAsync ( IEmaFluent emaFluent );

		/// <summary>
		/// Get instance <see cref="SmtpClient"/>.
		/// </summary>
		SmtpClient GetSmtpClient ();

	}

}
