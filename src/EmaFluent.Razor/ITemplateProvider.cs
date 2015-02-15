
namespace EmaFluent.RazorTemplateEngine {
	
	/// <summary>
	/// Template provider.
	/// </summary>
	public interface ITemplateProvider {

		/// <summary>
		/// Get template.
		/// </summary>
		/// <param name="templateName">Template name.</param>
		/// <returns>Template.</returns>
		string GetTemplate ( string templateName );

	}

}
