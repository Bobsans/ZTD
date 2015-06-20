using SFML.Graphics;
using SFML.System;

namespace ZTD.Core.Components {

	internal class GAnimatedSprite : GSprite {

		private int currentFrame;

		public Vector2I FrameSize { get; set; }
		public int FrameCount { get; set; }
		public float Speed { get; set; }
		public int FramesInLine { get; set; }

		public GAnimatedSprite(string filename, Vector2I framesize, int framecount, int framesinline, float speed)
			: base(filename) {
			FrameSize = framesize;
			FrameCount = framecount;
			Speed = speed;
			TextureRect = new IntRect(0, 0, framesize.X, FrameSize.Y);
			FramesInLine = framesinline;
		}

		readonly Clock clock = new Clock();

		public override void Update() {
			if(clock.ElapsedTime.AsSeconds() >= Speed) {
				currentFrame = (currentFrame == FrameCount - 1) ? 0: currentFrame + 1;
				TextureRect = new IntRect((currentFrame % FramesInLine) * FrameSize.X, (currentFrame / FramesInLine) * FrameSize.Y, FrameSize.X, FrameSize.Y);
				clock.Restart();
			}
		}
	}
}
