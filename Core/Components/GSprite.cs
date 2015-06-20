using System.IO;
using SFML.Graphics;
using SFML.System;

namespace ZTD.Core.Components {

	public class GSprite : GComponent {

		private readonly Sprite sprite = new Sprite();

		/***** PROPERTIES *****/

		public Color Color {
			get { return sprite.Color; }
			set { sprite.Color = value; }
		}

		public Vector2F Origin {
			get { return sprite.Origin; }
			set { sprite.Origin = value; }
		}

		public Vector2F Position {
			get { return sprite.Position; }
			set { sprite.Position = value; }
		}

		public float Rotation {
			get { return sprite.Rotation; }
			set { sprite.Rotation = value; }
		}

		public Vector2F Scale {
			get { return sprite.Scale; }
			set { sprite.Scale = value; }
		}

		public Texture Texture {
			get { return sprite.Texture; }
			set { sprite.Texture = value; }
		}

		public IntRect TextureRect {
			get { return sprite.TextureRect; }
			set { sprite.TextureRect = value; }
		}

		public GSprite() { }

		public GSprite(string filename) : this(filename, new Vector2F(0, 0)) { }

		public GSprite(string filename, Vector2F position) {
			if(File.Exists(filename))
				Texture = new Texture(filename);
			else if(File.Exists(Utils.GetFilePath(filename)))
				Texture = new Texture(Utils.GetFilePath(filename));
			else
				throw new FileNotFoundException("File not found: " + filename);
			Position = position;
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			target.Draw(sprite, states);
		}

		public override void Update() { }

		public FloatRect GetBounds() {
			return sprite.GetGlobalBounds();
		}
	}
}