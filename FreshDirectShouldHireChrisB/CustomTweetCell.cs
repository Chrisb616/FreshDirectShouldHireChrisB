using System;
using UIKit;
using Foundation;
namespace FreshDirectShouldHireChrisB
{
	public class CustomTweetCell : UITableViewCell
	{
		UILabel dateLabel = new UILabel();
		UILabel tweetLabel = new UILabel();
		UIView separatorView;

		public CustomTweetCell(NSString cellID) : base(UITableViewCellStyle.Default, cellID)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			ContentView.BackgroundColor = UIColor.Black;
			dateLabel = new UILabel()
			{
				Font = UIFont.FromName("HelveticaNeue", 12f)
			};
			tweetLabel = new UILabel()
			{
				Font = UIFont.FromName("HelveticaNeue", 16f)
			};
			separatorView = new UIView()
			{
				BackgroundColor = Colors.TwitterBlue 
			};
			ContentView.AddSubviews(new UIView[] { separatorView, dateLabel, tweetLabel });
		}
		public CustomTweetCell(IntPtr p) : base(p)
		{
		}
		public void UpdateCell(Post post)
		{
			Console.Write(post.text);
			dateLabel.Text = post.created_at;
			tweetLabel.Text = post.text;
		}
		override public void LayoutSubviews()
		{
			base.LayoutSubviews();
			dateLabel.Frame = new CoreGraphics.CGRect(0, 0, ContentView.Frame.Width, ContentView.Frame.Height);
		}
	}
}
