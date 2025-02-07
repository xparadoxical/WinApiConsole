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
	/// <summary>
	/// Enhanced keys for the IBM® 101- and 102-key keyboards are the INS, DEL, HOME, END, PAGE UP, PAGE DOWN,
	/// and direction keys in the clusters to the left of the keypad; and the divide (/) and ENTER keys in the keypad.
	/// </summary>
	IsEnhancedKey = Windows.ENHANCED_KEY
}
