using TerraFX.Interop.Windows;

namespace WinApiConsole;
[Flags]
public enum ControlKeyStates : uint
{
	RightAltPressed = Windows.RIGHT_ALT_PRESSED,
	LeftAltPressed = Windows.LEFT_ALT_PRESSED,
	RightCtrlPressed = Windows.RIGHT_CTRL_PRESSED,
	LeftCtrlPressed = Windows.LEFT_CTRL_PRESSED,
	ShiftPressed = Windows.SHIFT_PRESSED,
	NumLockOn = Windows.NUMLOCK_ON,
	ScrollLockOn = Windows.SCROLLLOCK_ON,
	CapsLockOn = Windows.CAPSLOCK_ON,
	IsEnhancedKey = Windows.ENHANCED_KEY
}
