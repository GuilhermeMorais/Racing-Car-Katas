namespace TDDMicroExercises.TurnTicketDispenser
{
    public class TurnNumberSequence : ITurnNumberSequence
    {
        private int _turnNumber = 0;

        public int GetNextTurnNumber()
        {
            return _turnNumber++;
        }
    }
}
