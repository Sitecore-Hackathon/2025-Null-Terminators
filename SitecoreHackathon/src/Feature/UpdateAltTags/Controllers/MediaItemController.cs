using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;

namespace SitecoreHackathon.Feature.UpdateAltTags.Controllers
{
    public class MediaItemController : Controller
    {
        // GET: MediaItem
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcessMediaItems()
        {
            string mediaItemPath = ConfigurationManager.AppSettings["MediaItemPath"];
            if (string.IsNullOrEmpty(mediaItemPath))
            {
                Log.Error("MediaItemPath is not set in the configuration.", this);
            }

            List<string> mediaUrls = GetMediaItemUrls(mediaItemPath);
            Database db = Sitecore.Configuration.Factory.GetDatabase("master");

            foreach (string mediaUrl in mediaUrls)
            {
                string altText = ProcessImage(mediaUrl);
                if (!string.IsNullOrEmpty(altText))
                {
                    UpdateAltText(mediaItemPath, altText, db);
                }
            }
            return View();
        }

        private List<string> GetMediaItemUrls(string mediaItemPath)
        {
            List<string> mediaUrls = new List<string>();
            try
            {
                Database db = Sitecore.Configuration.Factory.GetDatabase("master");
                if (db == null)
                {
                    Log.Error("Failed to retrieve Sitecore master database.", this);
                    return mediaUrls;
                }

                Item mediaItem = db.GetItem(mediaItemPath);
                if (mediaItem == null)
                {
                    Log.Warn($"Media item not found at path: {mediaItemPath}", this);
                    return mediaUrls;
                }

                // Process parent item
                mediaUrls.Add(MediaManager.GetMediaUrl(mediaItem));

                // Process all descendant items
                foreach (Item child in mediaItem.Axes.GetDescendants())
                {
                    mediaUrls.Add(MediaManager.GetMediaUrl(child));
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error processing media items.", ex, this);
            }
            return mediaUrls;
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
                        mediaItem["Alt"] = altText;
                        mediaItem.Editing.EndEdit();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error updating alt text.", ex, this);
                        mediaItem.Editing.CancelEdit();
                    }
                }
            }
        }

        private string ProcessImage(string imageUrl)
        {
            // Call OpenAI ChatGPT integration logic here
            return "Generated Alt Text"; // Placeholder for actual response
        }
    }
}
