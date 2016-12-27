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
		User user;
		List<Post> posts = new List<Post>();

		UITextField usernameTextField = new UITextField();
		UIButton searchButton = new UIButton();
		UITableView postHistoryTableView = new UITableView();

		public SolutionTwoViewController() : base("SolutionTwoViewController", null)
		{
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Console.WriteLine("View Did Load");
			getTwitterHistory();
			setUpViews();
			setUpTableView();
		}

		public void setUpViews()
		{
			View.BackgroundColor = new UIColor(74f / 255f, 54f / 255f, 76f / 255f, 1f);
		}

		public void setUpTableView()
		{
			postHistoryTableView.RegisterClassForCellReuse(typeof(UITableViewCell), "postCell");
			postHistoryTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			postHistoryTableView.Delegate = new PostHistoryTableViewDelegate();
			postHistoryTableView.DataSource = new PostHistoryTableViewDataSource(posts);


			this.View.AddSubview(postHistoryTableView);

			postHistoryTableView.Frame = CoreGraphics.CGRect.FromLTRB(
				0,
				UIScreen.MainScreen.Bounds.Height * 0.3f,
				UIScreen.MainScreen.Bounds.Width,
				UIScreen.MainScreen.Bounds.Height
			);
		}

		public void getTwitterHistory()
		{
			var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("com.SimpleBackgroundTransfer.BackgroundSession");
			var downloadDelegate = (NSUrlSessionDelegate)new NetworkDelegate(HandleUserDataRetrieved,HandlePostDataRetrieved);
			var session = NSUrlSession.FromConfiguration(config, downloadDelegate, new NSOperationQueue());
			var url = NSUrl.FromString("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=cboynton16");
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
			this.user = user;
			Console.WriteLine("In User Handle Function");
			Console.WriteLine(user.screen_name);
			return;
		}
		void HandlePostDataRetrieved(List<FreshDirectShouldHireChrisB.Post> posts)
		{
			NSOperationQueue.MainQueue.AddOperation(() =>
			{
				this.posts = posts;
				postHistoryTableView.DataSource = new PostHistoryTableViewDataSource(this.posts);
				Console.WriteLine("In Post Handle Function");
				foreach (Post post in posts)
				{
					Console.WriteLine(post.text);
				}
				postHistoryTableView.ReloadData();
			});
		}

		public class PostHistoryTableViewDelegate : UITableViewDelegate {}
		public class PostHistoryTableViewDataSource : UITableViewDataSource {

			List<Post> posts;

			public PostHistoryTableViewDataSource(List<Post> posts)
			{
				this.posts = posts;
			}

			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return posts.Count;
			}
			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("postCell", indexPath);

				cell.Bounds = CoreGraphics.CGRect.FromLTRB(
					0,
					0,
					UIScreen.MainScreen.Bounds.Width,
					UIScreen.MainScreen.Bounds.Height * 0.5f
				);

				cell.BackgroundColor = UIColor.Red;
				cell.TextLabel.Text = posts[indexPath.Row].text;

				return cell;
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
}


