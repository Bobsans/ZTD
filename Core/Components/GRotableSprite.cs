using System;
using SFML.Graphics;
using SFML.System;

namespace ZTD.Core.Components {

	class GRotableSprite : GSprite {

		private float angle;
		private int currentFrame;

		public Vector2I FrameSize { get; set; }
		public int FrameCount { get; set; }
		public int FramesInLine { get; set; }

		public float Angle {
			get { return angle; }
			set {
				angle = value % 360;
				UpdateTexture();
			}
		}

		public GRotableSprite(string filename, Vector2I framesize, int framecount, int framesinline)
			: base(filename) {
			FrameSize = framesize;
			FrameCount = framecount;
			TextureRect = new IntRect(0, 0, framesize.X, FrameSize.Y);
			FramesInLine = framesinline;
		}

		private void UpdateTexture() {
			currentFrame = (int)(angle / (360 / FrameCount));
			TextureRect = new IntRect((currentFrame % FramesInLine) * FrameSize.X, (currentFrame / FramesInLine) * FrameSize.Y, FrameSize.X, FrameSize.Y);;
		}
	}
}
