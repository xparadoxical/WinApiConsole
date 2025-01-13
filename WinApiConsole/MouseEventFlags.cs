namespace WinApiConsole;
[Flags]
public enum MouseEventFlags : uint
{
	MouseMoved = 0x1,
	DoubleClick = 0x2,
	WheelMoved = 0x4,
	HorizontalWheelMoved = 0x8
}
