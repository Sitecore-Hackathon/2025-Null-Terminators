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
        public List<Messages> messages { get; set; }
        public int Max_Tokens { get; set; }
    }
    public class Messages
    {
        public string Role { get; set; }
        public List<Object> Content { get; set; }
    }

    public class TextContent
    {
        public string Type { get; set; }
        public string Text { get; set; }
       
    }

    public class ImageContent
    {
        public string Type { get; set; }
        public ImageUrlContent Image_Url { get; set; }
    }

    public class ImageUrlContent
    {
        public string Url { get; set; }
    }

}