using System.ComponentModel;
using System.Runtime.CompilerServices;

using Microsoft.Win32.SafeHandles;

using TerraFX.Interop.Windows;

namespace WinApiConsole;

//SafeConsoleInputHandle? does this require a console input buffer handle and can't use a file handle from redirected stdin?
public class StandardInputHandle : StandardHandle
{
	public StandardInputHandle() : base(StandardHandleType.Input)
	{
		EventHandle = EventWaitHandleConstructor(new SafeWaitHandle(handle, false));

		[UnsafeAccessor(UnsafeAccessorKind.Constructor)]
		static extern EventWaitHandle EventWaitHandleConstructor(SafeWaitHandle handle);
	}

	public nint RawHandle => handle;

	public EventWaitHandle EventHandle { get; }

	public unsafe InputModes GetConsoleMode()
	{
		Unsafe.SkipInit(out uint modes);
		if (!Windows.GetConsoleMode((HANDLE)handle, &modes))
			throw new Win32Exception();

		return (InputModes)modes;
	}
}
