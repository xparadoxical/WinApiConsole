using TerraFX.Interop.Windows;

namespace WinApiConsole;
[Flags]
public enum MouseEventFlags : uint
{
	MouseMoved = Windows.MOUSE_MOVED,
	/// <summary>
	/// The second click (button press) of a double-click occurred.
	/// The first click is returned as a regular button-press event.
	/// </summary>
	DoubleClick = Windows.DOUBLE_CLICK,
	WheelMoved = Windows.MOUSE_WHEELED,
	HorizontalWheelMoved = Windows.MOUSE_HWHEELED
}
