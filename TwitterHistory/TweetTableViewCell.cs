using System;

using Foundation;
using UIKit;

namespace TwitterHistory
{
	public partial class TweetTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("TweetTableViewCell");
		public static readonly UINib Nib;

		UITextView TextView = new UITextView();
		UILabel DateLabel = new UILabel();
		UIView WhiteBackground = new UIView();


		public void UpdateCell(Tweet tweet)
		{
			ContentView.Layer.BorderWidth = 2f;
			ContentView.Layer.BorderColor = Constants.TwitterBlue.CGColor;


			TextView.Font = UIFont.FromName("HelveticaNeue", 12);
			TextView.ScrollEnabled = false;
			TextView.Editable = false;
			TextView.TextColor = UIColor.Black;
			TextView.BackgroundColor = UIColor.Clear;
			DateLabel.Font = UIFont.FromName("HelveticaNeue", 13);
			DateLabel.TextAlignment = UITextAlignment.Right;
			DateLabel.TextColor = UIColor.White;
			DateLabel.BackgroundColor = UIColor.Clear;

			WhiteBackground.BackgroundColor = UIColor.White;


			ContentView.AddSubviews(WhiteBackground,TextView, DateLabel);
			DateLabel.Text = tweet.created_at;
			TextView.Text = tweet.text;
		}

		static TweetTableViewCell()
		{
			Nib = UINib.FromName("TweetTableViewCell", NSBundle.MainBundle);

		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			WhiteBackground.Frame = new CoreGraphics.CGRect(0,22,ContentView.Frame.Width,ContentView.Frame.Height - 22);
			DateLabel.Frame = new CoreGraphics.CGRect(5, 5, ContentView.Frame.Width - 10, 17);
			TextView.Frame = new CoreGraphics.CGRect(0, 20, ContentView.Frame.Width, ContentView.Frame.Height - 20);
		}

		protected TweetTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	}
}
