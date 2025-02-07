using Microsoft.Win32.SafeHandles;

using TerraFX.Interop.Windows;

namespace WinApiConsole;

/// <summary>A handle to a standard device of a process.</summary>
public class StandardHandle : SafeHandleZeroOrMinusOneIsInvalid
{
	public StandardHandle(StandardHandleType type) : base(false)
		=> SetHandle(type);
	public StandardHandle(StandardHandleType type, bool ownsHandle) : base(ownsHandle)
		=> SetHandle(type);

	private void SetHandle(StandardHandleType type) => SetHandle(Windows.GetStdHandle((uint)type));

	protected override bool ReleaseHandle()
		=> Windows.CloseHandle((HANDLE)handle);
}
