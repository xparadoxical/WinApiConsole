using TerraFX.Interop.Windows;

namespace WinApiConsole;

/// <summary>Describes a menu event. These events are used internally and should be ignored.</summary>
public sealed record MenuEvent(uint CommandId) : InputRecord
{
	internal static MenuEvent FromNative(MENU_EVENT_RECORD r)
		=> new(r.dwCommandId);
}
