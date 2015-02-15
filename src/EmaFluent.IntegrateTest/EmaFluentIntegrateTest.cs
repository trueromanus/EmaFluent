using System.Net.Mime;
using System.Text;
using EmptyFlow.EmaFluent.Implementations.EmaFluentFactories;
using EmptyFlow.EmaFluent.Implementations.EmaFluents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmaFluent.IntegrateTest {

	/// <summary>
	/// Ema fluent integrate test.
	/// </summary>
	[TestClass]
	public class EmaFluentIntegrateTest {

		[TestMethod]
		public void Send_Test () {
			new DefaultEmaFluent ( new EmaFluentFactory () )
				.SetSendingSettings (
					( provider ) => provider
						.EnableSSL()
						.Host ( "smtp.mail.ru" )
						.Port ( 25 )
						.SetNetworkCredential ( "testor64@mail.ru" , "" )
				)
				.From ( "testor64@mail.ru" )
				.To ( "testor64@mail.ru" )
				.Subject ( "Hi guys!" )
				.Body ( "Hello my friends." )
				.Send ();
		}

		[TestMethod]
		public void Send_AlternateView_Test () {
			new DefaultEmaFluent ( new EmaFluentFactory () )
				.SetSendingSettings (
					( provider ) => provider
						.EnableSSL ()
						.Host ( "smtp.mail.ru" )
						.Port ( 25 )
						.SetNetworkCredential ( "testor64@mail.ru" , "" )
				)
				.From ( "testor64@mail.ru" )
				.To ( "testor64@mail.ru" )
				.Subject ( "Hi guys!" )
				.AlternateView (
					context =>
						context
							.Create ( "<html><body><img src=\"cid:companylogo\"/><br></body></html>" , MediaTypeNames.Text.Html , Encoding.UTF8 )
							.LinkedResource ( @"e:\pac.png" , mediaType: "image/png" , contentId: "companylogo" )
					,
					isHtml:true
				)
				.Send ();
		}

	}

}
