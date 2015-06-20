using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using ZTD.Core.Components;

namespace ZTD.Core.Controls {

	public class Button : GSprite {

		private string caption;
		private ButtonState state;

		private readonly Font font = new Font(Utils.GetFilePath("Assets\\Fonts\\Stencil.ttf"));
		private readonly Text text;

		public string Caption {
			get { return caption; }
			set {
				caption = value;
				if(text != null)
					text.DisplayedString = caption;
			}
		}

		public ButtonState State {
			get { return state; }
			set {
				state = value;
				TextureRect = new IntRect(0, (int)((int)State * Texture.Size.Y / 3), (int)Texture.Size.X, (int)(Texture.Size.Y / 3));
			}
		}

		/***** CONSTRUCTORS *****/

		public Button(string filename, string caption, Vector2F position, EventHandler<MouseButtonEventArgs> onClick) {
			Texture = new Texture(Utils.GetFilePath(filename));

			Position = position;
			State = ButtonState.Normal;

			Caption = caption;
			text = new Text(Caption, font, 64);
			text.Position = GetCenterPosition(GetBounds(), text.GetGlobalBounds());
			if(onClick != null)
				MousePressed += onClick;
		}

		public Button(string filename, string label, Vector2F position) : this(filename, label, position, null) { }



		public override void Update() {
			ButtonState ns = ButtonState.Normal;
			Vector2I pos = Program.Window.InternalGetMousePosition();

			if(GetBounds().Contains(pos.X, pos.Y))
				ns = Mouse.IsButtonPressed(Mouse.Button.Left) ? ButtonState.Pressed : ButtonState.Hovered;

			if(State != ns)
				State = ns;
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			base.Draw(target, states);
			target.Draw(text);
		}

		private static Vector2F GetCenterPosition(FloatRect src, FloatRect obj) {
			return new Vector2F(src.Left + (src.Width - obj.Width) / 2, src.Top + ((src.Height - obj.Height) / 2) - 20);
		}


		/***** EVENTS *****/

		public override void OnMousePressed(object sender, MouseButtonEventArgs e) {
			if(GetBounds().Contains(e.X, e.Y))
				base.OnMousePressed(this, e);
		}
	}

	public enum ButtonState {
		Normal = 0,
		Hovered = 1,
		Pressed = 2
	}
}
