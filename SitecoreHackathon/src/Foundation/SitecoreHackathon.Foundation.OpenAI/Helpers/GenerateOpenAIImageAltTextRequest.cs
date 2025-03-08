using SitecoreHackathon.Foundation.OpenAI.Models.GenerateOpenAIRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreHackathon.Foundation.OpenAI.Helpers
{
	public class GenerateOpenAIImageAltTextRequest
	{

        /// <summary>
        /// This method would generate the complete request to be sent to OPEN AI
        /// Not just alt text generation, this is a generic method which can be used 
        /// to sent request to OPEN AI API.
        /// </summary>
        /// <param name="userInputDetails"></param>
        /// <returns>OPEN AI API Request Details</returns>
        
        public static GenerateOpenAIRequest GenerateRequest(string userInputDetails)
        {
            try
            {
                GenerateOpenAIRequest request = new GenerateOpenAIRequest
                {
                    MaxTokens = 200,
                    Store = true,
                    Model = "gpt-4o",
                    Messages = OpenAIAltTextPrompt(userInputDetails)
                };
                return request;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, nameof(GenerateRequest));
                throw;
            }
        }

        /// <summary>
        /// This method would generate the prompt details to generate ALT text.
        /// </summary>
        /// <param name="ImageURL"></param>
        /// <returns></returns>

        private static Messages OpenAIAltTextPrompt(string ImageURL)
        {
            try
            {
                ImageUrlContent imageUrlContent = new ImageUrlContent
                {
                    Url = ImageURL
                };

                Messages messages = new Messages();
                messages.Role = "user";
                Content TextPrompt = new Content
                {
                    Type = "text",
                    Text = "Suggest ALT tags in less than 5 words"
                };

                Content ImageUrlPromt = new Content
                {
                    Type = "image_url",
                    ImageUrl = imageUrlContent
                };
                messages.Content.Add(TextPrompt);
                messages.Content.Add(ImageUrlPromt);

                return messages;
            }
            catch(Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, nameof(OpenAIAltTextPrompt));
                throw;
            }
        }
    }
}