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
using SitecoreHackathon.Foundation.OpenAI.Services;

namespace SitecoreHackathon.Feature.UpdateAltTags.Controllers
{
    public class MediaItemController : Controller
    {
        public Database db;
        
        public MediaItemController()
        {
            db = Factory.GetDatabase("master");
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ProcessMediaItems(string mediaItemPath)
        {
            mediaItemPath = Settings.GetSetting("UpdateAltTags.ImagePath");
            if (string.IsNullOrEmpty(mediaItemPath))
            {
                Log.Error("MediaItemPath is not set in the configuration.", this);
                return new JsonResult { Data = new { Success = false } };
            }

            return Json(IdentifyMediaItemandUpdateAltTags(mediaItemPath), JsonRequestBehavior.AllowGet);
        }

        private JsonResult IdentifyMediaItemandUpdateAltTags(string mediaItemPath)
        {
            try
            {
                //Process for Main Item
                IterateItemsAndUpdate(mediaItemPath);

                // Process all descendant items
                
                Item mediaItem = db.GetItem(mediaItemPath);

                foreach (Item child in mediaItem.Axes.GetDescendants())
                {
                    IterateItemsAndUpdate(child.Paths.FullPath);
                }
                return new JsonResult { Data = new { Success = true } };
            }
            catch (Exception ex)
            {
                Log.Error("Error processing media items.", ex, this);
                return new JsonResult { Data = new { Success = false } };
            }
        }

        private void IterateItemsAndUpdate(string mediaItemPath)
        {
            try
            {
                Item mediaItem = db.GetItem(mediaItemPath);

                Field altField = mediaItem.Fields["Alt"]; 
                if (altField != null && string.IsNullOrEmpty(altField.Value))
                {
                    var hostName = "https://www.tvsmotor.com";

                    string altText = ProcessImage(hostName + MediaManager.GetMediaUrl(mediaItem));
                    if (!string.IsNullOrEmpty(altText))
                    {
                        UpdateAltText(mediaItemPath, altText, db);
                    }
                }
                //var hostName = System.Web.HttpContext.Current.Request.Url.Host;
                
            }
            catch (Exception ex)
            {
                Log.Error("Error at IterateItemsAndUpdate.", ex, this);
                throw;
            }
           
        }

        private void UpdateAltText(string mediaItemPath, string altText, Database db)
        {
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                Item mediaItem = db.GetItem(mediaItemPath);
                if (mediaItem != null)
                {
                    mediaItem.Editing.BeginEdit();
                    try
                    {
                        mediaItem["Alt"] = altText.Trim('"');
                        mediaItem.Editing.EndEdit();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error updating alt text.", ex, this);
                        mediaItem.Editing.CancelEdit();
                        throw;
                    }
                }
            }
        }

        private string ProcessImage(string imageUrl)
        {
            try
            {
                OpenAIIntegration openAIIntegration = new OpenAIIntegration();
                return openAIIntegration.AltTextGeneration(imageUrl);
            }
            catch (Exception ex)
            {
                Log.Error("Error connecting open AI", ex, this);
                throw;
            }
        }
    }
}
