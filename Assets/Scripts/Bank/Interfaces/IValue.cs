using System;

namespace Bank
{
	public interface IValue
	{
		int Count { get; set; }
		bool Put(int count);
		void Put(int count, Action<bool> callback);
		bool Withdraw(int count);
		void Withdraw(int count, Action<bool> callback);
	}
}
