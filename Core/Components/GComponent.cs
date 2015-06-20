using System;
using SFML.Graphics;
using SFML.Window;

namespace ZTD.Core.Components {

	public abstract class GComponent : IDrawable, IMouseReactable {

		public abstract void Draw(IRenderTarget target, RenderStates states);
		public abstract void Update();

		public event EventHandler<MouseButtonEventArgs> MousePressed;
		public event EventHandler<MouseButtonEventArgs> MouseReleased;
		public event EventHandler<MouseMoveEventArgs> MouseMove;
		public event EventHandler MouseEnter;
		public event EventHandler MouseLeave;

		public event EventHandler<MouseWheelEventArgs> MouseWheelMoved;

		public virtual void OnMousePressed(object sender, MouseButtonEventArgs e) {
			if(MousePressed != null)
				MousePressed(sender, e);
		}

		public virtual void OnMouseReleased(object sender, MouseButtonEventArgs e) {
			if(MouseReleased != null)
				MouseReleased(sender, e);
		}

		public virtual void OnMouseMove(object sender, MouseMoveEventArgs e) {
			if(MouseMove != null)
				MouseMove(sender, e);
		}

		public virtual void OnMouseEnter(object sender, MouseButtonEventArgs e) {
			if(MouseEnter != null)
				MouseEnter(sender, e);
		}

		public virtual void OnMouseLeave(object sender, MouseButtonEventArgs e) {
			if(MouseLeave != null)
				MouseLeave(sender, e);
		}

		public virtual void OnMouseWheelMoved(object sender, MouseWheelEventArgs e) {
			if(MouseWheelMoved != null)
				MouseWheelMoved(sender, e);
		}
	}
}