using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Foundation;
using UIKit;

namespace TwitterHistory
{
	public class TwitterNetworkDelegate : NSUrlSessionDownloadDelegate
	{

		public delegate void UserDataRetrieved(User user);
		public delegate void UserProfileImageRetreived(UIImage profileImage);
		public delegate void TweetDataRetrieved(List<Tweet> tweets);
		public delegate void DidRecieveError();

		UserDataRetrieved UserDataRetrievedDelegate;
		UserProfileImageRetreived UserProfileImageRetrievedDelegate;
		TweetDataRetrieved TweetDataRetrievedDelegate;
		DidRecieveError ErrorDelegate;

		public TwitterNetworkDelegate(UserDataRetrieved UserHandle, TweetDataRetrieved TweetHandle, UserProfileImageRetreived ImageHandle, DidRecieveError ErrorHandle)
		{
			UserDataRetrievedDelegate = UserHandle;
			TweetDataRetrievedDelegate = TweetHandle;
			UserProfileImageRetrievedDelegate = ImageHandle;
			ErrorDelegate = ErrorHandle;
		}

		public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			//solution1
			NSData data = NSFileManager.DefaultManager.Contents(location.Path);
			NSString dataString = NSString.FromData(data, NSStringEncoding.UTF8);
			Console.WriteLine(dataString);


			if (!dataString.Contains(new NSString("created_at")))
			{
				ErrorDelegate();
				return;
			}

			var tweetData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dataString);

			FindUserInfo(tweetData);

			var tweetObjectList = JsonConvert.DeserializeObject<List<object>>(dataString);

			var tweets = new List<Tweet>();

			Console.WriteLine("Tweet Data:");
			foreach (var postObject in tweetObjectList)
			{
				string postString = postObject.ToString();
				var tweet = JsonConvert.DeserializeObject<Tweet>(postString);
				tweets.Add(tweet);
				Console.WriteLine(tweet.text);
			}

			TweetDataRetrievedDelegate(tweets);
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

			CallImageDownload(user.profile_image_url_https);
		}

		//public void CallImageDownload(string imageURL)
		//{
		//	var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("com.SimpleBackgroundTransferTwo.BackgroundSession");
		//	var downloadDelegate = (NSUrlSessionDelegate)new TwitterImageDownloadDelegate(HandleImageDownloaded);
		//	var session = NSUrlSession.FromConfiguration(config, downloadDelegate, new NSOperationQueue());
		//	var url = NSUrl.FromString(imageURL);
		//	var request = new NSMutableUrlRequest(url);

		//	Console.WriteLine(url);

		//	var task = session.CreateDataTask(request);
		//	task.Resume();
		//}

		public void CallImageDownload(string imageURL)
		{
			
		}

		void HandleAction(IntPtr arg1, nuint arg2)
		{

		}


		public void HandleImageDownloaded(UIImage ProfileImage)
		{
			Console.WriteLine("Should show image");
			UserProfileImageRetrievedDelegate(ProfileImage);
		}
	}
}

