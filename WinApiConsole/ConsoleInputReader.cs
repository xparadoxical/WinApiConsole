using System.Buffers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Hertzole.Buffers;

using TerraFX.Interop.Windows;

namespace WinApiConsole;

/// <summary>Provides methods for reading data from a standard input handle.</summary>
public class ConsoleInputReader(StandardInputHandle handle)
{
	public void SetHandle(StandardInputHandle newHandle) => handle = newHandle;

	/// <summary>
	/// Creates a task that will complete when the input handle becomes signaled.
	/// This occurs when the console input buffer receives input.
	/// </summary>
	public Task WhenInputAvailable()
	{
		var tcs = new TaskCompletionSource();
		var registration = ThreadPool.RegisterWaitForSingleObject(handle.EventHandle, (_, _) => tcs.SetResult(), null, -1, true);

		var t = tcs.Task;
		t.ContinueWith(_ => registration.Unregister(null));
		return tcs.Task;
	}

	/// <summary>Reads <see cref="InputRecord"/>s from the console input buffer, if any available.</summary>
	/// <returns>A disposable scope representing an array rented from an <see cref="ArrayPool{T}"/>.</returns>
	public unsafe ArrayPoolScope<InputRecord> ReadAvailableInputs()
	{
		var inputHandle = (HANDLE)handle.RawHandle;

		Unsafe.SkipInit(out uint recordsAvailable);
		if (!Windows.GetNumberOfConsoleInputEvents(inputHandle, &recordsAvailable))
			throw new Win32Exception();

		using var nativeRecords = ArrayPool<INPUT_RECORD>.Shared.RentScope((int)recordsAvailable);

		Unsafe.SkipInit(out uint recordsRead);
		fixed (INPUT_RECORD* pBuffer = UnsafeArrayScope.GetArray(nativeRecords))
		{
			if (!ConsoleInterop.ReadConsoleInputEx(inputHandle, pBuffer, recordsAvailable, &recordsRead, ConsoleInterop.ReadBehavior.NoWait))
				throw new Win32Exception();
		}

		var records = ArrayPool<InputRecord>.Shared.RentScope(nativeRecords.Length);
		for (int i = 0; i < nativeRecords.Length; i++)
			records[i] = InputRecord.FromNative(nativeRecords[i]);
		return records;
	}
}
