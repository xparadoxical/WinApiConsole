﻿namespace WinApiConsole;
[Flags]
public enum InputModes : uint
{
	/// <summary>
	/// CTRL+C is processed by the system and is not placed in the input buffer. If the input buffer
	/// is being read by ReadFile or ReadConsole, other control keys are processed by the system and
	/// are not returned in the ReadFile or ReadConsole buffer. If the <see cref="LineInput"/> mode is
	/// also enabled, backspace, carriage return, and line feed characters are handled by the system.
	/// </summary>
	ProcessedInput = 0x1, //TODO more processed sequences than ctrl+c and cr/lf?
	/// <summary>
	/// The ReadFile or ReadConsole function returns only when a carriage return character is read.
	/// If this mode is disabled, the functions return when one or more characters are available.
	/// </summary>
	LineInput = 0x2,
	/// <summary>
	/// Characters read by the ReadFile or ReadConsole function are written to the active screen buffer
	/// as they are typed into the console. This mode can be used only if the <see cref="LineInput"/> mode is also enabled.
	/// </summary>
	EchoInput = 0x4,
	/// <summary>
	/// User interactions that change the size of the console screen buffer are reported in the console's
	/// input buffer. Information about these events can be read from the input buffer by applications
	/// using <see cref="ConsoleInputReader.ReadAvailableInputs"/>, but not by those using ReadFile or ReadConsole.
	/// </summary>
	EnableWindowInput = 0x8,
	/// <summary>
	/// If the mouse pointer is within the borders of the console window and the window has the keyboard
	/// focus, mouse events generated by mouse movement and button presses are placed in the input buffer.
	/// These events are discarded by ReadFile or ReadConsole, even when this mode is enabled.
	/// The <see cref="ConsoleInputReader.ReadAvailableInputs"/> method can be used to read
	/// <see cref="MouseEvent"/>s from the input buffer. <see cref="QuickEditMode"/> may effectively disable
	/// mouse input, make sure to disable it.
	/// </summary>
	EnableMouseInput = 0x10,
	/// <summary>
	/// When enabled, text entered in a console window will be inserted at the current cursor location and all
	/// text following that location will not be overwritten. When disabled, all following text will be overwritten.
	/// <para>
	/// To enable this mode, use <see cref="InsertMode"/> | <see cref="EnableExtendedFlags"/>.
	/// To disable this mode, use <see cref="EnableExtendedFlags"/> without this flag.
	/// </para>
	/// </summary>
	InsertMode = 0x20,
	/// <summary>
	/// This flag enables the user to use the mouse to select and edit text.
	/// It may also prevent the input buffer from receiving mouse events if the terminal implementing quick edit mode
	/// doesn't forward events to the buffer.
	/// <para>
	/// To enable this mode, use <see cref="QuickEditMode"/> | <see cref="EnableExtendedFlags"/>.
	/// To disable this mode, use <see cref="EnableExtendedFlags"/> without this flag.
	/// </para>
	/// </summary>
	QuickEditMode = 0x40,
	/// <summary>
	/// Required to enable or disable extended flags. See <see cref="InsertMode"/> and <see cref="QuickEditMode"/>.
	/// </summary>
	EnableExtendedFlags = 0x80,
	AutoPosition = 0x100, //TODO does this control auto-positioning of cmd and wt?
	/// <summary>
	/// Setting this flag directs the Virtual Terminal processing engine to convert user input received
	/// by the console window into Console Virtual Terminal Sequences that can be retrieved by a supporting
	/// application through ReadFile or ReadConsole functions.
	/// <para>
	/// The typical usage of this flag is intended in conjunction with <see cref="OutputModes.VirtualTerminalProcessing"/>
	/// on the <see cref="StandardOutputHandle"/> to connect to an application that communicates exclusively
	/// via virtual terminal sequences.
	/// </para>
	/// </summary>
	VirtualTerminalInput = 0x200
}
