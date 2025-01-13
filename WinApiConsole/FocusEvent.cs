using TerraFX.Interop.Windows;

namespace WinApiConsole;
public sealed record FocusEvent(bool Focused) : InputRecord
{
	internal static FocusEvent FromNative(FOCUS_EVENT_RECORD r)
		=> new(r.bSetFocus);
}
