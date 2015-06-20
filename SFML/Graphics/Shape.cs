using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML {

	namespace Graphics {

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Base class for textured shapes with outline
		/// </summary>
		////////////////////////////////////////////////////////////
		public abstract class Shape : Transformable, IDrawable {

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Source texture of the shape
			/// </summary>
			////////////////////////////////////////////////////////////
			public Texture Texture {
				get { return myTexture; }
				set {
					myTexture = value;
					sfShape_setTexture(CPointer, value != null ? value.CPointer : IntPtr.Zero, false);
				}
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Sub-rectangle of the texture that the shape will display
			/// </summary>
			////////////////////////////////////////////////////////////
			public IntRect TextureRect {
				get { return sfShape_getTextureRect(CPointer); }
				set { sfShape_setTextureRect(CPointer, value); }
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Fill color of the shape
			/// </summary>
			////////////////////////////////////////////////////////////
			public Color FillColor {
				get { return sfShape_getFillColor(CPointer); }
				set { sfShape_setFillColor(CPointer, value); }
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Outline color of the shape
			/// </summary>
			////////////////////////////////////////////////////////////
			public Color OutlineColor {
				get { return sfShape_getOutlineColor(CPointer); }
				set { sfShape_setOutlineColor(CPointer, value); }
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Thickness of the shape's outline
			/// </summary>
			////////////////////////////////////////////////////////////
			public float OutlineThickness {
				get { return sfShape_getOutlineThickness(CPointer); }
				set { sfShape_setOutlineThickness(CPointer, value); }
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Get the total number of points of the shape
			/// </summary>
			/// <returns>The total point count</returns>
			////////////////////////////////////////////////////////////
			public abstract uint GetPointCount();

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Get the position of a point
			///
			/// The returned point is in local coordinates, that is,
			/// the shape's transforms (position, rotation, scale) are
			/// not taken into account.
			/// The result is undefined if index is out of the valid range.
			/// </summary>
			/// <param name="index">Index of the point to get, in range [0 .. PointCount - 1]</param>
			/// <returns>index-th point of the shape</returns>
			////////////////////////////////////////////////////////////
			public abstract Vector2F GetPoint(uint index);

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Get the local bounding rectangle of the entity.
			///
			/// The returned rectangle is in local coordinates, which means
			/// that it ignores the transformations (translation, rotation,
			/// scale, ...) that are applied to the entity.
			/// In other words, this function returns the bounds of the
			/// entity in the entity's coordinate system.
			/// </summary>
			/// <returns>Local bounding rectangle of the entity</returns>
			////////////////////////////////////////////////////////////
			public FloatRect GetLocalBounds() {
				return sfShape_getLocalBounds(CPointer);
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Get the global bounding rectangle of the entity.
			///
			/// The returned rectangle is in global coordinates, which means
			/// that it takes in account the transformations (translation,
			/// rotation, scale, ...) that are applied to the entity.
			/// In other words, this function returns the bounds of the
			/// sprite in the global 2D world's coordinate system.
			/// </summary>
			/// <returns>Global bounding rectangle of the entity</returns>
			////////////////////////////////////////////////////////////
			public FloatRect GetGlobalBounds() {
				// we don't use the native getGlobalBounds function,
				// because we override the object's transform
				return Transform.TransformRect(GetLocalBounds());
			}

			////////////////////////////////////////////////////////////
			/// <summmary>
			/// Draw the shape to a render target
			/// </summmary>
			/// <param name="target">Render target to draw to</param>
			/// <param name="states">Current render states</param>
			////////////////////////////////////////////////////////////
			public void Draw(IRenderTarget target, RenderStates states) {
				states.Transform *= Transform;
				RenderStates.MarshalData marshaledStates = states.Marshal();

				if(target is RenderWindow) {
					sfRenderWindow_drawShape(((RenderWindow)target).CPointer, CPointer, ref marshaledStates);
				} else if(target is RenderTexture) {
					sfRenderTexture_drawShape(((RenderTexture)target).CPointer, CPointer, ref marshaledStates);
				}
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Default constructor
			/// </summary>
			////////////////////////////////////////////////////////////
			protected Shape() :
				base(IntPtr.Zero) {
				CPointer = sfShape_create(InternalGetPointCount, InternalGetPoint, IntPtr.Zero);
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Construct the shape from another shape
			/// </summary>
			/// <param name="copy">Shape to copy</param>
			////////////////////////////////////////////////////////////
			protected Shape(Shape copy) :
				base(IntPtr.Zero) {
				CPointer = sfShape_create(InternalGetPointCount, InternalGetPoint, IntPtr.Zero);

				Origin = copy.Origin;
				Position = copy.Position;
				Rotation = copy.Rotation;
				Scale = copy.Scale;

				Texture = copy.Texture;
				TextureRect = copy.TextureRect;
				FillColor = copy.FillColor;
				OutlineColor = copy.OutlineColor;
				OutlineThickness = copy.OutlineThickness;
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Recompute the internal geometry of the shape.
			///
			/// This function must be called by the derived class everytime
			/// the shape's points change (ie. the result of either
			/// PointCount or GetPoint is different).
			/// </summary>
			////////////////////////////////////////////////////////////
			protected void Update() {
				sfShape_update(CPointer);
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Handle the destruction of the object
			/// </summary>
			/// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
			////////////////////////////////////////////////////////////
			protected override void Destroy(bool disposing) {
				sfShape_destroy(CPointer);
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Callback passed to the C API
			/// </summary>
			////////////////////////////////////////////////////////////
			private uint InternalGetPointCount(IntPtr userData) {
				return GetPointCount();
			}

			////////////////////////////////////////////////////////////
			/// <summary>
			/// Callback passed to the C API
			/// </summary>
			////////////////////////////////////////////////////////////
			private Vector2F InternalGetPoint(uint index, IntPtr userData) {
				return GetPoint(index);
			}

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			private delegate uint GetPointCountCallbackType(IntPtr userData);

			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			private delegate Vector2F GetPointCallbackType(uint index, IntPtr userData);

			private Texture myTexture;

			#region Imports

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern IntPtr sfShape_create(GetPointCountCallbackType getPointCount, GetPointCallbackType getPoint, IntPtr userData);

			// [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			// static extern IntPtr sfShape_copy(IntPtr shape);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfShape_destroy(IntPtr cPointer);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfShape_setTexture(IntPtr cPointer, IntPtr texture, bool adjustToNewSize);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfShape_setTextureRect(IntPtr cPointer, IntRect rect);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern IntRect sfShape_getTextureRect(IntPtr cPointer);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfShape_setFillColor(IntPtr cPointer, Color color);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern Color sfShape_getFillColor(IntPtr cPointer);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfShape_setOutlineColor(IntPtr cPointer, Color color);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern Color sfShape_getOutlineColor(IntPtr cPointer);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfShape_setOutlineThickness(IntPtr cPointer, float thickness);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern float sfShape_getOutlineThickness(IntPtr cPointer);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern FloatRect sfShape_getLocalBounds(IntPtr cPointer);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfShape_update(IntPtr cPointer);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfRenderWindow_drawShape(IntPtr cPointer, IntPtr shape, ref RenderStates.MarshalData states);

			[DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
			static extern void sfRenderTexture_drawShape(IntPtr cPointer, IntPtr shape, ref RenderStates.MarshalData states);

			#endregion
		}
	}
}
