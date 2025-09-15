using TerraFX.Interop.Windows;

namespace WinApiConsole;
[Flags]
public enum MouseButtons : ushort
{
	Left = Windows.FROM_LEFT_1ST_BUTTON_PRESSED,
	Right = Windows.RIGHTMOST_BUTTON_PRESSED,
	Scroll = Windows.FROM_LEFT_2ND_BUTTON_PRESSED
}
