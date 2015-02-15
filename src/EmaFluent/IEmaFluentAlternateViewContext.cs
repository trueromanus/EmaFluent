using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace EmptyFlow.EmaFluent {

	/// <summary>
	/// EmaFluent alternate view context.
	/// </summary>
	public interface IEmaFluentAlternateViewContext : IDisposable {

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="mediaType">Media type.</param>
		/// <param name="body">Body.</param>
		/// <param name="encoding">Encoding.</param>
		IEmaFluentAlternateViewContext Create ( string body , string mediaType , Encoding encoding );

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="mediaType">Media type.</param>
		/// <param name="renderEngine">Rendering engine.</param>
		/// <param name="templateName">Template name.</param>
		/// <param name="encoding">Encoding.</param>
		/// <param name="model">Model.</param>
		IEmaFluentAlternateViewContext Create<T> ( IRenderEngine renderEngine , string templateName , T model , string mediaType , Encoding encoding );

		/// <summary>
		/// Linked resource.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="mediaType">Media type.</param>
		/// <param name="contentId">Content identifier.</param>
		IEmaFluentAlternateViewContext LinkedResource ( string filePath , string mediaType = "" , string contentId = "" );

		/// <summary>
		/// Linked resource.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="mediaType">Media type.</param>
		/// <param name="contentId">Content identifier.</param>
		IEmaFluentAlternateViewContext LinkedResource ( Stream stream , string mediaType = "" , string contentId = "" );

		/// <summary>
		/// Get alternate view.
		/// </summary>
		AlternateView GetAlternateView ();

	}

}
