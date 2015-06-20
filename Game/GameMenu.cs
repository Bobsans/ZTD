using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using ZTD.Core;
using ZTD.Core.Components;
using ZTD.Core.Controls;

namespace ZTD.Game {

	class GameMenu : GSprite {

		private readonly Sprite[] particles = new Sprite[200];

		public readonly List<GComponent> components = new List<GComponent>();

		public GameMenu() {
			Texture = new Texture("Assets\\Images\\zombies.jpg");
			Color = new Color(100, 0, 0);
			Scale = new Vector2F((float)Program.Width / Texture.Size.X, (float)Program.Height / Texture.Size.Y);

			components.Add(new Button("Assets\\Images\\button.png", "START", new Vector2F(10, Program.Height - 330), (sender, e) => Program.State = GameState.Level) { Color = new Color(180, 20, 20) });
			components.Add(new Button("Assets\\Images\\button.png", "OPTIONS", new Vector2F(10, Program.Height - 220)) { Color = new Color(180, 20, 20) });
			components.Add(new Button("Assets\\Images\\button.png", "EXIT", new Vector2F(10, Program.Height - 110), (button, e) => Program.Window.Close()) { Color = new Color(180, 20, 20) });
			components.Add(new GText("ZTD", 360, new Vector2F(10, 10)) { Color = new Color(255, 60, 60, 200), Rotation = -10, Font = new Font(Utils.GetFilePath("Assets\\Fonts\\Stencil.ttf")) });

			Random r = new Random();
			for(int i = 0; i < particles.Length; i++) {
				float scale = (float)r.NextDouble();

				particles[i] = new Sprite("Assets\\Images\\particle.png") {
					Color = new Color(255, 0, 0, (byte)r.Next(200)),
					Scale = new Vector2F(scale, scale),
					Position = new Vector2F(r.Next((int)Program.Window.Size.X), r.Next((int)Program.Window.Size.Y)),
				};
			}
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			base.Draw(target, states);

			foreach(Sprite d in particles)
				Program.Window.Draw(d);
			foreach(GComponent c in components)
				Program.Window.Draw(c);
		}

		public override void Update() {
			foreach(GComponent c in components)
				c.Update();

			foreach(Sprite d in particles) {
				Vector2F pos = d.Position;
				if(pos.X > Program.Window.Size.X)
					pos.X = 0;
				else
					pos.X += d.Scale.X;
				if(pos.Y > Program.Window.Size.Y)
					pos.Y = 0;
				else
					pos.Y += d.Scale.Y;
				d.Position = pos;
			}
		}

		public void CallKeyEvent(KeyEventArgs e) {
			if(e.Code == Keyboard.Key.Escape)
				Program.Window.Close();
		}

		public override void OnMousePressed(object sender, MouseButtonEventArgs e) {
			foreach(GComponent c in components)
				c.OnMousePressed(c, e);
		}
	}
}
