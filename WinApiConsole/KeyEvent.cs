using TerraFX.Interop.Windows;

namespace WinApiConsole;

public sealed record KeyEvent(
	bool KeyDown,
	ushort RepeatCount,
	char Char, //TODO can it be null?
	ushort KeyCode, //TODO use some winforms/wpf enum?
	ushort ScanCode, //TODO any existing enum for this?
	ControlKeyStates ControlKeys) : InputRecord
{
	internal static KeyEvent FromNative(KEY_EVENT_RECORD r)
		=> new(r.bKeyDown, r.wRepeatCount, r.uChar.UnicodeChar, r.wVirtualKeyCode, r.wVirtualScanCode, (ControlKeyStates)r.dwControlKeyState);
}