﻿using System;
using UIKit;
namespace TwitterHistory
{
	public class User
	{
		public string name { get; set; }
		public UIImage profilePicture { get; set; }
		public string screen_name { get; set; }
		public string location { get; set; }
		public string description { get; set; }
		public string profile_image_url_https { get; set; }
	}
}
