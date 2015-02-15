using System;
using System.Collections.Generic;
using EmptyFlow.EmaFluent;
using EmptyFlow.EmaFluent.Implementations.EmaFluents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;

namespace EmaFluentUnitTest {

	/// <summary>
	/// EmaFluent unit test.
	/// </summary>
	[TestClass]
	public class EmaFluentUnitTest {

		private class Wrapper {

			/// <summary>
			/// Stub factory.
			/// </summary>
			public Mock<IEmaFluentFactory> StubFactory {
				get;
				set;
			}

			/// <summary>
			/// EmaFluent.
			/// </summary>
			public IEmaFluent EmaFluent {
				get;
				set;
			}

		}

		private Wrapper CreateWrapper () {
			var stubFactory = new Mock<IEmaFluentFactory> ();

			return new Wrapper {
				StubFactory = stubFactory ,
				EmaFluent = new DefaultEmaFluent ( stubFactory.Object )
			};
		}

		/// <summary>
		/// To single address.
		/// </summary>
		[TestMethod]
		public void To_SingleAddress () {
			var address = "test@test.com";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.To ( address );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.To[0].Address , address );
		}

		/// <summary>
		/// To multiple addresses.
		/// </summary>
		[TestMethod]
		public void To_MultipleAddress () {
			var addresses = new List<string> {
				"test@test.com",
				"test1@test.com"
			};

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.To ( addresses );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.To.Count , 2 );
			Assert.AreEqual ( message.To[0].Address , addresses.First () );
			Assert.AreEqual ( message.To[1].Address , addresses.Last () );
		}

		/// <summary>
		/// To single throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void To_SingleThrowArgument () {
			var wrapper = CreateWrapper ();
			string address = null;
			wrapper.EmaFluent.To ( address );
		}

		/// <summary>
		/// To single throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void To_MultipleThrowArgument () {
			var wrapper = CreateWrapper ();
			List<string> addresses = null;
			wrapper.EmaFluent.To ( addresses );
		}

		/// <summary>
		/// From check result.
		/// </summary>
		[TestMethod]
		public void From_CheckResult () {
			var address = "batman@gotham.com";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.From ( address );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.From.Address , address );
		}

		/// <summary>
		/// From throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void From_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.From ( null );
		}

		/// <summary>
		/// Bcc single address.
		/// </summary>
		[TestMethod]
		public void Bcc_SingleAddress () {
			var address = "robin@gotham.com";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Bcc ( address );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Bcc[0].Address , address );
		}

		/// <summary>
		/// Bcc multiple addresses.
		/// </summary>
		[TestMethod]
		public void Bcc_MultipleAddress () {
			var addresses = new List<string> {
				"robin@gotham.com",
				"batgirl@gotham.com"
			};

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Bcc ( addresses );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Bcc.Count , 2 );
			Assert.AreEqual ( message.Bcc[0].Address , addresses.First () );
			Assert.AreEqual ( message.Bcc[1].Address , addresses.Last () );
		}

		/// <summary>
		/// Bcc single throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Bcc_SingleThrow () {
			var wrapper = CreateWrapper ();
			string address = null;
			wrapper.EmaFluent.Bcc ( address );
		}

		/// <summary>
		/// Bcc multiple throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Bcc_MultipleThrow () {
			var wrapper = CreateWrapper ();
			IEnumerable<string> addresses = null;
			wrapper.EmaFluent.Bcc ( addresses );
		}

		/// <summary>
		/// CC single address.
		/// </summary>
		[TestMethod]
		public void CC_SingleAddress () {
			var address = "cj@wolfenstein.net";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Cc ( address );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.CC[0].Address , address );
		}

		/// <summary>
		/// CC multiple addresses.
		/// </summary>
		[TestMethod]
		public void CC_MultipleAddress () {
			var addresses = new List<string> {
				"cj@wolfenstein.net",
				"anna@wolfenstein.net"
			};

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Cc ( addresses );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.CC.Count , 2 );
			Assert.AreEqual ( message.CC[0].Address , addresses.First () );
			Assert.AreEqual ( message.CC[1].Address , addresses.Last () );
		}

		/// <summary>
		/// CC single throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void CC_SingleThrow () {
			var wrapper = CreateWrapper ();
			string address = null;
			wrapper.EmaFluent.Cc ( address );
		}

		/// <summary>
		/// CC multiple throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void CC_MultipleThrow () {
			var wrapper = CreateWrapper ();
			IEnumerable<string> addresses = null;
			wrapper.EmaFluent.Cc ( addresses );
		}

		/// <summary>
		/// ReplyTo single address.
		/// </summary>
		[TestMethod]
		public void ReplyTo_SingleAddress () {
			var address = "klarkkent@metropolis.com";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.ReplyTo ( address );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.ReplyToList[0].Address , address );
		}

		/// <summary>
		/// ReplyTo multiple addresses.
		/// </summary>
		[TestMethod]
		public void ReplyTo_MultipleAddress () {
			var addresses = new List<string> {
				"klarkkent@metropolis.com",
				"lanalang@metropolis.com"
			};

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.ReplyTo ( addresses );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.ReplyToList.Count , 2 );
			Assert.AreEqual ( message.ReplyToList[0].Address , addresses.First () );
			Assert.AreEqual ( message.ReplyToList[1].Address , addresses.Last () );
		}

		/// <summary>
		/// ReplyTo single throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void ReplyTo_SingleThrow () {
			var wrapper = CreateWrapper ();
			string address = null;
			wrapper.EmaFluent.ReplyTo ( address );
		}

		/// <summary>
		/// ReplyTo multiple throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void ReplyTo_MultipleThrow () {
			var wrapper = CreateWrapper ();
			IEnumerable<string> addresses = null;
			wrapper.EmaFluent.ReplyTo ( addresses );
		}

		/// <summary>
		/// BodyEncoding check result.
		/// </summary>
		[TestMethod]
		public void BodyEncoding_CheckResult () {
			var encoding = Encoding.UTF8;

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.BodyEncoding ( encoding );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.BodyEncoding , encoding );
		}

		/// <summary>
		/// BodyEncoding throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void BodyEncoding_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.BodyEncoding ( null );
		}

		/// <summary>
		/// SubjectEncoding check result.
		/// </summary>
		[TestMethod]
		public void SubjectEncoding_CheckResult () {
			var encoding = Encoding.UTF8;

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.SubjectEncoding ( encoding );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.SubjectEncoding , encoding );
		}

		/// <summary>
		/// SubjectEncoding throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void SubjectEncoding_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.SubjectEncoding ( null );
		}

		/// <summary>
		/// Subject check result.
		/// </summary>
		[TestMethod]
		public void Subject_CheckResult () {
			var subject = "Hi guys!";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( subject );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Subject , subject );
		}

		/// <summary>
		/// Subject format argument check result.
		/// </summary>
		[TestMethod]
		public void Subject_FormatArgument_CheckResult () {
			var subject = "Hi {0} and {1}!";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( subject , new List<string> { "Shine" , "Susanne" } );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Subject , "Hi Shine and Susanne!" );
		}

		/// <summary>
		/// Subject format argument throw subject argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_FormatArgument_ThrowSubjectArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( null , new List<string> { "Shine" , "Susanne" } );
		}

		/// <summary>
		/// Subject format argument throw format argument argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_FormatArgument_ThrowFormatArgumentArgument () {
			var wrapper = CreateWrapper ();
			List<string> emptyArgument = null;
			wrapper.EmaFluent.Subject ( "Throw test" , emptyArgument );
		}

		/// <summary>
		/// Subject encoding check result.
		/// </summary>
		[TestMethod]
		public void Subject_Encoding_CheckResult () {
			var subject = "Hi Guys!";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( subject , Encoding.ASCII );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Subject , subject );
			Assert.AreEqual ( message.SubjectEncoding , Encoding.ASCII );
		}

		/// <summary>
		/// Subject encoding throw subject.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_Encoding_ThrowSubject () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( null , Encoding.ASCII );
		}

		/// <summary>
		/// Subject encoding throw encoding.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_Encoding_ThrowEncoding () {
			var wrapper = CreateWrapper ();
			Encoding emptyEncoding = null;
			wrapper.EmaFluent.Subject ( "Throw test" , emptyEncoding );
		}

		/// <summary>
		/// Subject encoding and format argument check result.
		/// </summary>
		[TestMethod]
		public void Subject_Encoding_FormatArgument_CheckResult () {
			var subject = "Hi {0} and {1}!";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( subject , new List<string> { "Shine" , "Susanne" } , Encoding.ASCII );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Subject , "Hi Shine and Susanne!" );
			Assert.AreEqual ( message.SubjectEncoding , Encoding.ASCII );
		}

		/// <summary>
		/// Subject encoding and format argument subject throw.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_Encoding_FormatArgument_SubjectThrow () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( null , new List<string> () , Encoding.ASCII );
		}

		/// <summary>
		/// Subject encoding and format argument throw.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_Encoding_FormatArgument_FormatArgumentThrow () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( "Throw test" , null , Encoding.ASCII );
		}

		/// <summary>
		/// Subject encoding and format argument throw.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_Encoding_FormatArgument_EncodingThrow () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( "Throw test" , new List<string> () , null );
		}

		/// <summary>
		/// Subject throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Subject_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Subject ( null );
		}

		/// <summary>
		/// Body check result.
		/// </summary>
		[TestMethod]
		public void Body_CheckResult () {
			var body = "Batman is back!";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Body ( body );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Body , body );
		}

		/// <summary>
		/// Body check result is html.
		/// </summary>
		[TestMethod]
		public void Body_CheckResultIsHtml () {
			var body = "Batman is back!";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Body ( body , isHtml: true );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Body , body );
			Assert.AreEqual ( message.IsBodyHtml , true );
		}

		/// <summary>
		/// Body check result is html.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Body_Throw () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Body ( null );
		}

		/// <summary>
		/// Body template check result.
		/// </summary>
		[TestMethod]
		public void BodyTemplate_CheckResult () {
			var model = 1;
			var result = "Superman always here!";
			var templateName = "template";

			var renderingEngine = new Mock<IRenderEngine> ();
			renderingEngine
				.Setup ( a => a.GetBody<int> ( model , templateName ) )
				.Returns ( result );

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Body<int> ( renderingEngine.Object , templateName , model , isHtml: true );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Body , result );
			Assert.AreEqual ( message.IsBodyHtml , true );
		}

		/// <summary>
		/// Body template throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void BodyTemplate_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Body<object> ( null , "Template" , null , isHtml: true );
		}

		/// <summary>
		/// Body template throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void BodyTemplate_ThrowTemplateArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Body<int> ( new Mock<IRenderEngine>().Object , null , 1 , isHtml: true );
		}

		/// <summary>
		/// Add attachment check result.
		/// </summary>
		[TestMethod]
		public void AddAttachment_CheckResult () {
			var mediaType = "text/html";
			var name = "attachment.png";
			var stream = new MemoryStream ();

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.AddAttachment ( stream , mediaType , name );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Attachments.Count , 1 );
			Assert.AreEqual ( message.Attachments[0].Name , name );
			Assert.AreEqual ( message.Attachments[0].ContentType.MediaType , mediaType );
			Assert.AreEqual ( message.Attachments[0].ContentStream , stream );
		}

		/// <summary>
		/// Priority check result.
		/// </summary>
		[TestMethod]
		public void Priority_CheckResult () {
			var priority = MailPriority.High;

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Priority ( priority );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Priority , priority );
		}

		/// <summary>
		/// Sender check result.
		/// </summary>
		[TestMethod]
		public void Sender_CheckResult () {
			var sender = "thing@swamp.biz";

			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Sender ( sender );

			var message = wrapper.EmaFluent.GetMailMessage ();

			Assert.AreEqual ( message.Sender , sender );
		}

		/// <summary>
		/// Sender throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Sender_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.Sender ( null );
		}

		/// <summary>
		/// Send settings in parameter check result.
		/// </summary>
		[TestMethod]
		public void Send_SettingsInParameter_CheckResult () {
			var mockEmailSendingProvider = new Mock<IEmailSendingProvider> ();
			var wrapper = CreateWrapper ();
			wrapper.EmaFluent.SetSendingSettings ( mockEmailSendingProvider.Object );
			wrapper.EmaFluent.Send ();

			mockEmailSendingProvider.Verify ( a => a.Send ( wrapper.EmaFluent ) );
		}

		/// <summary>
		/// Send settings in parameter check result.
		/// </summary>
		[TestMethod]
		public void SetSendingSettings_ConnectionString () {
			var mockEmailSendingProvider = new Mock<IEmailSendingProvider> ();
			var connectionString = "";
			var wrapper = CreateWrapper ();
			wrapper.StubFactory
				.Setup ( a => a.CreateEmailSendingProvider () )
				.Returns ( mockEmailSendingProvider.Object );

			wrapper.EmaFluent.SetSendingSettings ( connectionString );

			mockEmailSendingProvider.Verify ( a => a.SetSettings ( connectionString ) );
		}

		/// <summary>
		/// Send settings callback.
		/// </summary>
		[TestMethod]
		public void SetSendingSettings_Callback () {
			var mockEmailSendingProvider = new Mock<IEmailSendingProvider> ();
			var connectionString = "";
			var wrapper = CreateWrapper ();
			wrapper.StubFactory
				.Setup ( a => a.CreateEmailSendingProvider () )
				.Returns ( mockEmailSendingProvider.Object );

			wrapper.EmaFluent.SetSendingSettings ( connectionString );

			mockEmailSendingProvider.Verify ( a => a.SetSettings ( connectionString ) );
		}

		/// <summary>
		/// Alternate view check result.
		/// </summary>
		[TestMethod]
		public void AlternateView_CheckResult () {
			var checkResult = false;
			var isHtml = true;

			var mockAlternateView = new Mock<IEmaFluentAlternateViewContext> ();

			var wrapper = CreateWrapper ();
			wrapper.StubFactory
				.Setup ( a => a.CreateEmaFluentAlternateViewContext () )
				.Returns ( mockAlternateView.Object );
			AlternateView alternateView = new AlternateView ( new MemoryStream () , "application/json" );
			mockAlternateView
				.Setup ( a => a.GetAlternateView () )
				.Returns ( alternateView );

			wrapper.EmaFluent.AlternateView (
				( context ) => {
					checkResult = true;
				} ,
				isHtml
			);

			Assert.IsTrue ( checkResult );
		}

		/// <summary>
		/// Alternate view throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void AlternateView_ThrowArgument () {
			CreateWrapper ().EmaFluent.AlternateView ( null , false );
		}

	}
}
