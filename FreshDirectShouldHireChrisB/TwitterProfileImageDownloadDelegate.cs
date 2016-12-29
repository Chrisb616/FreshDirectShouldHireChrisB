using System;
using Foundation;
using UIKit;
namespace FreshDirectShouldHireChrisB
{
	public class TwitterProfileImageDownloadDelegate : NSUrlSessionDownloadDelegate
	{
		public delegate void ImageDownloaded(UIImage ProfileImage);
		ImageDownloaded ImageDownloadedDelegate;

		public TwitterProfileImageDownloadDelegate(ImageDownloaded imageHandle)
		{
			ImageDownloadedDelegate = imageHandle;
		}

		public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			NSData data = NSFileManager.DefaultManager.Contents(location.Path);
			var image = UIImage.LoadFromData(data);
			ImageDownloadedDelegate(image);
		}


	}
}
