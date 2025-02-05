using System.Diagnostics;
using System.Drawing;

using TerraFX.Interop.Windows;

namespace WinApiConsole;
public sealed record MouseEvent(
	Point Position,
	MouseButtons Buttons,
	MouseEventFlags Flags,
	ScrollDirection ScrollDirection,
	ControlKeyStates ControlKeys) : InputRecord
{
	internal static MouseEvent FromNative(MOUSE_EVENT_RECORD r)
		=> new(new Point(r.dwMousePosition.X, r.dwMousePosition.Y),
			(MouseButtons)unchecked((ushort)r.dwButtonState),
			(MouseEventFlags)r.dwEventFlags,
			(r.dwEventFlags & (uint)MouseEventFlags.WheelMoved, r.dwEventFlags & (uint)MouseEventFlags.HorizontalWheelMoved, (int)r.dwButtonState & ~ushort.MaxValue) switch
			{
				(0, 0, _) => ScrollDirection.None,
				(not 0, 0, > 0) => ScrollDirection.Forward,
				(not 0, 0, < 0) => ScrollDirection.Backward,
				(0, not 0, > 0) => ScrollDirection.Right,
				(0, not 0, < 0) => ScrollDirection.Left,
				_ => throw new UnreachableException($"Unexpected scroll state: dwEventFlags={r.dwEventFlags:x8} dwButtonState={r.dwButtonState:x8}")
			},
			(ControlKeyStates)r.dwControlKeyState);
}
