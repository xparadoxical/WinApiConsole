namespace WinApiConsole;

[Flags]
public enum OutputModes : uint
{
	ProcessedOutput = 0x1,
	/// <summary>ENABLE_WRAP_AT_EOL_OUTPUT</summary>
	ScrollAtBufferEnd = 0x2,
	VirtualTerminalProcessing = 0x4,
	/// <summary>DISABLE_NEWLINE_AUTO_RETURN</summary>
	NoImplicitCROnLF = 0x8,
	LvbGridWorldwide = 0x10
}
