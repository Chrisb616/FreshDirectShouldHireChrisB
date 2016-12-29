using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Foundation;

namespace FreshDirectShouldHireChrisB
{
	public class TwitterNetworkDelegate : NSUrlSessionDownloadDelegate
	{

		public delegate void UserDataRetrieved(User user);
		public delegate void PostDataRetrieved(List<Post> posts);

		UserDataRetrieved UserDataRetrievedDelegate;
		PostDataRetrieved PostDataRetrievedDelegate;

		public TwitterNetworkDelegate(UserDataRetrieved userHandle, PostDataRetrieved postHandle)
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

			var postData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dataString);

			FindUserInfo(postData);

			var postObjectList = JsonConvert.DeserializeObject<List<object>>(dataString);

			var posts = new List<Post>();

			foreach (var postObject in postObjectList)
			{
				string postString = postObject.ToString();
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
