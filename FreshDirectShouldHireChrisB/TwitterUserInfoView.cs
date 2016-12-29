using System;
using UIKit;
namespace FreshDirectShouldHireChrisB
{
	public class TwitterUserInfoView : UIView
	{
		UIImageView ProfilePictureImageView = new UIImageView();
		UIView UsernameTopDivider = new UIView();
		UILabel UsernameLabel = new UILabel();
		UILabel UserFullNameLabel = new UILabel();
		UILabel LocationLabel = new UILabel();
		UITextView DescriptionTextView = new UITextView();


		public void InitialSetUp()
		{
			UIColor TwitterBlue = new UIColor(85f / 255f, 172 / 255f, 238 / 255f,1);

			this.Layer.CornerRadius = 3;
			this.BackgroundColor = UIColor.White;


			this.AddSubview(UsernameTopDivider);
			UsernameTopDivider.BackgroundColor = TwitterBlue;
			UsernameTopDivider.Frame = CoreGraphics.CGRect.FromLTRB(
				0,
				0,
				this.Frame.Width,
				this.Frame.Height * 0.3f
			);
			this.AddSubview(ProfilePictureImageView);
			ProfilePictureImageView.BackgroundColor = UIColor.Red;
			ProfilePictureImageView.Frame = CoreGraphics.CGRect.FromLTRB(
				0,
				0,
				this.Frame.Height,
				this.Frame.Height
			);
			ProfilePictureImageView.Layer.CornerRadius = 3;

		}
		public void SetupForUser(User user)
		{
		}
	}
}
