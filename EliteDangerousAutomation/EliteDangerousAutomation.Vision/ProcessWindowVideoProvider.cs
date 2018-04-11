using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Accord.Video;

namespace EliteDangerousAutomation.Vision
{
	internal class ProcessWindowVideoProvider
	{
		private readonly Process _process;

		public ProcessWindowVideoProvider(Int32 processId)
		{
			_process = Process.GetProcessById(processId);
		}

		public IVideoSource CaptureVideoSource()
		{
			var rect = new User32.Rect();
			User32.GetWindowRect(_process.MainWindowHandle, ref rect);

			return new ScreenCaptureStream(rect.ToRectangle());
		}

		private static class User32
		{
			[StructLayout(LayoutKind.Sequential)]
			public struct Rect
			{
				public readonly Int32 Left;
				public readonly Int32 Top;
				public readonly Int32 Right;
				public readonly Int32 Bottom;

				public Rectangle ToRectangle()
				{
					return new Rectangle(Left, Top, Right - Left, Bottom - Top);
				}
			}

			[DllImport("user32.dll")]
			public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
		}
	}
}