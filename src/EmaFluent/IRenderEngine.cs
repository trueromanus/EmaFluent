
namespace EmptyFlow.EmaFluent {
	
	/// <summary>
	/// Rander engine.
	/// </summary>
	public interface IRenderEngine {

		/// <summary>
		/// Get body.
		/// </summary>
		/// <param name="model">Model.</param>
		/// <param name="templateName">Template name.</param>
		/// <returns>Rendered body.</returns>
		string GetBody<T> ( T model , string templateName );

	}

}
