using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaFluent.RazorTemplateEngine.Implementation.TemplateProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmaFluent.RazorTemplateEngineUnitTest {

	/// <summary>
	/// File template provider unit test.
	/// </summary>
	[TestClass]
	public class FileTemplateProviderUnitTest {

		private class Wrapper {

			public FileTemplateProvider Provider {
				get;
				set;
			}

		}

		/// <summary>
		/// Factory method.
		/// </summary>
		private Wrapper CreateWrapper () {
			return new Wrapper {
				Provider = new FileTemplateProvider ()
			};
		}

		[TestMethod]
		public void GetTemplate_ReadText () {
			var path = Path.Combine ( AppDomain.CurrentDomain.BaseDirectory , "test.txt" );
			var testText = "fullmetal alchemist";
			var wrapper = CreateWrapper ();

			File.WriteAllText ( path , testText );

			var result = wrapper.Provider.GetTemplate ( path );

			File.Delete ( path );

			Assert.AreEqual ( result , testText );
		}

		/// <summary>
		/// Get template path null.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void GetTemplate_Path_Null () {
			CreateWrapper ().Provider.GetTemplate ( null );
		}

	}

}
