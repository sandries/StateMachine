namespace Atm
{
	using Atm.Factory;

	class Program
	{
		static void Main(string[] args)
		{
			AtmStateMachineFactory factory = new AtmStateMachineFactory();
			var atmStateMachine = factory.CreateStateMachine();
			atmStateMachine.Run();
		}
	}
}
