namespace Atm
{
	using System;
	using System.Collections.Generic;
	using StateMachine;
	using StateMachine.Transition;

	public class AtmStateMachine : StateMachine
	{
		public AtmStateMachine(List<Transition> transitions, State currentState)
			: base(transitions, currentState)
		{

		}

		public void Run()
		{
			while (true)
			{
				this.MoveNext(new StateEventArgs((int)Event.EnterMenu));
				if (!this.ShowMenu())
				{
					return;
				}

				Console.WriteLine();
				Console.WriteLine("Press ESC to stop");
				Console.WriteLine("Press ENTER to show menu");

				ConsoleKey key = Console.ReadKey(true).Key;
				if (key == ConsoleKey.Escape)
				{
					return;
				}
				if (key == ConsoleKey.Enter)
				{
					this.MoveNext(new StateEventArgs((int)Event.EnterIdle));
				}

				Console.Clear();
			}
		}

		private bool ShowMenu()
		{
			Console.WriteLine("Please select an action: ");
			Console.WriteLine("1. Show balance");
			Console.WriteLine("2. Withdraw money");
			Console.WriteLine("3. Deposit money");
			Console.WriteLine("Press ESC to stop");			

			var keiConsoleKeyInfo = Console.ReadKey(true);
			if (keiConsoleKeyInfo.Key == ConsoleKey.Escape)
			{
				return false;				
			}

			int number ;
			if (Int32.TryParse(keiConsoleKeyInfo.KeyChar.ToString(), out number))
			{
				this.MoveNext(new StateEventArgs(Convert.ToInt32(number)));			
			}

			return true;
		}
	}
}
