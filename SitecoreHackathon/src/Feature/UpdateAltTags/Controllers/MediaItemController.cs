using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using Sitecore.Shell.Framework.Commands.TemplateBuilder;
using SitecoreHackathon.Feature.UpdateAltTags.Repository;
using SitecoreHackathon.Foundation.OpenAI.Services;

namespace SitecoreHackathon.Feature.UpdateAltTags.Controllers
{
    public class MediaItemController : Controller
    {
        public IMediaItemRepository _mediaItemRepository;

        public MediaItemController()
        {
            _mediaItemRepository = new MediaItemRepository();
        }

        /// <summary>
        /// Processes media items and updates their alt tags.
        /// </summary>
        /// <param name="mediaItemPath">The path of the media item.</param>
        /// <returns>A JSON result indicating success or failure.</returns>
        [HttpGet]
        public JsonResult ProcessMediaItems(string mediaItemPath)
        {
            if (string.IsNullOrEmpty(mediaItemPath))
            {
                Log.Error("MediaItemPath is not set in the configuration.", this);
                return new JsonResult { Data = new { Success = false } };
            }

            return Json(_mediaItemRepository.IdentifyMediaItemandUpdateAltTags(mediaItemPath), JsonRequestBehavior.AllowGet);
        }
    }
}
