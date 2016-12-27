using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using UIKit;
using Foundation;

namespace FreshDirectShouldHireChrisB
{
	public partial class SolutionTwoViewController : UIViewController
	{
		public User user { get; set; }

		public SolutionTwoViewController() : base("SolutionTwoViewController", null)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Console.WriteLine("View Did Load");
			getApps();
		}

		public void getApps()
		{
			var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("com.SimpleBackgroundTransfer.BackgroundSession");
			var session = NSUrlSession.FromConfiguration(config, (NSUrlSessionDelegate)new NetworkDelegate(), new NSOperationQueue());
			var url = NSUrl.FromString("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=cboynton16&count=2");
			var request = new NSMutableUrlRequest(url);


			NSString key = new NSString("Authorization");
			NSString authentication = new NSString("Bearer AAAAAAAAAAAAAAAAAAAAAD33yQAAAAAAdN75U4ZxRRjPhruc7laPrdkz8Vc%3D4igSgnqa9FHA6GSlFpngaR809UWDuCgDb296mM86Om5P55a3Gh");

			Console.WriteLine(key);
			Console.WriteLine(authentication);

			var headers = NSDictionary.FromObjectAndKey(authentication, key);

			request.Headers = headers;

			var downloadTask = session.CreateDownloadTask(request);
			downloadTask.Resume();
		}


		public class NetworkDelegate : NSUrlSessionDownloadDelegate
		{
			public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
			{
				Console.WriteLine(downloadTask.Error);
				NSData data = NSFileManager.DefaultManager.Contents(location.Path);
				NSString dataString = NSString.FromData(data, NSStringEncoding.UTF8);
				Console.WriteLine(dataString);

				List<Dictionary<string,object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string,object>>>(dataString);

				var userInfoObject = new object();
				if (posts[0].TryGetValue("user", out userInfoObject))
				{
					//Console.WriteLine(userInfoObject);
				}

				string userInfoString = userInfoObject.ToString();

				var user = JsonConvert.DeserializeObject<User>(userInfoString);

				Console.WriteLine(user.name);
				Console.WriteLine(user.screen_name);
				Console.WriteLine(user.description);
				Console.WriteLine(user.location);

				for (int i = 0; i < posts.Count; i++)
				{
					var postString = posts[i].ToString();
					var post = JsonConvert.DeserializeObject<Post>(postString);

					Console.WriteLine("Post number" + i);
					Console.WriteLine(post.text);
					Console.WriteLine(post.createdAt);
				}
			}
		}
	}
}
