using System;
using EmptyFlow.EmaFluent.Implementations.EmailSendingProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmaFluentUnitTest {

	/// <summary>
	/// Email sending provider unit test.
	/// </summary>
	[TestClass]
	public class EmailSendingProviderUnitTest {

		private class Wrapper {

			public EmailSendingProvider Provider {
				get;
				set;
			}

		}

		private Wrapper CreateWrapper () {
			return new Wrapper {
				Provider = new EmailSendingProvider ()
			};
		}

		/// <summary>
		/// Disable SSL check result.
		/// </summary>
		[TestMethod]
		public void DisableSSL_CheckResult () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.DisableSSL ();

			Assert.IsFalse ( wrapper.Provider.GetSmtpClient ().EnableSsl );
		}

		/// <summary>
		/// Enable SSL check result.
		/// </summary>
		[TestMethod]
		public void EnableSSL_CheckResult () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.EnableSSL ();

			Assert.IsTrue ( wrapper.Provider.GetSmtpClient ().EnableSsl );
		}

		/// <summary>
		/// Host check result.
		/// </summary>
		[TestMethod]
		public void Host_CheckResult () {
			var host = "metropolis.com";

			var wrapper = CreateWrapper ();
			wrapper.Provider.Host ( host );

			Assert.AreEqual ( wrapper.Provider.GetSmtpClient ().Host , host );
		}

		/// <summary>
		/// Host exception.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Host_ThrowException () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.Host ( null );
		}

		/// <summary>
		/// Port check result.
		/// </summary>
		[TestMethod]
		public void Port_CheckResult () {
			var port = 26;

			var wrapper = CreateWrapper ();
			wrapper.Provider.Port ( port );

			Assert.AreEqual ( wrapper.Provider.GetSmtpClient ().Port , port );
		}

		/// <summary>
		/// Port less exception.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentOutOfRangeException ) )]
		public void Port_ThrowLess () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.Port ( 0 );
		}

		/// <summary>
		/// Port more exception.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentOutOfRangeException ) )]
		public void Port_ThrowMore () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.Port ( 65536 );
		}

		/// <summary>
		/// Timeout check result.
		/// </summary>
		[TestMethod]
		public void Timeout_CheckResult () {
			var timeout = 100;

			var wrapper = CreateWrapper ();
			wrapper.Provider.Timeout ( timeout );

			Assert.AreEqual ( wrapper.Provider.GetSmtpClient ().Timeout , timeout );
		}

		/// <summary>
		/// Timeout throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentOutOfRangeException ) )]
		public void Timeout_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.Timeout ( -1 );
		}

		/// <summary>
		/// Set network check result.
		/// </summary>
		[TestMethod]
		public void SetNetwork_CheckResult () {
			var login = "sandman";
			var password = "dream";

			var wrapper = CreateWrapper ();
			wrapper.Provider.SetNetworkCredential ( login , password );

			//TODO:why check credentials???

			Assert.AreEqual ( wrapper.Provider.GetSmtpClient ().UseDefaultCredentials , false );
		}

		/// <summary>
		/// Set network credentials throw login.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SetNetworkCredentials_ThrowLogin () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.SetNetworkCredential ( null , "" );
		}

		/// <summary>
		/// Set network credentials throw password.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void SetNetworkCredentials_ThrowPassword () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.SetNetworkCredential ( "" , null );
		}

		/// <summary>
		/// Use default credentials check result.
		/// </summary>
		[TestMethod]
		public void UseDefaultCredentials_CheckResult () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.UseDefaultCredentials ();

			Assert.IsTrue ( wrapper.Provider.GetSmtpClient ().UseDefaultCredentials );
		}

		/// <summary>
		/// Set settings check result.
		/// </summary>
		[TestMethod]
		public void SetSettings_CheckResult (){
			var wrapper = CreateWrapper ();
			wrapper.Provider.SetSettings ( "EnableSSL=true;UseDefaultCredentials=true;Host=blabla.com;Timeout=100;Port=25;UserLogin=romanus;UserPassword=12345" );
			var smtpClient = wrapper.Provider.GetSmtpClient();

			Assert.IsTrue ( smtpClient.EnableSsl );
			Assert.IsFalse ( smtpClient.UseDefaultCredentials );
			Assert.AreEqual ( smtpClient.Timeout , 100 );
			Assert.AreEqual ( smtpClient.Port , 25 );
			Assert.AreEqual ( smtpClient.Host , "blabla.com" );
		}

		/// <summary>
		/// Set settings check result default credentials.
		/// </summary>
		[TestMethod]
		public void SetSettings_CheckResult_DefaultCredentials () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.SetSettings ( "EnableSSL=true;UseDefaultCredentials=true;Host=blabla.com;Timeout=100;Port=25" );
			var smtpClient = wrapper.Provider.GetSmtpClient ();

			Assert.IsTrue ( smtpClient.EnableSsl );
			Assert.IsTrue ( smtpClient.UseDefaultCredentials );
			Assert.AreEqual ( smtpClient.Timeout , 100 );
			Assert.AreEqual ( smtpClient.Port , 25 );
			Assert.AreEqual ( smtpClient.Host , "blabla.com" );
		}

		/// <summary>
		/// Set settings check result port empty.
		/// </summary>
		[TestMethod]
		public void SetSettings_CheckResult_PortEmpty () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.SetSettings ( "EnableSSL=true;UseDefaultCredentials=true;Host=blabla.com;Timeout=100" );
			var smtpClient = wrapper.Provider.GetSmtpClient ();

			Assert.IsTrue ( smtpClient.EnableSsl );
			Assert.IsTrue ( smtpClient.UseDefaultCredentials );
			Assert.AreEqual ( smtpClient.Timeout , 100 );
			Assert.AreEqual ( smtpClient.Port , 25 );
			Assert.AreEqual ( smtpClient.Host , "blabla.com" );
		}

		/// <summary>
		/// Set settings throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void SetSettings_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.SetSettings ( null );
		}

		/// <summary>
		/// Send throw argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Send_ThrowArgument () {
			var wrapper = CreateWrapper ();
			wrapper.Provider.Send ( null );
		}

	}
}
