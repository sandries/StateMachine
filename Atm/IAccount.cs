namespace Atm
{
	public interface IAccount
	{
		int GetCurrentBalance();

		void AddAmount(int amount);

		void SubtractAmount(int amount);
	}
}