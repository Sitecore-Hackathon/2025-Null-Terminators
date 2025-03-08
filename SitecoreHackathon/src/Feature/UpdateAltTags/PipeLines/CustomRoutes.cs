using Sitecore.Pipelines;
using SitecoreHackathon.Feature.UpdateAltTags.App_Start;
using System.Web.Routing;


namespace SitecoreHackathon.Feature.UpdateAltTags.PipeLines
{
	public class CustomRoutes
	{
        public void Process(PipelineArgs args)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}