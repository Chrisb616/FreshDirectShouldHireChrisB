using System;
using UIKit;

namespace FreshDirectShouldHireChrisB
{
	public class PostTableViewCell : UITableViewCell
	{
		public Post post { get; set; }

		UILabel tweetLabel = new UILabel();

		public PostTableViewCell(IntPtr placeholder)
		{
		}

		public void LayoutCell()
		{
			tweetLabel.Frame = this.ContentView.Frame;
			tweetLabel.Lines = 3;
			tweetLabel.Font = UIFont.FromName("HelveticaNeue", 12);
		}
		public void UpdateForPost()
		{
			tweetLabel.Text = post.text;
		}
	}
}
