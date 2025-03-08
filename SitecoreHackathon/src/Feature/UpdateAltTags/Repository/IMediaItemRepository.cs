using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreHackathon.Feature.UpdateAltTags.Repository
{
    public interface IMediaItemRepository
    {
        JsonResult IdentifyMediaItemandUpdateAltTags(string mediaPath);
    }
}