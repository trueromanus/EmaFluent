using System;
using EmaFluent.RazorTemplateEngine;
using EmaFluent.RazorTemplateEngine.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmaFluent.RazorTemplateEngineUnitTest {
	
	/// <summary>
	/// Razor template engine unit test.
	/// </summary>
	[TestClass]
	public class RazorTemplateEngineUnitTest {

		private class Wrapper {

			public TemplateEngine Engine {
				get;
				set;
			}

			public Mock<ITemplateProvider> StubTemplateProvider {
				get;
				set;
			}

		}

		/// <summary>
		/// Factory method.
		/// </summary>
		private Wrapper CreateWrapper () {
			var stubTemplateProvider = new Mock<ITemplateProvider> ();

			return new Wrapper {
				Engine = new TemplateEngine ( stubTemplateProvider.Object ),
				StubTemplateProvider = stubTemplateProvider
			};
		}

		/// <summary>
		/// Constructor provider null.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Constructor_ProviderNull () {
			new TemplateEngine ( null );
		}

		/// <summary>
		/// Get body string model.
		/// </summary>
		[TestMethod]
		public void GetBody_StringModel () {
			var model = "no";
			var templateName = "test";
			var wrapper = CreateWrapper ();
			wrapper.StubTemplateProvider
				.Setup ( a => a.GetTemplate ( templateName ) )
				.Returns (
					"@model string\n" +
					"Kaze @Model stigma"
				);

			var result = wrapper.Engine.GetBody<string> ( model , templateName );

			Assert.AreEqual ( result , "Kaze no stigma" );
		}

		/// <summary>
		/// Get body template name null.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetBody_TemplateName_Null () {
			var wrapper = CreateWrapper ();
			wrapper.Engine.GetBody<string> ( "" , null );
		}

	}

}
