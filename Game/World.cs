using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using ZTD.Core.Components;
using ZTD.Core.Types;
using ZTD.Game.WorldBase;

namespace ZTD.Game {

	class World : GComponent {

		public Map Map { get; set; }

		public World() {
			Map = new Map();

			Random r = new Random();
			for(int i = 0; i < 10; i++) {
				Vector2F pos1 = new Vector2F(r.Next((int)Map.Background.GetBounds().Width), r.Next((int)Map.Background.GetBounds().Height));
				Vector2F pos2 = new Vector2F(r.Next((int)Map.Background.GetBounds().Width), r.Next((int)Map.Background.GetBounds().Height));
				Map.StaticObjects.Add(new GSprite("Assets\\Images\\rock-1.png") { Position = pos1 });
				Map.StaticObjects.Add(new GSprite("Assets\\Images\\rock-2.png") { Position = pos2 });
				Map.DynamicObjects.Add(new GAnimatedSprite("Assets/Images/explosion.png", new Vector2I(128, 128), 49, 7, 0.01f) { Position = new Vector2F(pos1.X - 10, pos1.Y - 10) });
				Map.DynamicObjects.Add(new GAnimatedSprite("Assets/Images/explosion.png", new Vector2I(128, 128), 49, 7, 0.01f) { Position = pos2 });
			}

			Map.DynamicObjects.Add(new Entity(Utils.GetFilePath("Assets\\Images\\button.png"), 100, false, new Vector2F(100, 100)));
			// Map.AddDynamicObject(new GAnimatedSprite("Assets/Images/explosion.png", new Vector2I(128, 128), 49, 7, 0.05f) { Position = new Vector2F(1000, 1000) });
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			Map.Draw(target, states);
		}

		public override void Update() {
			Map.Update();
			foreach(GComponent o in Map.DynamicObjects) {
				if(o is Turrent) {
					((Turrent)o).Target = Map.GetPosOnMap(Mouse.GetPosition().X, Mouse.GetPosition().Y);
				}
			}
		}

		public override void OnMousePressed(object sender, MouseButtonEventArgs e) {
			Vector2F pos = Map.GetPosOnMap(e.X, e.Y);
			Map.DynamicObjects.Add(new Turrent("Assets/Images/turret-2.png", new Vector2I(64, 64), 36, 12) { Position = new Vector2F(pos.X - 32, pos.Y - 32) });
		}
	}
}