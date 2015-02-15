using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace EmaFluent.RazorTemplateEngine.Implementation.TemplateProviders {

	/// <summary>
	/// File template provider.
	/// </summary>
	public class FileTemplateProvider : ITemplateProvider {

		/// <summary>
		/// Get template.
		/// </summary>
		/// <param name="templateName">Template name.</param>
		/// <returns>Template.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public string GetTemplate ( string templateName ) {
			Contract.Requires ( templateName != null );
			if ( templateName == null ) throw new ArgumentNullException ( "templateName" );
			
			return File.ReadAllText ( templateName );
		}

	}

}
