
namespace EmptyFlow.EmaFluent {
	
	/// <summary>
	/// EmaFluent factory.
	/// </summary>
	public interface IEmaFluentFactory {

		/// <summary>
		/// Create instance <see cref="IEmailSendingProvider"/>.
		/// </summary>
		IEmailSendingProvider CreateEmailSendingProvider ();

		/// <summary>
		/// Create instance <see cref="IEmaFluentAlternateViewContext"/>.
		/// </summary>
		IEmaFluentAlternateViewContext CreateEmaFluentAlternateViewContext ();

	}

}
