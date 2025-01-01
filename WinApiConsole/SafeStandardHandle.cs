using Microsoft.Win32.SafeHandles;

using TerraFX.Interop.Windows;

namespace WinApiConsole;
public class SafeStandardHandle : SafeHandleZeroOrMinusOneIsInvalid
{
	public SafeStandardHandle(StandardHandleType type) : base(false)
		=> SetHandle(type);
	public SafeStandardHandle(StandardHandleType type, bool ownsHandle) : base(ownsHandle)
		=> SetHandle(type);

	private void SetHandle(StandardHandleType type) => SetHandle(Windows.GetStdHandle((uint)type));

	protected override bool ReleaseHandle()
		=> throw new NotSupportedException();
}
