namespace WinapiConsole;
public sealed class PinnableBox<T>(T? value)
{
	private T? _value = value;
	public ref T? Value => ref _value;

	public PinnableBox() : this(default) { }

	public ref T? GetPinnableReference() => ref _value;

	public static implicit operator T?(PinnableBox<T> box) => box._value;
}
