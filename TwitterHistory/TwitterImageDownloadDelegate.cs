using System;
using Foundation;
using UIKit;
namespace TwitterHistory
{
	public class TwitterImageDownloadDelegate : NSUrlSessionDownloadDelegate
	{

		public delegate void ImageDownloaded(UIImage ProfileImage);
		ImageDownloaded ImageDownloadedDelegate;

		public TwitterImageDownloadDelegate(ImageDownloaded imageHandle)
		{
			ImageDownloadedDelegate = imageHandle;
		}

		public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			Console.WriteLine("DidFinishDownloading");
			NSData data = NSFileManager.DefaultManager.Contents(location.Path);
			var image = UIImage.LoadFromData(data);
			ImageDownloadedDelegate(image);
		}
	}
}
