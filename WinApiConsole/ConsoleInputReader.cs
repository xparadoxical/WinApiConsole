using System.Buffers;
using System.ComponentModel;

using TerraFX.Interop.Windows;

namespace WinApiConsole;
public class ConsoleInputReader(SafeStandardInputHandle handle)
{
	public void SetHandle(SafeStandardInputHandle newHandle) => handle = newHandle;

	public Task WhenInputAvailable()
	{
		var tcs = new TaskCompletionSource();
		var registration = ThreadPool.RegisterWaitForSingleObject(handle.EventHandle, (_, _) => tcs.SetResult(), null, -1, true);

		var t = tcs.Task;
		t.ContinueWith(_ => registration.Unregister(null));
		return tcs.Task;
	}

	public unsafe IEnumerable<INPUT_RECORD> ReadEachInput()
	{
		var inputHandle = (HANDLE)handle.Handle;
		var recordsAvailable = new PinnableBox<uint>(0);
		unsafe
		{
			fixed (uint* pRecordsAvailable = recordsAvailable)
				if (!Windows.GetNumberOfConsoleInputEvents(inputHandle, pRecordsAvailable))
					throw new Win32Exception();
		}

		scoped Span<INPUT_RECORD> buffer;
		INPUT_RECORD[]? bufferArray = null;
		if (recordsAvailable <= 64)
			buffer = stackalloc INPUT_RECORD[(int)recordsAvailable.Value];
		else
		{
			bufferArray = ArrayPool<INPUT_RECORD>.Shared.Rent((int)recordsAvailable.Value);
			buffer = bufferArray.AsSpan(..(int)recordsAvailable.Value);
		}

		var recordsRead = new PinnableBox<uint>(0);
		unsafe
		{
			fixed (INPUT_RECORD* pBuffer = buffer)
			fixed (uint* pRecordsRead = recordsRead)
			{
				const int CONSOLE_READ_NOWAIT = 0x0002;
				if (!Interop.Console.ReadConsoleInputEx(inputHandle, pBuffer, recordsAvailable, pRecordsRead, CONSOLE_READ_NOWAIT))
				{
					if (bufferArray is not null)
						ArrayPool<INPUT_RECORD>.Shared.Return(bufferArray);
					throw new Win32Exception();
				}
			}
		}

		foreach (var record in buffer)
			yield return record;

		if (bufferArray is not null)
			ArrayPool<INPUT_RECORD>.Shared.Return(bufferArray);
	}
}
