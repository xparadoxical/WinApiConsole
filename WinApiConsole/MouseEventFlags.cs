namespace WinApiConsole;
[Flags]
public enum MouseEventFlags : uint
{
	MouseMoved = 0x1,
	/// <summary>
	/// The second click (button press) of a double-click occurred.
	/// The first click is returned as a regular button-press event.
	/// </summary>
	DoubleClick = 0x2,
	WheelMoved = 0x4,
	HorizontalWheelMoved = 0x8
}
