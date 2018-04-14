using System.Drawing;

namespace EliteDangerousAutomation.Vision
{
	/// <summary>
	///     Contains data retrieved from frame.
	/// </summary>
	internal struct FrameData
	{
		/// <summary>
		///     Perform frame analysis and puts result into provided <see cref="destinationFrameData" />.
		/// </summary>
		/// <param name="frame">Frame bitmap.</param>
		/// <param name="destinationFrameData">Destination frame data.</param>
		public static void FromFrameBitmap(Bitmap frame, ref FrameData destinationFrameData) { }

		/// <summary>
		///     Perform frame analysis and returns resulting frame data.
		/// </summary>
		/// <param name="frame">Frame bitmap.</param>
		public static FrameData FromFrameBitmap(Bitmap frame)
		{
			var data = new FrameData();
			FromFrameBitmap(frame, ref data);

			return data;
		}
	}
}