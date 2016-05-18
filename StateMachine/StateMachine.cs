namespace StateMachine
{
	using System.Collections.Generic;
	using System.Linq;
	using global::StateMachine.Exception;

	public class StateMachine
	{
		public List<Transition.Transition> Transitions { get; private set; }

		public State CurrentState { get; private set; }

		public StateMachine(List<Transition.Transition> transitions, State currentState)
		{
			this.Transitions = transitions;
			this.CurrentState = currentState;
		}

		public void MoveNext(StateEventArgs firedEvent)
		{
			if (!this.ContainsValidTransition(firedEvent))
			{
				throw new InvalidStateException();
			}

			if (!this.TransitionHasCondition(firedEvent))
			{
				this.TryMoveTransitionWithoutCondition(firedEvent);
			}
			else
			{
				this.TryMoveTransition(firedEvent);
			}
		}

		private void TryMoveTransition(StateEventArgs firedEvent)
		{
			Transition.Transition conditionalTransition =
				this.Transitions.FirstOrDefault(
					transition =>
					Equals(transition.Source, this.CurrentState) && (transition.StateEventArgs.Id == firedEvent.Id) && transition.Condition != null);

			if (conditionalTransition != null && conditionalTransition.TryMove())
			{
				this.CurrentState = conditionalTransition.Destination;
			}
			else
			{
				Transition.Transition transition =
					this.Transitions.FirstOrDefault(
						t => Equals(t.Source, this.CurrentState) && (t.StateEventArgs.Id == firedEvent.Id) && t.Condition == null);

				if (transition != null && transition.TryMove())
				{
					this.CurrentState = transition.Destination;
				}
				else
				{
					throw new InvalidStateException();
				}
			}
		}

		private void TryMoveTransitionWithoutCondition(StateEventArgs firedEvent)
		{
			Transition.Transition transition = this.GetTransitionByEvent(firedEvent);
			if (transition.TryMove())
			{
				this.CurrentState = transition.Destination;
			}
			else
			{
				throw new InvalidStateException();
			}
		}

		private bool ContainsValidTransition(StateEventArgs firedEvent)
		{
			return
				this.Transitions.Any(
					transition => Equals(transition.Source, this.CurrentState)
						&& (transition.StateEventArgs.Id == firedEvent.Id));
		}

		private bool TransitionHasCondition(StateEventArgs firedEvent)
		{
			return
				this.Transitions
					.Count(transition => Equals(transition.Source, this.CurrentState)
						&& (transition.StateEventArgs.Id == firedEvent.Id)) == 2;
		}

		private Transition.Transition GetTransitionByEvent(StateEventArgs firedEvent)
		{
			return
				this.Transitions.FirstOrDefault(
					transition => Equals(transition.Source, this.CurrentState)
						&& (transition.StateEventArgs.Id == firedEvent.Id));
		}
	}
}

