using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using EmptyFlow.EmaFluent.Models;

namespace EmptyFlow.EmaFluent.Serializers {

	/// <summary>
	/// Email settings serializer.
	/// </summary>
	public static class EmailSettingsDeserializer {

		private const char KeyAndValueSeparator = '=';
		
		private const char OptionsSeparator = ';';

		private const string TrueValue = "true";

		/// <summary>
		/// Option enable SSL.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="value">Value.</param>
		private static void EnableSSL ( EmailSettingsModel model , string value ) {
			Contract.Requires ( model != null );
			Contract.Requires ( value != null );
			if ( model == null ) throw new ArgumentNullException ( "model" );
			if ( value == null ) throw new ArgumentNullException ( "value" );

			model.EnableSSL = TrueValue == value.ToLower ();
		}

		/// <summary>
		/// Use default credentials.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="value">Value.</param>
		private static void UseDefaultCredentials ( EmailSettingsModel model , string value ) {
			Contract.Requires ( model != null );
			Contract.Requires ( value != null );
			if ( model == null ) throw new ArgumentNullException ( "model" );
			if ( value == null ) throw new ArgumentNullException ( "value" );

			model.UseDefaultCredentials = TrueValue == value.ToLower ();
		}

		/// <summary>
		/// Timeout.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="value">Value.</param>
		private static void Timeout ( EmailSettingsModel model , string value ) {
			Contract.Requires ( model != null );
			Contract.Requires ( value != null );
			if ( model == null ) throw new ArgumentNullException ( "model" );
			if ( value == null ) throw new ArgumentNullException ( "value" );

			model.Timeout = Convert.ToInt32 ( value );
		}

		/// <summary>
		/// Port.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="value">Value.</param>
		private static void Port ( EmailSettingsModel model , string value ) {
			Contract.Requires ( model != null );
			Contract.Requires ( value != null );
			if ( model == null ) throw new ArgumentNullException ( "model" );
			if ( value == null ) throw new ArgumentNullException ( "value" );
			
			model.Port = Convert.ToInt32 ( value );
		}

		/// <summary>
		/// Host.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="value">Value.</param>
		private static void Host ( EmailSettingsModel model , string value ) {
			Contract.Requires ( model != null );
			Contract.Requires ( value != null );
			if ( model == null ) throw new ArgumentNullException ( "model" );
			if ( value == null ) throw new ArgumentNullException ( "value" );

			model.Host = value;
		}

		/// <summary>
		/// User login.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="value">Value.</param>
		private static void UserLogin ( EmailSettingsModel model , string value ) {
			Contract.Requires ( model != null );
			Contract.Requires ( value != null );
			if ( model == null ) throw new ArgumentNullException ( "model" );
			if ( value == null ) throw new ArgumentNullException ( "value" );

			model.UserLogin = value;
		}

		/// <summary>
		/// User password.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="value">Value.</param>
		private static void UserPassword ( EmailSettingsModel model , string value ) {
			Contract.Requires ( model != null );
			Contract.Requires ( value != null );
			if ( model == null ) throw new ArgumentNullException ( "model" );
			if ( value == null ) throw new ArgumentNullException ( "value" );

			model.UserPassword = value;
		}

		/// <summary>
		/// Deserialize.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public static EmailSettingsModel Deserialize ( string connectionString ) {
			Contract.Requires ( connectionString != null );
			if ( connectionString == null ) throw new ArgumentNullException ( "connectionString" );

			var result = new EmailSettingsModel ();

			var items = connectionString.Split ( OptionsSeparator );
			foreach ( var item in items ) {
				var itemSeparate = item.Split ( KeyAndValueSeparator );
				if ( itemSeparate.Length != 2 ) continue;

				var key = itemSeparate.First ();
				var value = itemSeparate.Last ();

				var method = typeof ( EmailSettingsDeserializer )
					.GetMethod ( key , BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static );
				if ( method == null ) continue;

				method.Invoke ( null , new List<object> { result , value }.ToArray () );
			}

			return result;
		}

	}

}
