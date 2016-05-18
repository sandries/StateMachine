namespace StateMachine
{
	using System;

	public class StateEventArgs : EventArgs
	{
		private readonly int eventId;

		public StateEventArgs(int id)
		{
			this.eventId = id;
		}
		
		public int Id
		{
			get { return this.eventId; }
		}
	}
}