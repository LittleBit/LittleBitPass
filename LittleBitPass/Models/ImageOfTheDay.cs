using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Timers;
using Newtonsoft.Json;

namespace LittleBitPass.Models
{
	public class ImageOfTheDay {
		public static readonly ImageOfTheDay Instance = new ImageOfTheDay();
		public string Image, ImageCopyright;

	    private ImageOfTheDay() {
		    var update = new Timer(3600000) {AutoReset = true};
		    update.Elapsed += (ob, ev) => UpdateImage();
			update.Start ();
			UpdateImage ();
		}

		private void UpdateImage() {
		    try
		    {
		        using (var client = new WebClient())
		        {
		            var response = client.DownloadString(Constanten.BING_PICTURE_OF_THE_DAY_ADDR);
		            var obj = JsonConvert.DeserializeObject<RootObject>(response).images[0];
		            Image = "http://www.bing.com" + obj.url;
		            ImageCopyright = obj.copyright;
		        }
		    }
		    catch
		    {
		        Debug.WriteLine("Updating the image from the Bing service failed, default image is used...");
		    }
		    finally
		    {
		        Image = "/Content/Images/default.jpg";
		        ImageCopyright = "Night Sky";
		    }
		}

		private class IotdImage
		{
			public string startdate { get; set; }
			public string fullstartdate { get; set; }
			public string enddate { get; set; }
			public string url { get; set; }
			public string urlbase { get; set; }
			public string copyright { get; set; }
			public string copyrightlink { get; set; }
			public bool wp { get; set; }
			public string hsh { get; set; }
			public int drk { get; set; }
			public int top { get; set; }
			public int bot { get; set; }
			public List<object> hs { get; set; }
		}

		private class Tooltips
		{
			public string loading { get; set; }
			public string previous { get; set; }
			public string next { get; set; }
			public string walle { get; set; }
			public string walls { get; set; }
		}

		private class RootObject
		{
			public List<IotdImage> images { get; set; }
			public Tooltips tooltips { get; set; }
		}

	}
}

