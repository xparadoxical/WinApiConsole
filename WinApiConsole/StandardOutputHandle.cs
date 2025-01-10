using System.ComponentModel;
using System.Runtime.CompilerServices;

using TerraFX.Interop.Windows;

namespace WinApiConsole;
public sealed class StandardOutputHandle : StandardHandle
{
	public StandardOutputHandle(StandardHandleType type) : base(type) { }

	public StandardOutputHandle(StandardHandleType type, bool ownsHandle) : base(type, ownsHandle) { }

	public unsafe OutputModes GetConsoleMode()
	{
		Unsafe.SkipInit(out uint modes);
		if (!Windows.GetConsoleMode((HANDLE)handle, &modes))
			throw new Win32Exception();

		return (OutputModes)modes;
	}
}
