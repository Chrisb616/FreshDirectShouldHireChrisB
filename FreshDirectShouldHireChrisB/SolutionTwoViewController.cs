using System;
using System.Runtime;
using System.Collections.Generic;

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

		TwitterUserInfoView twitterUserInfoView = new TwitterUserInfoView();

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
			var BackgroundFrame = UIScreen.MainScreen.Bounds; 

			View.BackgroundColor = new UIColor(74f / 255f, 54f / 255f, 76f / 255f, 1f);

			View.AddSubview(usernameTextField);

			usernameTextField.BackgroundColor = UIColor.White;
			usernameTextField.Layer.CornerRadius = 3;
			usernameTextField.AutocorrectionType = UITextAutocorrectionType.No;
			usernameTextField.AutocapitalizationType = UITextAutocapitalizationType.None;
			usernameTextField.EditingChanged += HandleTextFieldDidChange;
			usernameTextField.Placeholder = "Please enter a twitter username";
			usernameTextField.Frame = CoreGraphics.CGRect.FromLTRB(
				BackgroundFrame.Width * 0.05f,
				BackgroundFrame.Height * 0.075f,
				BackgroundFrame.Width * 0.95f,
				BackgroundFrame.Height * 0.075f + 30f
			);

			View.AddSubview(searchButton);

			searchButton.BackgroundColor = UIColor.Clear;
			searchButton.SetTitle("Get History", UIControlState.Normal);
			searchButton.Frame = CoreGraphics.CGRect.FromLTRB(
				BackgroundFrame.Width * 0.25f,
				usernameTextField.Frame.Bottom + 10f,
				BackgroundFrame.Width * 0.75f,
				usernameTextField.Frame.Bottom + 35f
			);
			searchButton.Hidden = true;
			searchButton.TouchUpInside += SearchButtonTapped;

			View.AddSubview(twitterUserInfoView);

			twitterUserInfoView.Frame = CoreGraphics.CGRect.FromLTRB(
				usernameTextField.Frame.Left,
				searchButton.Frame.Bottom + 10f,
				usernameTextField.Frame.Right,
				searchButton.Frame.Bottom + 110f
			);
			twitterUserInfoView.InitialSetUp();
		}

		public void setUpTableView()
		{
			postHistoryTableView.Hidden = true;
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
			var downloadDelegate = (NSUrlSessionDelegate)new TwitterNetworkDelegate(HandleUserDataRetrieved,HandlePostDataRetrieved);
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
			NSOperationQueue.MainQueue.AddOperation( () =>
			{
				twitterUserInfoView.SetupForUser(user);
			});
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

		void HandleTextFieldDidChange(object sender, EventArgs e)
		{
			if (usernameTextField.Text == "")
			{
				searchButton.Hidden = true;
			}
			else {
				searchButton.Hidden = false;
			}
			Console.WriteLine("Did change");
		}
		void SearchButtonTapped(object sender, EventArgs e)
		{
			usernameTextField.Text = "";
			searchButton.Hidden = true;
		}

		public class PostHistoryTableViewDelegate : UITableViewDelegate {
		}
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


	}
}


