using System;
using MoreMountains.Feedbacks;

namespace Metro
{
	[Serializable]
	public class FeedbacksData
	{
		public MMFeedbacks JumpFeedbacks;
		public MMFeedbacks FallFeedbacks;
		public MMFeedbacks WallSlideFeedbacks;
		public MMFeedbacks WallJumpFeedbacks;
		public MMFeedbacks IdleGroundedFeedbacks;
		public MMFeedbacks MoveGroundedFeedbacks;
		public MMFeedbacks DashFeedbacks;
	}
}