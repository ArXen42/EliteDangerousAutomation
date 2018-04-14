using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace EliteDangerousAutomation.Vision
{
	public static class FrameAnalysis
	{
		public static Image<Gray, Byte> GetAdaptiveThresholdedFrame(Image<Bgr, Byte> frame)
		{
			var result = new Mat();
			CvInvoke.CvtColor(frame, result, ColorConversion.Bgr2Gray);
			CvInvoke.AdaptiveThreshold(result, result, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 15, -10);

			return result.ToImage<Gray, Byte>();
		}

		public static Image<Gray, Byte> GetStarThresholdedFrame(Image<Bgr, Byte> frame)
		{
			var src = frame.Mat;
			//CvInvoke.CvtColor(frame, result, ColorConversion.Bgr2Gray);
			Mat p = Mat.Zeros(src.Cols , src.Rows, DepthType.Cv32F, 1);
			Mat bestLabels, centers, clustered;
			VectorOfMat bgr = new VectorOfMat();
			CvInvoke.Split(src, bgr);
			// i think there is a better way to split pixel bgr color
			/*for (int i = 0; i < src.Cols * src.Rows; i++)
			{
				p.at<float>(i, 0) = (i / src.cols) / src.rows;
				p.at<float>(i, 1) = (i % src.cols) / src.cols;
				p.at<float>(i, 2) = bgr[0].data[i] / 255.0;
				p.at<float>(i, 3) = bgr[1].data[i] / 255.0;
				p.at<float>(i, 4) = bgr[2].data[i] / 255.0;
			}

			int K = 8;
			cv::kmeans(p, K, bestLabels,
				TermCriteria(CV_TERMCRIT_EPS + CV_TERMCRIT_ITER, 10, 1.0),
				3, KMEANS_PP_CENTERS, centers);

			int colors[K];
			for (int i = 0; i < K; i++)
			{
				colors[i] = 255 / (i + 1);
			}
			// i think there is a better way to do this mayebe some Mat::reshape?
			clustered = Mat(src.rows, src.cols, CV_32F);
			for (int i = 0; i < src.cols * src.rows; i++)
			{
				clustered.at<float>(i / src.cols, i % src.cols) = (float)(colors[bestLabels.at<int>(0, i)]);
				//      cout << bestLabels.at<int>(0,i) << " " << 
				//              colors[bestLabels.at<int>(0,i)] << " " << 
				//              clustered.at<float>(i/src.cols, i%src.cols) << " " <<
				//              endl;
			}

			clustered.convertTo(clustered, CV_8U);
			imshow("clustered", clustered);

			waitKey();
			return 0; */

			throw new NotImplementedException();
		}
	}
}