using TerraFX.Interop.Windows;

namespace WinApiConsole;
public enum StandardHandleType : uint
{
	Input = STD.STD_INPUT_HANDLE,
	Output = STD.STD_OUTPUT_HANDLE,
	Error = STD.STD_ERROR_HANDLE
}
