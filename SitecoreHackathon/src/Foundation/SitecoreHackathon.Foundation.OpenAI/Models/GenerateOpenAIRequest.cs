using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace SitecoreHackathon.Foundation.OpenAI.Models.GenerateOpenAIRequest
{
	public class GenerateOpenAIRequest
	{
        public string Model { get; set; }
        public bool Store { get; set; }
        public Messages Messages { get; set; }
        public int MaxTokens { get; set; }
    }
    public class Messages
    {
        public string Role { get; set; }
        public List<Content> Content { get; set; }
    }

    public class Content
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public ImageUrlContent ImageUrl { get; set; }
    }

    public class ImageUrlContent
    {
        public string Url { get; set; }
    }

}