// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FreshDirectShouldHireChrisB
{
    [Register ("SolutionOneViewController")]
    partial class SolutionOneViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DismissButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SearchButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UsernameTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView WebViewContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DismissButton != null) {
                DismissButton.Dispose ();
                DismissButton = null;
            }

            if (SearchButton != null) {
                SearchButton.Dispose ();
                SearchButton = null;
            }

            if (UsernameTextField != null) {
                UsernameTextField.Dispose ();
                UsernameTextField = null;
            }

            if (WebViewContainer != null) {
                WebViewContainer.Dispose ();
                WebViewContainer = null;
            }
        }
    }
}