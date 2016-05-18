namespace Atm.Factory
{
	using System;
	using System.Collections.Generic;
	using StateMachine;
	using StateMachine.Transition;

	public class AtmStateMachineFactory
	{
		private readonly IAccount userAccount = new Account();

		private readonly IAccount atm = new Account();

		public AtmStateMachine CreateStateMachine()
		{
			TransitionBuilder builder = new TransitionBuilder();

			State enteredMenu = new State("EnteredMenu");
			State interogatedAccount = new State("InterogatedAccount");
			State withdrawedMoney = new State("WithdrawedMoney");
			State insufficientAmount = new State("InsufficientAmount");
			State moneyDeposited = new State("MoneyDeposited");
			State idle = new State("Idle");

			List<Transition> transitions = new List<Transition>
			{
				builder.CreateTransition()
					.WithSourceState(idle)
					.WithDestinationState(enteredMenu)
					.WithStateEventArgs(new StateEventArgs((int)Event.EnterMenu))
					.Build(),
				builder.CreateTransition()
					.WithSourceState(enteredMenu)
					.WithDestinationState(idle)
					.WithStateEventArgs(new StateEventArgs((int)Event.EnterIdle))
					.Build(),
				builder.CreateTransition()
					.WithSourceState(enteredMenu)
					.WithDestinationState(interogatedAccount)
					.WithStateEventArgs(new StateEventArgs((int)Event.InterogateAccount))
					.WithTransitionAction(this.OnInterogateAccount)
					.Build(),
				builder.CreateTransition()
					.WithSourceState(enteredMenu)
					.WithDestinationState(withdrawedMoney)
					.WithStateEventArgs(new StateEventArgs((int)Event.WithdrawMoney))
					.WithTransitionCondition(this.CanWithdrawAmount)
					.WithTransitionAction(this.OnWithdrawal)
					.Build(),
				builder.CreateTransition()
					.WithSourceState(enteredMenu)
					.WithDestinationState(insufficientAmount)
					.WithStateEventArgs(new StateEventArgs((int)Event.WithdrawMoney))
					.WithTransitionAction(this.OnInsufficientAmount)
					.Build(),
				builder.CreateTransition()
					.WithSourceState(interogatedAccount)
					.WithDestinationState(withdrawedMoney)
					.WithStateEventArgs(new StateEventArgs((int)Event.WithdrawMoney))
					.WithTransitionCondition(this.CanWithdrawAmount)
					.WithTransitionAction(this.OnWithdrawal)
					.Build(),
				builder.CreateTransition()
					.WithSourceState(interogatedAccount)
					.WithDestinationState(insufficientAmount)
					.WithStateEventArgs(new StateEventArgs((int)Event.WithdrawMoney))
					.WithTransitionAction(this.OnInsufficientAmount)
					.Build(),
				builder.CreateTransition()
					.WithSourceState(interogatedAccount)
					.WithDestinationState(idle)
					.WithStateEventArgs(new StateEventArgs((int)Event.EnterIdle))
					.Build(),
				builder.CreateTransition()
					.WithSourceState(insufficientAmount)
					.WithDestinationState(idle)
					.WithStateEventArgs(new StateEventArgs((int)Event.EnterIdle))
					.Build(),
				builder.CreateTransition()
					.WithSourceState(withdrawedMoney)
					.WithDestinationState(idle)
					.WithStateEventArgs(new StateEventArgs((int)Event.EnterIdle))
					.Build(),
				builder.CreateTransition()
					.WithSourceState(enteredMenu)
					.WithDestinationState(moneyDeposited)
					.WithStateEventArgs(new StateEventArgs((int)Event.RefillAtm))
					.WithTransitionAction(this.OnDeposit)
					.Build(),
				builder.CreateTransition()
					.WithSourceState(moneyDeposited)
					.WithDestinationState(idle)
					.WithStateEventArgs(new StateEventArgs((int)Event.EnterIdle))
					.Build()
			};
			return new AtmStateMachine(transitions, idle);
		}

		public void OnInterogateAccount()
		{
			Console.WriteLine();
			Console.WriteLine("Current Balance: " + this.userAccount.GetCurrentBalance());
		}

		public void OnWithdrawal()
		{
			var amount = ConfirmAmountAtCommandLine();
			this.atm.SubtractAmount(amount);
			this.userAccount.SubtractAmount(amount);
		}

		public bool CanWithdrawAmount()
		{
			var amount = GetAmountFromCommandLine();
			return (this.atm.GetCurrentBalance() >= amount && this.userAccount.GetCurrentBalance() >= amount);
		}

		public void OnDeposit()
		{
			var amount = GetAmountFromCommandLine();
			this.atm.AddAmount(amount);
			this.userAccount.AddAmount(amount);
		}

		public void OnInsufficientAmount()
		{
			Console.WriteLine("For the moment, the operation cannot be performed due to insufficient funds.");
		}

		private static int GetAmountFromCommandLine()
		{
			Console.WriteLine("Enter amount: ");
			int amount = Convert.ToInt32(Console.ReadLine());
			return amount;
		}

		private static int ConfirmAmountAtCommandLine()
		{
			Console.WriteLine("Confirm amount: ");
			int amount = Convert.ToInt32(Console.ReadLine());
			return amount;
		}
	}
}
