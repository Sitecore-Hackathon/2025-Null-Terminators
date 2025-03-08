using Newtonsoft.Json;
using SitecoreHackathon.Foundation.OpenAI.Models;
using SitecoreHackathon.Foundation.OpenAI.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Sitecore.Configuration;
using System;
using System.Net;

namespace SitecoreHackathon.Foundation.OpenAI.Services
{
    public class OpenAIIntegration
    {
        /// <summary>
        /// Based on the Image URL, CHAT gpt will generate ALT text
        /// </summary>
        /// <param name="ImageURL"></param>
        /// <returns></returns>
        public string AltTextGeneration(string ImageURL)
        {
            try
            {
                string accessToken = Settings.GetSetting("OpenAI.ApiKey");
                string apiEndPoint = Settings.GetSetting("OpenAI.APIEndPoint");
                var response = string.Empty;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                    var jsonContent = JsonConvert.SerializeObject(GenerateOpenAIImageAltTextRequest.GenerateRequest(ImageURL)).ToLower();
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var res = client.PostAsync(apiEndPoint, content).Result;
                    if(res.StatusCode==HttpStatusCode.OK)
                    {
                        response = res.Content.ReadAsStringAsync().Result.ToString();
                        ImageAltTextOpenAIResponse DetailedResponse = JsonConvert.DeserializeObject<ImageAltTextOpenAIResponse>(response);
                        return DetailedResponse.Choices[0].Message.Content;
                    }
                    else
                    {
                        Sitecore.Diagnostics.Log.Error("OpenAI response : " + res.StatusCode, this);
                        return Settings.GetSetting("OpenAI.ApiFailure");
                    }
                }
            }
            catch(Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, this);
                return Settings.GetSetting("OpenAI.ErrorMessage");
            }
        }
    }
}