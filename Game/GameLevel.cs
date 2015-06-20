using SFML.Graphics;
using SFML.Window;
using ZTD.Core.Components;

namespace ZTD.Game {

	class GameLevel : GComponent {

		public World World { get; set; }

		public GameLevel() {
			World = new World();
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			World.Draw(target, states);
		}

		public override void Update() {
			if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
				Program.State = GameState.Menu;

			World.Update();
		}
	}
}
