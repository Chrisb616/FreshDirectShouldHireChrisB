using System;

using UIKit;

namespace FreshDirectShouldHireChrisB
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			LayoutSubviews();
		}

		public void LayoutSubviews()
		{
			var Button = new UIButton();

			Button.BackgroundColor = UIColor.Blue;

			View.AddSubview(Button);

			Button.Frame = CoreGraphics.CGRect.FromLTRB(
				View.Frame.Right * 0.1f,
				View.Frame.Bottom * 0.2f,
				View.Frame.Right * 0.9f,
				View.Frame.Bottom * 0.3f
			);

			Button.TouchUpInside += Button_TouchUpInside;

		}

		void Button_TouchUpInside(object sender, EventArgs e)
		{
			Console.WriteLine("Button Tapped");

			var dest = new SolutionOneViewController();

			PresentViewController(
				dest,
				true,
				HandleAction
			);
		}

		void HandleAction()
		{

		}
	}
}
