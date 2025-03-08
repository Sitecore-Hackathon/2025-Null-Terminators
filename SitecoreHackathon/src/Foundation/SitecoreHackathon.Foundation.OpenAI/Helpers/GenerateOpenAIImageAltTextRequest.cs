using Sitecore.Configuration;
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
                    Max_Tokens = Convert.ToInt32(Settings.GetSetting("OpenAI.MaxTokens")),
                    Store = Convert.ToBoolean(Settings.GetSetting("OpenAI.Store")),
                    Model = Settings.GetSetting("OpenAI.Model"),
                    messages = OpenAIAltTextPrompt(userInputDetails)
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

        private static List<Messages> OpenAIAltTextPrompt(string ImageURL)
        {
            try
            {
                ImageUrlContent imageUrlContent = new ImageUrlContent
                {
                    Url = ImageURL
                };

                List<Messages> listOfMessages = new List<Messages>();
                Messages message = new Messages();
                message.Role = Settings.GetSetting("OpenAI.Role");
                message.Content = new List<Object>();
                TextContent TextPrompt = new TextContent
                {
                    Type = Settings.GetSetting("OpenAI.TextPromptLabel"),
                    Text = Settings.GetSetting("OpenAI.TextPrompt"),
                };

                ImageContent ImageUrlPromt = new ImageContent
                {
                    Type = Settings.GetSetting("OpenAI.ImageUrlPromptLabel"),
                    Image_Url = imageUrlContent
                };
                message.Content.Add(TextPrompt);
                message.Content.Add(ImageUrlPromt);

                listOfMessages.Add(message);

                return listOfMessages;
            }
            catch(Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, nameof(OpenAIAltTextPrompt));
                throw;
            }
        }
    }
}