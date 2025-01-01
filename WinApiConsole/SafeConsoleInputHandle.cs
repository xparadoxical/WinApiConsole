using Microsoft.Win32.SafeHandles;

using TerraFX.Interop.Windows;

namespace WinapiConsole;
public class SafeConsoleInputHandle : SafeHandleZeroOrMinusOneIsInvalid
{
	private SafeConsoleInputHandle(bool ownsHandle) : base(ownsHandle) { }

	public SafeConsoleInputHandle() : this(false)
	{
		Handle = FetchHandle();
		EventHandle = new EventWaitHandle(false, EventResetMode.ManualReset) //TODO don't waste a handle each time
		{
			SafeWaitHandle = new SafeWaitHandle(handle, false)
		};
	}

	public nint Handle { get; }

	public EventWaitHandle EventHandle { get; }

	protected override bool ReleaseHandle()
		=> throw new NotSupportedException();

	private HANDLE FetchHandle()
	{
		const uint STD_INPUT_HANDLE = unchecked((uint)-10);

		var handle = Windows.GetStdHandle(STD_INPUT_HANDLE);
		SetHandle(handle);
		return handle;
	}
}
