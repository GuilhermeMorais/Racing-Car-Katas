namespace TDDMicroExercises.TurnTicketDispenser
{
    public class TicketDispenser
    {
        private int _newTurnNumber;
        private readonly ITurnNumberSequence _turnNumberSequence;

        public TicketDispenser() : this(new TurnNumberSequence())
        {
        }

        public TicketDispenser(ITurnNumberSequence turnNumberSequence)
        {
            _turnNumberSequence = turnNumberSequence;
        }

        public TurnTicket GetTurnTicket()
        {
            GetNextTurnNumber();
            var newTurnTicket = new TurnTicket(_newTurnNumber);

            return newTurnTicket;
        }

        private void GetNextTurnNumber()
        {
            _newTurnNumber = _turnNumberSequence.GetNextTurnNumber();
        }
    }
}
