// ReSharper disable once CheckNamespace

using Avalonia;
using System;

namespace AvaloniaProgressRing
{
	internal sealed class SimpleObserver<TOwner, T> : IObserver<T>
		where TOwner : AvaloniaObject
	{
		private readonly TOwner _owner;
		private readonly Action<TOwner, T> _listener;

		public SimpleObserver(TOwner owner, Action<TOwner, T> listener)
		{
			_owner = owner ?? throw new ArgumentNullException(nameof(owner));
			_listener = listener ?? throw new ArgumentNullException(nameof(listener));
		}

		public void OnCompleted()
		{ }

		public void OnError(Exception error)
		{ }

		public void OnNext(T value)
			=> _listener(_owner, value);
	}
}