using Sitecore.Data.Fields;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using SitecoreHackathon.Foundation.OpenAI.Services;
using System;
using System.Web.Mvc;
using Sitecore.Configuration;
using Sitecore.Data.Items;

namespace SitecoreHackathon.Feature.UpdateAltTags.Repository
{
	public class MediaItemRepository : IMediaItemRepository
	{
        /// <summary>
        /// The Sitecore database instance.
        /// </summary>
        public Database db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaItemRepository"/> class.
        /// </summary>
        public MediaItemRepository()
        {
            db = Factory.GetDatabase("master");
        }

        /// <summary>
        /// Identifies the media item and updates its alt tags.
        /// </summary>
        /// <param name="mediaItemPath">The path of the media item.</param>
        /// <returns>A JSON result indicating success or failure.</returns>
        public JsonResult IdentifyMediaItemandUpdateAltTags(string mediaItemPath)
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

        /// <summary>
        /// Iterates through items and identifies media item of which alt text needs to be updated.
        /// </summary>
        /// <param name="mediaItemPath">The path of the media item.</param>
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

        /// <summary>
        /// Updates the alt text of a media item.
        /// </summary>
        /// <param name="mediaItemPath">The path of the media item.</param>
        /// <param name="altText">The alt text to update.</param>
        /// <param name="db">The Sitecore database instance.</param>
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

        /// <summary>
        /// Processes the image to generate alt text using OpenAI.
        /// </summary>
        /// <param name="imageUrl">The URL of the image.</param>
        /// <returns>The generated alt text.</returns>
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