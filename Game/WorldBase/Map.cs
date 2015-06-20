using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using ZTD.Core.Components;

namespace ZTD.Game.WorldBase {

	class Map : GSprite {

		public const int GRID_SIZE = 64;
		public const int GRID_COUNT = 32;

		public RenderTexture RTexture;
		public float GlobScale = 2;

		public Vector2F Offset { get; set; }
		public GSprite Background { get; set; }

		public List<GComponent> StaticObjects = new List<GComponent>();
		public List<GComponent> DynamicObjects = new List<GComponent>();

		public Map() {
			Background = new GSprite(Utils.GetFilePath("Assets\\Images\\grass.gif"));
			Offset = new Vector2F(-(Background.GetBounds().Width / 2), -(Background.GetBounds().Height / 2));

			RTexture = new RenderTexture((uint)Background.GetBounds().Width, (uint)Background.GetBounds().Width);
		}


		public void Prerender() {
			RTexture.Draw(Background);
			foreach(GComponent s in StaticObjects)
				RTexture.Draw(s);
			foreach(GComponent s in DynamicObjects)
				RTexture.Draw(s);
			RTexture.Display();
			Texture = RTexture.Texture;
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			Prerender();
			base.Draw(target, states);
		}

		public override void Update() {
			if(Keyboard.IsKeyPressed(Keyboard.Key.W))
				Offset = new Vector2F(Offset.X, Offset.Y + 5);
			if(Keyboard.IsKeyPressed(Keyboard.Key.S))
				Offset = new Vector2F(Offset.X, Offset.Y - 5);
			if(Keyboard.IsKeyPressed(Keyboard.Key.A))
				Offset = new Vector2F(Offset.X + 5, Offset.Y);
			if(Keyboard.IsKeyPressed(Keyboard.Key.D))
				Offset = new Vector2F(Offset.X - 5, Offset.Y);

			Position = new Vector2F((float)Program.Width / 2 + (Offset.X * GlobScale), (float)Program.Height / 2 + (Offset.Y * GlobScale));
			Scale = new Vector2F(GlobScale, GlobScale);

			foreach(GComponent o in DynamicObjects)
				o.Update();
		}

		public Vector2F GetGridPos(int x, int y) {
			return new Vector2F(x * GRID_SIZE, y * GRID_SIZE);
		}

		public Vector2F GetPosOnMap(int x, int y) {
			return new Vector2F((x - Position.X) / GlobScale, (y - Position.Y) / GlobScale);
		}

		public override void OnMouseWheelMoved(object sender, MouseWheelEventArgs e) {
			base.OnMouseWheelMoved(sender, e);
			float newrts = GlobScale + ((e.Delta < 0 ? -1 : 1) * (GlobScale / 10));
			GlobScale = newrts <= 0.5f ? 0.5f : (newrts >= 5.0f ? 5.0f : newrts);
		}
	}
}