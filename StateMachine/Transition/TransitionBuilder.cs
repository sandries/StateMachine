namespace StateMachine.Transition
{
	public class TransitionBuilder
	{
		private Transition transition;

		public TransitionBuilder CreateTransition()
		{
			this.transition = new Transition();
			return this;
		}

		public TransitionBuilder WithTransitionCondition(TransitionCondition transitionCondition)
		{
			this.transition.Condition = transitionCondition;
			return this;
		}
		public TransitionBuilder WithTransitionAction(TransitionAction transitionAction)
		{
			this.transition.Action = transitionAction;
			return this;
		}

		public TransitionBuilder WithStateEventArgs(StateEventArgs stateEventArgs)
		{
			this.transition.StateEventArgs = stateEventArgs;
			return this;
		}

		public TransitionBuilder WithSourceState(State state)
		{
			this.transition.Source = state;
			return this;
		}

		public TransitionBuilder WithDestinationState(State state)
		{
			this.transition.Destination = state;
			return this;
		}

		public Transition Build()
		{
			return this.transition;
		}
	}
}
