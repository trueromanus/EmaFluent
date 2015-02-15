using System;
using EmptyFlow.EmaFluent.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmaFluentUnitTest {
	[TestClass]
	public class EmailSettingsDeserializerUnitTest {

		/// <summary>
		/// Deserialize enable ssl.
		/// </summary>
		[TestMethod]
		public void Deserialize_EnableSSL () {
			var model = EmailSettingsDeserializer.Deserialize ( "EnableSSL=true" );

			Assert.IsTrue ( model.EnableSSL );
		}

		/// <summary>
		/// Deserialize use default credentials.
		/// </summary>
		[TestMethod]
		public void Deserialize_UseDefaultCredentials () {
			var model = EmailSettingsDeserializer.Deserialize ( "UseDefaultCredentials=true" );

			Assert.IsTrue ( model.UseDefaultCredentials );
		}

		/// <summary>
		/// Deserialize timeout.
		/// </summary>
		[TestMethod]
		public void Deserialize_Timeout () {
			var model = EmailSettingsDeserializer.Deserialize ( "Timeout=100" );

			Assert.AreEqual ( model.Timeout , 100 );
		}

		/// <summary>
		/// Deserialize port.
		/// </summary>
		[TestMethod]
		public void Deserialize_Port () {
			var model = EmailSettingsDeserializer.Deserialize ( "Port=25" );

			Assert.AreEqual ( model.Port , 25 );
		}

		/// <summary>
		/// Deserialize user login.
		/// </summary>
		[TestMethod]
		public void Deserialize_UserLogin () {
			var model = EmailSettingsDeserializer.Deserialize ( "UserLogin=romanus" );

			Assert.AreEqual ( model.UserLogin , "romanus" );
		}

		/// <summary>
		/// Deserialize user password.
		/// </summary>
		[TestMethod]
		public void Deserialize_UserPassword () {
			var model = EmailSettingsDeserializer.Deserialize ( "UserPassword=12345" );

			Assert.AreEqual ( model.UserPassword , "12345" );
		}

		/// <summary>
		/// Deserialize host.
		/// </summary>
		[TestMethod]
		public void Deserialize_Host () {
			var model = EmailSettingsDeserializer.Deserialize ( "Host=blabla.com" );

			Assert.AreEqual ( model.Host , "blabla.com" );
		}

		/// <summary>
		/// Deserialize all model.
		/// </summary>
		[TestMethod]
		public void Deserialize_AllModel () {
			var model = EmailSettingsDeserializer.Deserialize ( "EnableSSL=true;UseDefaultCredentials=true;Host=blabla.com;Timeout=100;Port=25;UserLogin=romanus;UserPassword=12345" );

			Assert.IsTrue ( model.EnableSSL );
			Assert.IsTrue ( model.UseDefaultCredentials );
			Assert.AreEqual ( model.Timeout , 100 );
			Assert.AreEqual ( model.Port , 25 );
			Assert.AreEqual ( model.UserLogin , "romanus" );
			Assert.AreEqual ( model.UserPassword , "12345" );
			Assert.AreEqual ( model.Host , "blabla.com" );
		}

		/// <summary>
		/// Deserialize empty connection string.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Deserialize_EmptyConnectionString () {
			EmailSettingsDeserializer.Deserialize ( null );
		}

	}

}
