using System;
using System.Runtime;
using System.Collections.Generic;
using Newtonsoft.Json;

using UIKit;
using Foundation;

namespace FreshDirectShouldHireChrisB
{
	public partial class SolutionTwoViewController : UIViewController
	{
		

		public SolutionTwoViewController() : base("SolutionTwoViewController", null)
		{
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Console.WriteLine("View Did Load");
			getApps();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		public void getApps()
		{
			var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("com.SimpleBackgroundTransfer.BackgroundSession");
			var downloadDelegate = (NSUrlSessionDelegate)new NetworkDelegate(HandleUserDataRetrieved,HandlePostDataRetrieved);
			var session = NSUrlSession.FromConfiguration(config, downloadDelegate, new NSOperationQueue());
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

		void HandleUserDataRetrieved(FreshDirectShouldHireChrisB.User user)
		{
			Console.WriteLine("In User Handle Function");
			Console.WriteLine(user.screen_name);
			return;
		}
		void HandlePostDataRetrieved(List<FreshDirectShouldHireChrisB.Post> posts)
		{
			Console.WriteLine("In Post Handle Function");
			foreach (Post post in posts) {
				Console.WriteLine(post.text);
			}
		}

		public class NetworkDelegate : NSUrlSessionDownloadDelegate
		{

			public delegate void UserDataRetrieved(User user);
			public delegate void PostDataRetrieved(List<Post> posts);

			UserDataRetrieved UserDataRetrievedDelegate;
			PostDataRetrieved PostDataRetrievedDelegate;

			public NetworkDelegate(UserDataRetrieved userHandle, PostDataRetrieved postHandle)
			{
				UserDataRetrievedDelegate = userHandle;
				PostDataRetrievedDelegate = postHandle;
			}

			public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
			{
				//solution1
				NSData data = NSFileManager.DefaultManager.Contents(location.Path);
				NSString dataString = NSString.FromData(data, NSStringEncoding.UTF8);
				Console.WriteLine(dataString);

				var postData = JsonConvert.DeserializeObject<List<Dictionary<string,object>>> (dataString);

				FindUserInfo(postData);

				var posts = new List<Post>();

				foreach (Dictionary<string,object> postInfo in postData)
				{
					var postString = postInfo.ToString();
					Console.WriteLine(postString);

					var post = JsonConvert.DeserializeObject<Post>(postString);

					posts.Add(post);
				}

				PostDataRetrievedDelegate(posts);
			}
			public void FindUserInfo(List<Dictionary<string, object>> postData)
			{

				object userObject;
				if (postData[0].TryGetValue("user", out userObject))
				{
					Console.WriteLine(userObject);
				};

				string userString = userObject.ToString();

				var user = JsonConvert.DeserializeObject<User>(userString);

				UserDataRetrievedDelegate(user);
			}

		}
	}
}


