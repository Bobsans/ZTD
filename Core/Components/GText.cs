using SFML.Graphics;
using SFML.System;

namespace ZTD.Core.Components {

	public class GText : GComponent {

		private readonly Text text = new Text();

		/***** PROPERTIES *****/

		public uint CharacterSize {
			get { return text.CharacterSize; }
			set { text.CharacterSize = value; }
		}

		public Color Color {
			get { return text.Color; }
			set { text.Color = value; }
		}

		public string DisplayedString {
			get { return text.DisplayedString; }
			set { text.DisplayedString = value; }
		}

		public Font Font {
			get { return text.Font; }
			set { text.Font = value; }
		}

		public Vector2F Origin {
			get { return text.Origin; }
			set { text.Origin = value; }
		}

		public Vector2F Position {
			get { return text.Position; }
			set { text.Position = value; }
		}

		public float Rotation {
			get { return text.Rotation; }
			set { text.Rotation = value; }
		}

		public Vector2F Scale {
			get { return text.Scale; }
			set { text.Scale = value; }
		}

		public Text.Styles Style {
			get { return text.Style; }
			set { text.Style = value; }
		}

		public GText() { }

		public GText(string caption) {
			DisplayedString = caption;
		}

		public GText(string caption, uint characterSize, Vector2F position)
			: this(caption) {
			CharacterSize = characterSize;
			Position = position;
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			target.Draw(text, states);
		}

		public override void Update() { }

		public FloatRect GetBounds() {
			return text.GetGlobalBounds();
		}
	}
}