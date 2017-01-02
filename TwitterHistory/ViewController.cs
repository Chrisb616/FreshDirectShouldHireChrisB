using System;
using System.Runtime;
using System.Collections.Generic;

using UIKit;
using Foundation;

namespace TwitterHistory
{
	public partial class ViewController : UIViewController
	{
		UITextField usernameTextField = new UITextField();
		UIButton searchButton = new UIButton();
		TwitterUserInfoView twitterUserInfoView = new TwitterUserInfoView();
		UILabel errorLabel = new UILabel();

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			setUpViews();
		}

		public void setUpViews()
		{
			var BackgroundFrame = UIScreen.MainScreen.Bounds;

			View.BackgroundColor = Constants.Purple;

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

			View.AddSubview(errorLabel);

			errorLabel.Frame = new CoreGraphics.CGRect(
				0,
				BackgroundFrame.Height * 0.5,
				BackgroundFrame.Width,
				BackgroundFrame.Height * 0.5
			);
			errorLabel.Text = "Uh oh! Either this user does not exist, or they've set their settings to private. Try again with another name!";
			errorLabel.Lines = 6;
			errorLabel.TextColor = UIColor.White;
			errorLabel.TextAlignment = UITextAlignment.Center;
			errorLabel.Hidden = true;

			TweetTableView.Hidden = true;
			TweetTableView.RowHeight = 100f;
		}

		void SearchButtonTapped(object sender, EventArgs e)
		{
			searchButton.Hidden = true;
			getTwitterHistory();
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
		}
		public void getTwitterHistory()
		{
			var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("com.SimpleBackgroundTransfer.BackgroundSession");
			var downloadDelegate = (NSUrlSessionDelegate)new TwitterNetworkDelegate(HandleUserDataRetrieved, HandleTweetDataRetrieved, HandleProfileImageDownloaded,HandleDidRecieveError);
			var session = NSUrlSession.FromConfiguration(config, downloadDelegate, new NSOperationQueue());
			var url = NSUrl.FromString("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=" + usernameTextField.Text);
			var request = new NSMutableUrlRequest(url);


			NSString key = new NSString("Authorization");
			NSString authentication = new NSString("Bearer AAAAAAAAAAAAAAAAAAAAAD33yQAAAAAAdN75U4ZxRRjPhruc7laPrdkz8Vc%3D4igSgnqa9FHA6GSlFpngaR809UWDuCgDb296mM86Om5P55a3Gh");


			var headers = NSDictionary.FromObjectAndKey(authentication, key);

			request.Headers = headers;

			var downloadTask = session.CreateDownloadTask(request);
			downloadTask.Resume();
		}
		void HandleUserDataRetrieved(User user)
		{
			NSOperationQueue.MainQueue.AddOperation(() =>
		   {
			   twitterUserInfoView.SetupForUser(user);
		   });
			return;
		}
		void HandleTweetDataRetrieved(List<Tweet> tweets)
		{
			NSOperationQueue.MainQueue.AddOperation(() =>
			{
				TweetTableView.DataSource = new TweetTableViewDataSource(tweets);
				TweetTableView.ReloadData();
				TweetTableView.Hidden = false;
				errorLabel.Hidden = true;
			});
		}
		void HandleProfileImageDownloaded(UIImage profileImage)
		{
			twitterUserInfoView.UpdateProfileImage(profileImage);
		}

		public class TweetTableViewDataSource : UITableViewDataSource
		{
			List<Tweet> tweets;

			public TweetTableViewDataSource(List<Tweet> tweets)
			{
				this.tweets = tweets;
			}

			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return tweets.Count;
			}
			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("tweetCell") as TweetTableViewCell;

				cell.UpdateCell(tweets[indexPath.Row]);

				return cell;
			}
		}

		void HandleDidRecieveError()
		{
			NSOperationQueue.MainQueue.AddOperation(() =>
		   {
			   TweetTableView.Hidden = true;
			   errorLabel.Hidden = false;
			   twitterUserInfoView.clearInfo();
		   });
		}
	}
}
