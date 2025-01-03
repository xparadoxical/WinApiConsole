﻿using System.Runtime.InteropServices;

using TerraFX.Interop.Windows;

namespace WinApiConsole.Interop;
internal unsafe partial class Console
{
	[DllImport("kernel32", SetLastError = true, EntryPoint = "ReadConsoleInputExW")]
	public static extern BOOL ReadConsoleInputEx(HANDLE hConsoleInput, INPUT_RECORD* lpBuffer, uint nLength, uint* lpNumberOfEventsRead, ushort wFlags);

	[Flags]
	public enum ReadBehavior : ushort
	{
		/// <summary>Leave the events in the input buffer (as in PeekConsoleInput)</summary>
		NoRemove = 1,
		/// <summary>Return immediately, even if there are no events in the input buffer.</summary>
		NoWait = 2
	}
}
