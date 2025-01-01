namespace WinApiConsole;
public enum StandardHandleType : uint
{
	Input = unchecked((uint)-10),
	Output = unchecked((uint)-11),
	Error = unchecked((uint)-12)
}
