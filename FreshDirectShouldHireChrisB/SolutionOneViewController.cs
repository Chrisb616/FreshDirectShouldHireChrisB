using System;

using UIKit;
using Foundation;

namespace FreshDirectShouldHireChrisB
{
	public partial class SolutionOneViewController : UIViewController
	{
		void HandleAction()
		{

		}

		public SolutionOneViewController() : base("SolutionOneViewController", null)
		{
		}
		public SolutionOneViewController(IntPtr ptr) : base (ptr)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SearchButton.TouchUpInside += (object sender, EventArgs e) => 
			{
				string username = UsernameTextField.Text;

				var webView = new UIWebView(WebViewContainer.Bounds);
				WebViewContainer.AddSubview(webView);

				var url = "https://twitter.com/" + username;
				webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
			};

			DismissButton.TouchUpInside += (object sender, EventArgs e) =>
			{
				DismissViewController(true, HandleAction);
			};

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

