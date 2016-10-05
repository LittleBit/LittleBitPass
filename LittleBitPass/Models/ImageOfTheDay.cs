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
		            var obj = JsonConvert.DeserializeObject<ImagesOfTheDayJson>(response).Images[0];
		            Image = "http://www.bing.com" + obj.Url;
		            ImageCopyright = obj.Copyright;
		        }
		    }
		    catch
		    {
		        Debug.WriteLine("Updating the image from the Bing service failed, default image is used...");
                Image = "/Content/Images/default.jpg";
                ImageCopyright = "Night Sky";
            }
		}
		

        internal class ImagesOfTheDayJson
        {

            [JsonProperty("images")]
            public Images[] Images { get; set; }

            [JsonProperty("tooltips")]
            public Tooltips Tooltips { get; set; }
        }

        internal class Images
        {

            [JsonProperty("startdate")]
            public string Startdate { get; set; }

            [JsonProperty("fullstartdate")]
            public string Fullstartdate { get; set; }

            [JsonProperty("enddate")]
            public string Enddate { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("urlbase")]
            public string Urlbase { get; set; }

            [JsonProperty("copyright")]
            public string Copyright { get; set; }

            [JsonProperty("copyrightlink")]
            public string Copyrightlink { get; set; }

            [JsonProperty("wp")]
            public bool Wp { get; set; }

            [JsonProperty("hsh")]
            public string Hsh { get; set; }

            [JsonProperty("drk")]
            public int Drk { get; set; }

            [JsonProperty("top")]
            public int Top { get; set; }

            [JsonProperty("bot")]
            public int Bot { get; set; }

            [JsonProperty("hs")]
            public object[] Hs { get; set; }
        }

        internal class Tooltips
        {

            [JsonProperty("loading")]
            public string Loading { get; set; }

            [JsonProperty("previous")]
            public string Previous { get; set; }

            [JsonProperty("next")]
            public string Next { get; set; }

            [JsonProperty("walle")]
            public string Walle { get; set; }

            [JsonProperty("walls")]
            public string Walls { get; set; }
        }

    }
}

