using System;
using SFML.Graphics;
using SFML.System;

namespace ZTD.Core {

	public abstract class Effect : IDrawable {
		public IDrawable Drawable { get; set; }
		public Shader Shader { get; set; }

		public void Update(float time, float x, float y) {
			if(Shader.IsAvailable)
				OnUpdate(time, x, y);
		}

		public void Draw(IRenderTarget target, RenderStates states) {
			if(Shader.IsAvailable)
				OnDraw(target, states);
		}

		public abstract void OnUpdate(float time, float x, float y);
		public abstract void OnDraw(IRenderTarget target, RenderStates states);
	}


	#region EFFECTS

	public class Pixelate : Effect {

		public Pixelate(IDrawable drawable) {
			Drawable = drawable;
			Shader = new Shader(null, Utils.GetFilePath("Assets\\Resources\\Shaders\\pixelate.frag"));
			Shader.SetParameter("texture", Shader.CurrentTexture);
		}

		public override void OnUpdate(float time, float x, float y) {
			Shader.SetParameter("pixel_threshold", (x + y) / 30);
		}

		public override void OnDraw(IRenderTarget target, RenderStates states) {
			states = new RenderStates(states) { Shader = Shader };
			target.Draw(Drawable, states);
		}
	}

	public class WaveBlur : Effect {

		public WaveBlur(IDrawable drawable) {
			Drawable = drawable;
			Shader = new Shader(Utils.GetFilePath("Assets\\Resources\\Shaders\\wave.vert"), Utils.GetFilePath("Assets\\Resources\\Shaders\\blur.frag"));
		}

		public override void OnUpdate(float time, float x, float y) {
			Shader.SetParameter("wave_phase", time);
			Shader.SetParameter("wave_amplitude", x * 40, y * 40);
			Shader.SetParameter("blur_radius", (x + y) * 0.008F);
		}

		public override void OnDraw(IRenderTarget target, RenderStates states) {
			states = new RenderStates(states) { Shader = Shader };
			target.Draw(Drawable, states);
		}
	}

	public class Blur : Effect {

		public Blur(IDrawable drawable) {
			Drawable = drawable;
			Shader = new Shader(null, Utils.GetFilePath("Assets\\Resources\\Shaders\\gaus.frag"));
		}

		public override void OnUpdate(float time, float x, float y) {
			Shader.SetParameter("time", time);
			Shader.SetParameter("mouse", new Vector2F(x, y));
			Shader.SetParameter("resolution", new Vector2F(x, y));
			
		}

		public override void OnDraw(IRenderTarget target, RenderStates states) {
			states = new RenderStates(states) { Shader = Shader };
			target.Draw(Drawable, states);
		}
	}

	public class StormBlink : Effect {

		public StormBlink(IDrawable drawable) {
			Drawable = drawable;
			Shader = new Shader(Utils.GetFilePath("Assets\\Resources\\Shaders\\storm.vert"), Utils.GetFilePath("Assets\\Resources\\Shaders\\blink.frag"));
		}

		public override void OnUpdate(float time, float x, float y) {
			float radius = 200 + (float)Math.Cos(time) * 150;
			Shader.SetParameter("storm_position", x * 800, y * 600);
			Shader.SetParameter("storm_inner_radius", radius / 3);
			Shader.SetParameter("storm_total_radius", radius);
			Shader.SetParameter("blink_alpha", 0.5F + (float)Math.Cos(time * 3) * 0.25F);
		}

		public override void OnDraw(IRenderTarget target, RenderStates states) {
			states = new RenderStates(states) { Shader = Shader };
			target.Draw(Drawable, states);
		}
	}


	public class Edge : Effect {

		public Edge(IDrawable drawable) {
			Drawable = drawable;

			Shader = new Shader(null, Utils.GetFilePath("Assets\\Resources\\Shaders\\edge.frag"));
			Shader.SetParameter("texture", Shader.CurrentTexture);
		}

		public override void OnUpdate(float time, float x, float y) {
			Shader.SetParameter("edge_threshold", 1 - (x + y) / 2);
		}

		public override void OnDraw(IRenderTarget target, RenderStates states) {
			states = new RenderStates(states) { Shader = Shader };
			target.Draw(Drawable, states);
		}
	}

	#endregion
}