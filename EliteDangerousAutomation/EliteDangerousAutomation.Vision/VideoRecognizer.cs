using System;
using Accord.Video;

namespace EliteDangerousAutomation.Vision
{
	/// <summary>
	///     Provides frames recognition.
	/// </summary>
	internal class VideoRecognizer
	{
		private readonly ProcessWindowVideoProvider _videoProvider;
		private IVideoSource _videoSource;

		public VideoRecognizer(ProcessWindowVideoProvider videoProvider)
		{
			_videoProvider = videoProvider;
		}

		public event EventHandler<FrameDataEventArgs> FrameDataReceived;

		public void StartCapture()
		{
			_videoSource = _videoProvider.CaptureVideoSource();
			_videoSource.Start();
			_videoSource.NewFrame += OnNewFrame;
		}

		private void OnNewFrame(Object sender, NewFrameEventArgs eventArgs)
		{
			var frameDataEventArgs = new FrameDataEventArgs(FrameData.FromFrameBitmap(eventArgs.Frame));
			FrameDataReceived?.Invoke(this, frameDataEventArgs);
		}
	}
}