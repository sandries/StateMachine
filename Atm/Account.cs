namespace Atm
{
	public class Account : IAccount
	{
		private  int currentBalance = 50;

		public int GetCurrentBalance()
		{
			return this.currentBalance;
		}

		public void AddAmount(int amount)
		{
			this.currentBalance += amount;
		}

		public void SubtractAmount(int amount)
		{
			this.currentBalance -= amount;
		}
	}
}
