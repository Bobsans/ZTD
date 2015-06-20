using System;
using SFML.Graphics;
using SFML.System;
using ZTD.Core.Components;

namespace ZTD.Core.Types {

	class Turrent : GRotableSprite {

		private CircleShape radiusOverlay;

		public Vector2F Target { get; set; }
		public float ShotingRadius { get; set; }
		public bool IsOverlayShow { get; set; }

		public Turrent(string filename, Vector2I framesize, int framecount, int framesinline) : base(filename, framesize, framecount, framesinline) { }

		public override void Update() {
			UpdateRotation();
		}

		public void UpdateRotation() {
			Vector2F v1 = new Vector2F(Position.X + FrameSize.X / 2, Position.Y + FrameSize.Y / 2 - Target.Y);
			Vector2F v2 = new Vector2F(Position.X + FrameSize.X / 2 - Target.X, Position.Y + FrameSize.Y / 2 - Target.Y);
			double ca = (v1.X * v2.X + v1.Y * v2.Y) / Math.Sqrt((v1.X * v1.X + v1.Y * v1.Y) * (v2.X * v2.X + v2.Y * v2.Y));
			float ang = (float)(Math.Acos(ca) / (2 * Math.PI) * 360);
			Angle = Target.Y >= (Position.Y + FrameSize.Y / 2) ? 360 - ang + 270 : ang + 270;
		}
	}
}