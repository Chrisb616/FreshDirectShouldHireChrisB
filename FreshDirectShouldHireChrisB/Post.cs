using System;
using System.Collections.Generic;

namespace FreshDirectShouldHireChrisB
{
	public class Post
	{
		public string text { get; set; }
		public string created_at { get; set; }



		public static List<Post> Dummy
		{
			get
			{
				var DummyData = new List<Post>();

				var post = new Post();
				post.text = "Just rolled into the shop";
				post.created_at = "June 16 1990 03:45PM";

				DummyData.Add(post);

				var anotherPost = new Post();
				anotherPost.text = "Can we all just get along?";
				anotherPost.created_at = "Nov 9 2020 05:33PM";

				DummyData.Add(anotherPost);

				var yetAnotherPost = new Post();
				yetAnotherPost.text = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
				yetAnotherPost.created_at = "Dec 1 2021 11:14AM";

				DummyData.Add(yetAnotherPost);

				return DummyData;
			}
		}
	}

}
