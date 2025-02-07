namespace WinApiConsole;

[Flags]
public enum OutputModes : uint
{
	/// <summary>
	/// Characters written by the WriteFile or WriteConsole function or echoed by the ReadFile or ReadConsole
	/// function are parsed for ASCII control sequences, and the correct action is performed. Backspace, tab,
	/// bell, carriage return, and line feed characters are processed. It should be enabled when using control
	/// sequences or when <see cref="VirtualTerminalProcessing"/> is set.
	/// </summary>
	ProcessedOutput = 0x1,
	/// <summary>
	/// When writing with WriteFile or WriteConsole or echoing with ReadFile or ReadConsole, the cursor moves
	/// to the beginning of the next row when it reaches the end of the current row. This causes the rows
	/// displayed in the console window to scroll up automatically when the cursor advances beyond the last row
	/// in the window. It also causes the contents of the console screen buffer to scroll up (discarding the top
	/// row of the console screen buffer) when the cursor advances beyond the last row in the console screen buffer.
	/// If this mode is disabled, the last character in the row is overwritten with any subsequent characters.
	/// </summary>
	ScrollAtBufferEnd = 0x2,
	/// <summary>
	/// When writing with WriteFile or WriteConsole, characters are parsed for VT100 and similar control character
	/// sequences that control cursor movement, color/font mode, and other operations that can also be performed via
	/// the existing Console APIs. Ensure <see cref="ProcessedOutput"/> is set when using this flag.
	/// </summary>
	VirtualTerminalProcessing = 0x4,
	/// <summary>DISABLE_NEWLINE_AUTO_RETURN</summary>
	NoImplicitCROnLF = 0x8, //TODO verify all the yapping from the docs
	/// <summary>
	/// The APIs for writing character attributes including WriteConsoleOutput and WriteConsoleOutputAttribute
	/// allow the usage of flags from character attributes to adjust the color of the foreground and background
	/// of text. With exception of the leading byte and trailing byte flags, the remaining flags describing line
	/// drawing and reverse video (swap foreground and background colors) can be useful to emphasize portions of output.
	/// Setting this console mode flag will allow these attributes to be used in every code page on every language.
	/// </summary>
	LvbGridWorldwide = 0x10 //TODO can you really use the line thing and color reversing?
}
