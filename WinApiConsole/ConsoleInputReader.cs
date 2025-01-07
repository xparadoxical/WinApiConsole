using System.Buffers;
using System.ComponentModel;

using Hertzole.Buffers;

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

	public unsafe ArrayPoolScope<INPUT_RECORD> ReadInputs()
	{
		var inputHandle = (HANDLE)handle.Handle;

		var recordsAvailable = new PinnableBox<uint>(0);
		fixed (uint* pRecordsAvailable = recordsAvailable)
			if (!Windows.GetNumberOfConsoleInputEvents(inputHandle, pRecordsAvailable))
				throw new Win32Exception();

		var ret = ArrayPool<INPUT_RECORD>.Shared.RentScope((int)recordsAvailable.Value);
		var buffer = UnsafeArrayScope.GetArray(ret);

		var recordsRead = new PinnableBox<uint>(0);
		fixed (INPUT_RECORD* pBuffer = buffer)
		fixed (uint* pRecordsRead = recordsRead)
		{
			const int CONSOLE_READ_NOWAIT = 0x0002;
			if (!Interop.Console.ReadConsoleInputEx(inputHandle, pBuffer, recordsAvailable, pRecordsRead, CONSOLE_READ_NOWAIT))
			{
				if (buffer is not null)
					ret.Dispose();
				throw new Win32Exception();
			}
		}

		return ret;
	}
}
