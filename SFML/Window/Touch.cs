using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML {

	namespace Window {

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Give access to the real-time state of the touches
		/// </summary>
		////////////////////////////////////////////////////////////
		public static class Touch {

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Check if a touch event is currently down
			/// </summary>
			/// <param name="finger">Finger index</param>
			/// <returns>True if the finger is currently touching the screen, false otherwise</returns>
			////////////////////////////////////////////////////////////
			public static bool IsDown(uint finger) {
				return sfTouch_isDown(finger);
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// This function returns the current touch position
			/// </summary>
			/// <param name="finger">Finger index</param>
			/// <returns>Current position of the finger</returns>
			////////////////////////////////////////////////////////////
			public static Vector2I GetPosition(uint finger) {
				return GetPosition(finger, null);
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// This function returns the current touch position
			/// relative to the given window
			/// </summary>
			/// <param name="finger">Finger index</param>
			/// <param name="RelativeTo">Reference window</param>
			/// <returns>Current position of the finger</returns>
			////////////////////////////////////////////////////////////
			public static Vector2I GetPosition(uint finger, Window RelativeTo) {
				if(RelativeTo != null)
					return RelativeTo.InternalGetTouchPosition(finger);
				return sfTouch_getPosition(finger, IntPtr.Zero);
			}

			#region Imports

			[DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern bool sfTouch_isDown(uint finger);

			[DllImport("csfml-window-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern Vector2I sfTouch_getPosition(uint finger, IntPtr relativeTo);

			#endregion
		}
	}
}
