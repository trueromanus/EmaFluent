using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace EmptyFlow.EmaFluent.Implementations.EmaFluentAlternateViewContexts {

	/// <summary>
	/// EmaFluent alternate view context.
	/// </summary>
	public sealed class EmaFluentAlternateViewContext : IEmaFluentAlternateViewContext {

		private AlternateView m_AlternateView;

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="body">Body.</param>
		/// <param name="mediaType">Media type.</param>
		/// <param name="encoding">Encoding.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluentAlternateViewContext Create ( string body , string mediaType , Encoding encoding ) {
			Contract.Requires ( body != null );
			Contract.Requires ( mediaType != null );
			Contract.Requires ( encoding != null );
			if ( body == null ) throw new ArgumentNullException ( "body" );
			if ( mediaType == null ) throw new ArgumentNullException ( "mediaType" );
			if ( encoding == null ) throw new ArgumentNullException ( "encoding" );

			m_AlternateView = AlternateView.CreateAlternateViewFromString ( body , encoding , mediaType );

			return this;
		}

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="mediaType">Media type.</param>
		/// <param name="renderEngine">Rendering engine.</param>
		/// <param name="templateName">Template name.</param>
		/// <param name="encoding">Encoding.</param>
		/// <param name="model">Model.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public IEmaFluentAlternateViewContext Create<T> ( IRenderEngine renderEngine , string templateName , T model , string mediaType , Encoding encoding ) {
			Contract.Requires ( renderEngine != null );
			Contract.Requires ( mediaType != null );
			Contract.Requires ( encoding != null );
			Contract.Requires ( templateName != null );
			if ( renderEngine == null ) throw new ArgumentNullException ( "renderEngine" );
			if ( mediaType == null ) throw new ArgumentNullException ( "mediaType" );
			if ( encoding == null ) throw new ArgumentNullException ( "encoding" );
			if ( templateName == null ) throw new ArgumentNullException ( "templateName" );

			m_AlternateView = AlternateView.CreateAlternateViewFromString (
				renderEngine.GetBody ( model , templateName ) ,
				encoding ,
				mediaType
			);

			return this;
		}

		/// <summary>
		/// Linked resource.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="mediaType">Media type.</param>
		/// <param name="contentId">Content identifier.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public IEmaFluentAlternateViewContext LinkedResource ( string filePath , string mediaType = "" , string contentId = "" ) {
			Contract.Requires ( filePath != null );
			Contract.Requires ( mediaType != null );
			Contract.Requires ( contentId != null );
			if ( filePath == null ) throw new ArgumentNullException ( "filePath" );
			if ( mediaType == null ) throw new ArgumentNullException ( "mediaType" );
			if ( contentId == null ) throw new ArgumentNullException ( "contentId" );
			if ( m_AlternateView == null ) throw new InvalidOperationException ( "Your need to calling Create method first." );

			var linkedResource = new LinkedResource ( filePath , mediaType );
			linkedResource.ContentId = contentId;

			m_AlternateView.LinkedResources.Add ( linkedResource );

			return this;
		}

		/// <summary>
		/// Linked resource.
		/// </summary>
		/// <param name="stream">Stream.</param>
		/// <param name="mediaType">Media type.</param>
		/// <param name="contentId">Content identifier.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public IEmaFluentAlternateViewContext LinkedResource ( Stream stream , string mediaType = "" , string contentId = "" ) {
			Contract.Requires ( stream != null );
			Contract.Requires ( mediaType != null );
			Contract.Requires ( contentId != null );
			if ( stream == null ) throw new ArgumentNullException ( "stream" );
			if ( mediaType == null ) throw new ArgumentNullException ( "mediaType" );
			if ( contentId == null ) throw new ArgumentNullException ( "contentId" );
			if ( m_AlternateView == null ) throw new InvalidOperationException ( "Your need to calling Create method first." );

			var linkedResource = new LinkedResource ( stream , mediaType );
			linkedResource.ContentId = contentId;

			m_AlternateView.LinkedResources.Add ( linkedResource );

			return this;
		}

		/// <summary>
		/// Get alternate view.
		/// </summary>
		public AlternateView GetAlternateView () {
			return m_AlternateView;
		}

		/// <summary>
		/// Dispose.
		/// </summary>
		public void Dispose () {
			m_AlternateView.Dispose ();
		}

	}

}
