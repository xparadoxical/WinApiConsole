namespace WinApiConsole;
[Flags]
public enum InputModes : uint
{
	ProcessedInput = 0x1,
	LineInput = 0x2,
	EchoInput = 0x4,
	EnableWindowInput = 0x8,
	EnableMouseInput = 0x10,
	InsertMode = 0x20,
	QuickEditMode = 0x40,
	EnableExtendedFlags = 0x80,
	AutoPosition = 0x100,
	VirtualTerminalInput = 0x200
}
