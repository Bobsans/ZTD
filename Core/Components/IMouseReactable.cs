using System;
using SFML.Window;

namespace ZTD.Core.Components {

	public interface IMouseReactable {

		event EventHandler<MouseButtonEventArgs> MousePressed;
		event EventHandler<MouseButtonEventArgs> MouseReleased;
		event EventHandler<MouseMoveEventArgs> MouseMove;
		event EventHandler MouseEnter;
		event EventHandler MouseLeave;

		void OnMousePressed(object sender, MouseButtonEventArgs e);
		void OnMouseReleased(object sender, MouseButtonEventArgs e);
		void OnMouseMove(object sender, MouseMoveEventArgs e);
		void OnMouseEnter(object sender, MouseButtonEventArgs e);
		void OnMouseLeave(object sender, MouseButtonEventArgs e);
	}
}