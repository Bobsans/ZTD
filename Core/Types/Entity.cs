using SFML.Graphics;
using SFML.System;
using ZTD.Core.Components;

namespace ZTD.Core.Types {

	class Entity : GSprite {

		readonly GText Caption;

		public bool Destroyable { get; set; }
		public int HealthPoints { get; set; }

		public Entity(string imageFile, int hp, bool destroyable, Vector2F pos) {
			Texture = new Texture(Utils.GetFilePath(imageFile));
			Position = pos;
			HealthPoints = hp;
			Destroyable = destroyable;

			Caption = new GText(hp.ToString()) { Font = new Font(Utils.GetFilePath("Assets\\Fonts\\Stencil.ttf")), CharacterSize = 30 };
			FloatRect b = Caption.GetBounds();
			Caption.Position = new Vector2F(Position.X + ((Texture.Size.X - b.Width) / 2), Position.Y - 50);
		}

		public override void Draw(IRenderTarget target, RenderStates states) {
			base.Draw(target, states);
			target.Draw(Caption);
		}
	}
}
