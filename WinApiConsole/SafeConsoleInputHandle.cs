using System.Runtime.CompilerServices;

using Microsoft.Win32.SafeHandles;

using WinApiConsole;

namespace WinapiConsole;

//SafeConsoleInputHandle? does this require a console input buffer handle and can't use a file handle from redirected stdin?
public class SafeStandardInputHandle : SafeStandardHandle
{
	public SafeStandardInputHandle() : base(StandardHandleType.Input)
	{
		EventHandle = EventWaitHandleConstructor(new SafeWaitHandle(handle, false));

		[UnsafeAccessor(UnsafeAccessorKind.Constructor)]
		static extern EventWaitHandle EventWaitHandleConstructor(SafeWaitHandle handle);
	}

	public nint Handle => handle;

	public EventWaitHandle EventHandle { get; }
}
