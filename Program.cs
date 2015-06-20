using System.Windows.Forms;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using ZTD.Game;

namespace ZTD {

	class Program {

		public static RenderWindow Window { get; set; }

		public static int Width { get; set; }
		public static int Height { get; set; }

		public static GameState State { get; set; }

		public static GameMenu Menu;
		public static GameLevel Level;

		static void Main() {
			State = GameState.Menu;
			Width = Screen.PrimaryScreen.Bounds.Width;
			Height = Screen.PrimaryScreen.Bounds.Height;

			Window = new RenderWindow(new VideoMode((uint)Width, (uint)Height), "ZTD", Styles.Fullscreen, new ContextSettings(32, 4, 4));
			Window.SetVerticalSyncEnabled(true);
			Window.SetFramerateLimit(60);
			Window.SetVisible(true);

			Window.Closed += (sender, args) => Window.Close();

			Window.MouseButtonPressed += (sender, e) => {
				Menu.OnMousePressed(sender, e);
				Level.World.OnMousePressed(sender, e);
			};
			Window.MouseWheelMoved += (sender, e) => Level.World.Map.OnMouseWheelMoved(sender, e);

			Window.KeyPressed += (sender, e) => {
				if(State == GameState.Menu)
					Menu.CallKeyEvent(e);
			};

			Menu = new GameMenu();
			Level = new GameLevel();

			Music back = new Music(Utils.GetFilePath("Assets\\back.ogg"));
			//back.Play();
			back.Pitch = 0.2f;

			while(Window.IsOpen) {
				Window.DispatchEvents();

				Window.Clear(Color.White);

				if(State == GameState.Menu) {
					Menu.Update();
					Window.Draw(Menu);
				} else {
					Level.Update();
					Window.Draw(Level);
				}
				
				Window.Display();
			}
		}
	}

	public enum GameState {
		Menu,
		Level
	}
}