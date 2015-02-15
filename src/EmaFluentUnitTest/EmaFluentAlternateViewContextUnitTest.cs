using System;
using System.IO;
using System.Text;
using EmptyFlow.EmaFluent;
using EmptyFlow.EmaFluent.Implementations.EmaFluentAlternateViewContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmaFluentUnitTest {

	/// <summary>
	/// Test class <see cref="EmaFluentAlternateViewContext"/>.
	/// </summary>
	[TestClass]
	public class EmaFluentAlternateViewContextUnitTest {

		private class Wrapper {

			public EmaFluentAlternateViewContext AlternateViewContext {
				get;
				set;
			}

		}

		/// <summary>
		/// Factory method.
		/// </summary>
		private Wrapper CreateWrapper () {
			return new Wrapper {
				AlternateViewContext = new EmaFluentAlternateViewContext ()
			};
		}

		/// <summary>
		/// Create check result.
		/// </summary>
		[TestMethod]
		public void Create_CheckResult () {
			var body = "Body content";
			var mediaType = "text/html";

			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create ( body , mediaType , Encoding.ASCII );

			var alternateView = wrapper.AlternateViewContext.GetAlternateView ();

			Assert.AreEqual ( alternateView.ContentType.MediaType , mediaType );
			Assert.AreEqual ( alternateView.ContentType.CharSet , "us-ascii" );
			Assert.AreEqual ( new StreamReader ( alternateView.ContentStream ).ReadToEnd () , body );
		}

		/// <summary>
		/// Create throw body argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Create_ThrowBodyArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create ( null , "text/html" , Encoding.ASCII );
		}

		/// <summary>
		/// Create throw media type argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Create_ThrowMediaTypeArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create ( "Throw test" , null , Encoding.ASCII );
		}

		/// <summary>
		/// Create throw encoding argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void Create_ThrowEncodingArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create ( "Throw test" , "text/html" , null );
		}

		/// <summary>
		/// Create template check result.
		/// </summary>
		[TestMethod]
		public void CreateTemplate_CheckResult () {
			var body = "Body content";
			var mediaType = "text/html";
			var model = 1;
			var templateName = "template";

			var stubRenderingEngine = new Mock<IRenderEngine> ();
			stubRenderingEngine
				.Setup ( a => a.GetBody<int> ( model , templateName ) )
				.Returns ( body );
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create<int> ( stubRenderingEngine.Object , templateName , model , mediaType , Encoding.ASCII );

			var alternateView = wrapper.AlternateViewContext.GetAlternateView ();

			Assert.AreEqual ( alternateView.ContentType.MediaType , mediaType );
			Assert.AreEqual ( alternateView.ContentType.CharSet , "us-ascii" );
			Assert.AreEqual ( new StreamReader ( alternateView.ContentStream ).ReadToEnd () , body );
		}

		/// <summary>
		/// Create template throw render engine argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void CreateTemplate_ThrowRenderEngineArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create<int> ( null , "template" , 1 , "text/html" , Encoding.ASCII );
		}

		/// <summary>
		/// Create template throw media type argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void CreateTemplate_ThrowMediaTypeArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create<int> ( new Mock<IRenderEngine> ().Object , "template" , 1 , null , Encoding.ASCII );
		}

		/// <summary>
		/// Create template throw encoding argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void CreateTemplate_ThrowEncodingArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create<int> ( new Mock<IRenderEngine> ().Object , "template",  1 , "text/html" , null );
		}

		/// <summary>
		/// Create template throw template argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void CreateTemplate_ThrowTemplateArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create<int> ( new Mock<IRenderEngine> ().Object , null , 1 , "text/html" , Encoding.ASCII );
		}

		/// <summary>
		/// Linked resource filepath check result.
		/// </summary>
		[TestMethod]
		public void LinkedResource_FilePath_CheckResult () {
			var body = "Body content";
			var mediaType = "text/html";
			var path = Path.Combine ( AppDomain.CurrentDomain.BaseDirectory , "test.txt" );
			var contentId = "contentId";

			using ( var file = File.CreateText ( path ) ) {
				file.WriteLine ( body );
			}

			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create ( body , mediaType , Encoding.ASCII );
			wrapper.AlternateViewContext.LinkedResource ( path , mediaType , contentId );

			var alternateView = wrapper.AlternateViewContext.GetAlternateView ();

			var linkedResource = alternateView.LinkedResources[0];

			Assert.AreEqual ( linkedResource.ContentId , contentId );
			Assert.AreEqual ( linkedResource.ContentType , mediaType );
		}

		/// <summary>
		/// Linked resource filepath throw path argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void LinkedResource_FilePath_ThrowPathArgument () {
			var wrapper = CreateWrapper ();
			string emptyString = null;
			wrapper.AlternateViewContext.LinkedResource ( emptyString , "text/html" , "ContentId" );
		}

		/// <summary>
		/// Linked resource filepath throw mediatype argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void LinkedResource_FilePath_ThrowMediaTypeArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.LinkedResource ( "Throw test" , null , "ContentId" );
		}

		/// <summary>
		/// Linked resource filepath throw content identifier argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void LinkedResource_FilePath_ThrowContentIdArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.LinkedResource ( "Throw test" , "text/html" , null );
		}

		/// <summary>
		/// Linked resource filepath throw alternate view.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( InvalidOperationException ) )]
		public void LinkedResource_FilePath_ThrowAlternateView () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.LinkedResource ( "Throw test" , "text/html" , "ContentId" );
		}

		/// <summary>
		/// Linked resource stream check result.
		/// </summary>
		[TestMethod]
		public void LinkedResource_Stream_CheckResult () {
			var body = "Body content";
			var mediaType = "text/html";
			var stream = new MemoryStream ();
			var writer = new StreamWriter ( stream );
			writer.WriteLine ( body );
			var contentId = "contentId";

			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.Create ( body , mediaType , Encoding.ASCII );
			wrapper.AlternateViewContext.LinkedResource ( stream , mediaType , contentId );

			var alternateView = wrapper.AlternateViewContext.GetAlternateView ();

			var linkedResource = alternateView.LinkedResources[0];

			Assert.AreEqual ( linkedResource.ContentId , contentId );
			Assert.AreEqual ( linkedResource.ContentType , mediaType );
		}

		/// <summary>
		/// Linked resource stream throw stream argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void LinkedResource_Stream_ThrowStreamArgument () {
			var wrapper = CreateWrapper ();
			Stream emptyStream = null;
			wrapper.AlternateViewContext.LinkedResource ( emptyStream , "text/html" , "ContentId" );
		}

		/// <summary>
		/// Linked resource stream throw mediatype argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void LinkedResource_Stream_ThrowMediaTypeArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.LinkedResource ( new MemoryStream () , null , "ContentId" );
		}

		/// <summary>
		/// Linked resource filepath throw content identifier argument.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( ArgumentNullException ) )]
		public void LinkedResource_Stream_ThrowContentIdArgument () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.LinkedResource ( new MemoryStream () , "text/html" , null );
		}

		/// <summary>
		/// Linked resource filepath throw alternate view.
		/// </summary>
		[TestMethod]
		[ExpectedException ( typeof ( InvalidOperationException ) )]
		public void LinkedResource_Stream_ThrowAlternateView () {
			var wrapper = CreateWrapper ();
			wrapper.AlternateViewContext.LinkedResource ( new MemoryStream () , "text/html" , "ContentId" );
		}


	}

}
