using System.Drawing;

using TerraFX.Interop.Windows;

namespace WinApiConsole;
public sealed record BufferResizeEvent(Size NewSize) : InputRecord
{
	internal static BufferResizeEvent FromNative(WINDOW_BUFFER_SIZE_RECORD r)
		=> new(new Size(r.dwSize.X, r.dwSize.Y));
}
