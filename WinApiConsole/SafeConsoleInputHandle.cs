using Microsoft.Win32.SafeHandles;

using WinApiConsole;

namespace WinapiConsole;

//SafeConsoleInputHandle? does this require a console input buffer handle and can't use a file handle from redirected stdin?
public class SafeStandardInputHandle : SafeStandardHandle
{
	public SafeStandardInputHandle() : base(StandardHandleType.Input)
	{
		EventHandle = new EventWaitHandle(false, EventResetMode.ManualReset) //TODO don't waste a handle each time
		{
			SafeWaitHandle = new SafeWaitHandle(handle, false)
		};
	}

	public nint Handle => handle;

	public EventWaitHandle EventHandle { get; }
}
