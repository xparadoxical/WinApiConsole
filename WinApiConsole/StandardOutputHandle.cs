using System.ComponentModel;
using System.Runtime.CompilerServices;

using TerraFX.Interop.Windows;

namespace WinApiConsole;

/// <summary>A handle to a standard output or error device of a process.</summary>
public sealed class StandardOutputHandle : StandardHandle
{
	public StandardOutputHandle(StandardHandleType type) : base(type) { }

	public StandardOutputHandle(StandardHandleType type, bool ownsHandle) : base(type, ownsHandle) { }

	/// <exception cref="Win32Exception"></exception>
	public unsafe OutputModes GetConsoleMode()
	{
		Unsafe.SkipInit(out uint modes);
		if (!Windows.GetConsoleMode((HANDLE)handle, &modes))
			throw new Win32Exception();

		return (OutputModes)modes;
	}

	/// <exception cref="Win32Exception"></exception>
	public void SetConsoleMode(OutputModes mode)
	{
		if (!Windows.SetConsoleMode((HANDLE)handle, (uint)mode))
			throw new Win32Exception();
	}
}
