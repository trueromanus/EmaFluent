using System;
using System.Diagnostics.Contracts;
using EmptyFlow.EmaFluent;
using RazorEngine;

namespace EmaFluent.RazorTemplateEngine.Implementation {

	/// <summary>
	/// Razor template engine.
	/// </summary>
	public class TemplateEngine : IRenderEngine {

		private ITemplateProvider m_TemplateProvider;

		/// <summary>
		/// Constructor injection.
		/// </summary>
		/// <param name="templateProvider">Template provider.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public TemplateEngine ( ITemplateProvider templateProvider ) {
			Contract.Requires ( templateProvider != null );
			if ( templateProvider == null ) throw new ArgumentNullException ( "templateProvider" );

			m_TemplateProvider = templateProvider;
		}

		/// <summary>
		/// Get body.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="templateName">Template name.</param>
		/// <returns>Rendered template.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public string GetBody<T> ( T model , string templateName ) {
			Contract.Requires ( templateName != null );
			if ( templateName == null ) throw new ArgumentNullException ( "templateName" );

			return Razor.Parse<T> ( m_TemplateProvider.GetTemplate ( templateName ) , model );
		}

	}

}
