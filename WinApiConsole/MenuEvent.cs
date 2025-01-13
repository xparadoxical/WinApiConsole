using TerraFX.Interop.Windows;

namespace WinApiConsole;
public sealed record MenuEvent(uint CommandId) : InputRecord
{
	internal static MenuEvent FromNative(MENU_EVENT_RECORD r)
		=> new(r.dwCommandId);
}
