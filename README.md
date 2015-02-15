# EmaFluent


Fluent email wrapper. Sending mail to the declarative style.

### Features

If you want to send messages quickly and easily you can use this library to do this in a declarative style.

- Supported depedency injection style or classic style
- You may using any template engine via implement IRenderEngine interface
- Supported Alternate View
- Supported async/await send email

### Basic usage


```

new DefaultEmaFluent ( new EmaFluentFactory () )
	.SetSendingSettings (
		( provider ) => provider
			.Host ( "you smtp host" )
			.Port ( 25 )
			.SetNetworkCredential ( "you smtp login" , "you smtp password" )
	)
	.From ( "batman@gotham.dc" )
	.To ( "robin@gotham.dc" )
	.Subject ( "Hi Robin!" )
	.Body ( "How are you?" )
	.Send ();

```

### AlternateView usage

```

new DefaultEmaFluent ( new EmaFluentFactory () )
	.SetSendingSettings (
		( provider ) => provider
			.Host ( "you smtp host" )
			.Port ( 25 )
			.SetNetworkCredential ( "you smtp login" , "you smtp password" )
	)
	.From ( "robin@gotham.dc" )
	.To ( "batman@gotham.dc" )
	.Subject ( "Hi Batman!" )
	.AlternateView (
		context => context
			.Create ( "<img src=\"cid:companylogo\"/>" , MediaTypeNames.Text.Html , Encoding.UTF8 )
			.LinkedResource ( @"e:\pac.png" , mediaType: "image/png" , contentId: "companylogo" )
		,
		isHtml:true
	)
	.Send ();

```

### You may install in nuget

PM> Install-Package EmaFluent
PM> Install-Package EmaFluent.RazorTemplateEngine