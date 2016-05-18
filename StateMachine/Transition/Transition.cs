namespace StateMachine.Transition
{
	public delegate void TransitionAction();

	public delegate bool TransitionCondition();

	public class Transition
	{
		public TransitionAction Action { get; set; }

		public TransitionCondition Condition { get; set; }

		public State Source { get; set; }

		public StateEventArgs StateEventArgs { get; set; }

		public State Destination { get; set; }

		public bool TryMove()
		{
			if (this.Condition != null)
			{
				if (!this.Condition())
				{
					return false;
				}
			}
			if (this.Action!=null) this.Action();
			return true;
		}
	}
}
