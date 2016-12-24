using System;

using UIKit;
using Foundation;

namespace FreshDirectShouldHireChrisB
{
	public partial class SolutionTwoViewController : UIViewController
	{
		

		public SolutionTwoViewController() : base("SolutionTwoViewController", null)
		{
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Console.WriteLine("View Did Load");
			getApps();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		public void getApps()
		{
			var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("com.SimpleBackgroundTransfer.BackgroundSession");
			var session = NSUrlSession.FromConfiguration(config, (NSUrlSessionDelegate)new NetworkDelegate(), new NSOperationQueue());
			var url = NSUrl.FromString("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=cboynton16&count=2");
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

		public class NetworkDelegate : NSUrlSessionDownloadDelegate
		{
			public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
			{
				//solution1
				NSData data = NSFileManager.DefaultManager.Contents(location.Path);
				NSString dataString = NSString.FromData(data, NSStringEncoding.UTF8);
				Console.WriteLine(dataString);



		}

		}
	}
}


