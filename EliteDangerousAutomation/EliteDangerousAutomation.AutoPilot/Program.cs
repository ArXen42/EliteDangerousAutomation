using System;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;

namespace EliteDangerousAutomation.AutoPilot
{
	internal class Program
	{
		private static void Main(String[] args)
		{
			Mat img = new Mat("img.jpg");
			Mat detail = new Mat("detail.png");


			var result = DrawMatches.Draw(detail, img);

			result.Save("res.png");
		}
	}

	public static class DrawMatches
	{
		public static Mat Draw(Mat modelImage, Mat observedImage)
		{
			var sift = new SIFT();

			var modelKeyPoints = new VectorOfKeyPoint();
			var observedKeyPoints = new VectorOfKeyPoint();

			UMat modelDescriptors = new UMat();
			UMat observedDescriptors = new UMat();

			sift.DetectAndCompute(modelImage,    null, modelKeyPoints,    modelDescriptors,    false);
			sift.DetectAndCompute(observedImage, null, observedKeyPoints, observedDescriptors, false);

			BFMatcher matcher = new BFMatcher(DistanceType.L2);
			matcher.Add(modelDescriptors);

			var matches = new VectorOfVectorOfDMatch();
			matcher.KnnMatch(observedDescriptors, matches, 2, null);

			var mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
			mask.SetTo(new MCvScalar(255));
			Features2DToolbox.VoteForUniqueness(matches, 0.8, mask);
			Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);

			var homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, matches, mask, 10);

			var result = new Mat();
			Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints, matches, result,
				new MCvScalar(255, 255, 255),
				new MCvScalar(0,   0,   0),
				mask,
				Features2DToolbox.KeypointDrawType.NotDrawSinglePoints);

			Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
			PointF[] pts =
			{
				new PointF(rect.Left,  rect.Bottom),
				new PointF(rect.Right, rect.Bottom),
				new PointF(rect.Right, rect.Top),
				new PointF(rect.Left,  rect.Top)
			};
			pts = CvInvoke.PerspectiveTransform(pts, homography);

			Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
			using (VectorOfPoint vp = new VectorOfPoint(points))
			{
				CvInvoke.Polylines(result, vp, true, new MCvScalar(0,255,0,55), 2);
			}

			return result;
		}
	}
}