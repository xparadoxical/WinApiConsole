using System.Buffers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
		var inputHandle = (HANDLE)handle.RawHandle;

		Unsafe.SkipInit(out uint recordsAvailable);
		if (!Windows.GetNumberOfConsoleInputEvents(inputHandle, &recordsAvailable))
			throw new Win32Exception();

		var ret = ArrayPool<INPUT_RECORD>.Shared.RentScope((int)recordsAvailable);
		var buffer = UnsafeArrayScope.GetArray(ret);

		Unsafe.SkipInit(out uint recordsRead);
		fixed (INPUT_RECORD* pBuffer = buffer)
		{
			const int CONSOLE_READ_NOWAIT = 0x0002;
			if (!Interop.Console.ReadConsoleInputEx(inputHandle, pBuffer, recordsAvailable, &recordsRead, CONSOLE_READ_NOWAIT))
			{
				if (buffer is not null)
					ret.Dispose();
				throw new Win32Exception();
			}
		}

		return ret;
	}
}
