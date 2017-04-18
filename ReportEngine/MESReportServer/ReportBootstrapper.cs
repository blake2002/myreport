using System;
using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;

namespace NetSCADA.MESReportServer
{
	public class ReportBootstrapper :DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions (NancyConventions nancyConventions)
		{
			base.ConfigureConventions (nancyConventions);
			//nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("ServerWeb"));
			nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilderEx.AddDirectory ("ServerWeb/Query"));
			nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilderEx.AddDirectory ("ServerWeb/Export"));
			nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilderEx.AddDirectory ("Web"));
//			nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("content"));
//			nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("css"));
//			nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("fonts"));

		}

		protected override void RequestStartup (TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
		{
			// At request startup we modify the request pipelines to
			// include stateless authentication
			//
			// Configuring stateless authentication is simple. Just use the
			// NancyContext to get the apiKey. Then, use the apiKey to get
			// your user's identity.


			//	AllowAccessToConsumingSite (pipelines);
			base.RequestStartup (requestContainer, pipelines, context);

		}

		static void AllowAccessToConsumingSite (IPipelines pipelines)
		{
			pipelines.AfterRequest.AddItemToEndOfPipeline (x => {
				x.Response.Headers.Add ("Access-Control-Allow-Origin", "*");
				x.Response.Headers.Add ("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
			});
		}

		protected override void ApplicationStartup (TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup (container, pipelines);
			AllowAccessToConsumingSite (pipelines);
		}
	
	}


}

