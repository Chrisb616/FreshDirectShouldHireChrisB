using System;

using UIKit;
using Foundation;

namespace FreshDirectShouldHireChrisB
{
	public partial class SolutionOneViewController : UIViewController
	{
		UITextField UserNameTextField;
		UIView WebViewContainmentView;
		UIWebView WebView;
		UIButton SearchButton;
		UIButton DismissButton;

		public SolutionOneViewController() : base("SolutionOneViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.Black;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			LayoutSubviews();

		}
		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private void LayoutSubviews()
		{
			View.BackgroundColor = UIColor.Black;

			UserNameTextField = new UITextField();
			UserNameTextField.BackgroundColor = UIColor.White;
			UserNameTextField.Layer.CornerRadius = 2;

			View.AddSubview(UserNameTextField);

			UserNameTextField.Frame = CoreGraphics.CGRect.FromLTRB(
				10f,//View.Frame.Right * 0.05f,
				20f,
				View.Frame.Width - 10f,
				50f
			);

			SearchButton = new UIButton();
			SearchButton.SetTitle("Search", UIControlState.Normal);
			SearchButton.TouchUpInside += SearchButton_TouchUpInside;

			View.AddSubview(SearchButton);

			SearchButton.Frame = CoreGraphics.CGRect.FromLTRB(
				40f,
				50f,
				View.Frame.Width - 40f,
				90f
			);

			WebViewContainmentView = new UIView();
			WebViewContainmentView.BackgroundColor = UIColor.Clear;
			View.AddSubview(WebViewContainmentView);
			WebViewContainmentView.Frame = CoreGraphics.CGRect.FromLTRB(
				0f,
				SearchButton.Frame.Bottom,
				View.Frame.Width,
				View.Frame.Height * 0.8f
			);

			DismissButton = new UIButton();
			DismissButton.SetTitle("Clever, but you can do better. ", UIControlState.Normal);
			DismissButton.TouchUpInside += DismissButton_TouchUpInside;
			DismissButton.Hidden = true;

			View.AddSubview(DismissButton);

			DismissButton.Frame = CoreGraphics.CGRect.FromLTRB(
				0,
				WebViewContainmentView.Frame.Bottom,
				View.Frame.Width,
				View.Frame.Bottom
			);

		}

		void WebView_LoadFinished(object sender, EventArgs e)
		{
			DismissButton.Hidden = false;
		}

		void SearchButton_TouchUpInside(object sender, EventArgs e)
		{
			WebView = new UIWebView(WebViewContainmentView.Bounds);
			WebViewContainmentView.AddSubview(WebView);
			var url = "https://twitter.com/" + UserNameTextField.Text;
			WebView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
			WebView.LoadFinished += WebView_LoadFinished;
		}


		void DismissButton_TouchUpInside(object sender, EventArgs e)
		{
			DismissViewController(
				true,DismissedViewController
			);
		}

		void DismissedViewController()
		{

		}
	}
}

