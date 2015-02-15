using EmptyFlow.EmaFluent.Implementations.EmaFluentAlternateViewContexts;
using EmptyFlow.EmaFluent.Implementations.EmailSendingProviders;

namespace EmptyFlow.EmaFluent.Implementations.EmaFluentFactories {
	
	/// <summary>
	/// EmaFluent Factory.
	/// </summary>
	public class EmaFluentFactory : IEmaFluentFactory {

		/// <summary>
		/// Create email sending provider.
		/// </summary>
		/// <returns>Instance <see cref="IEmailSendingProvider"/>.</returns>
		public IEmailSendingProvider CreateEmailSendingProvider () {
			return new EmailSendingProvider ();
		}

		/// <summary>
		/// Create alternate view context.
		/// </summary>
		/// <returns>Instance <see cref="IEmaFluentAlternateViewContext"/>.</returns>
		public IEmaFluentAlternateViewContext CreateEmaFluentAlternateViewContext () {
			return new EmaFluentAlternateViewContext ();
		}

	}

}
