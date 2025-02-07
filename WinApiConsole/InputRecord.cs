using System.Diagnostics;

using TerraFX.Interop.Windows;

namespace WinApiConsole;

/// <summary>Describes an input event in the console input buffer.</summary>
public abstract record InputRecord
{
	internal static InputRecord FromNative(INPUT_RECORD r)
	{
		return r.EventType switch
		{
			1 /*KEY_EVENT*/ => KeyEvent.FromNative(r.Event.KeyEvent),
			Windows.MOUSE_EVENT => MouseEvent.FromNative(r.Event.MouseEvent),
			Windows.WINDOW_BUFFER_SIZE_EVENT => BufferResizeEvent.FromNative(r.Event.WindowBufferSizeEvent),
			Windows.MENU_EVENT => MenuEvent.FromNative(r.Event.MenuEvent),
			Windows.FOCUS_EVENT => FocusEvent.FromNative(r.Event.FocusEvent),
			_ => throw new UnreachableException()
		};
	}
}
