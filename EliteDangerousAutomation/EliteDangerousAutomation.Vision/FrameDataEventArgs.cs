using System;

namespace EliteDangerousAutomation.Vision {
	internal class FrameDataEventArgs : EventArgs
	{
		public readonly FrameData NewFrameData;

		public FrameDataEventArgs(FrameData newFrameData)
		{
			NewFrameData = newFrameData;
		}
	}
}