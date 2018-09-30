// The cell class and the states enumeration

namespace GameOfLife
{
    /// <summary>
    /// Inhabitant of the universe
    /// </summary>
    public class Cell
    {
        #region ctor
        public Cell(State state = State.Dead)
        {
            if (state == State.Alive)
            {
                this.Generation = 1;
            }
            else
            {
                this.Generation = 0;
            }
            this.State = state;
        }
        #endregion

        public State State { get; set; }
        public int Generation { get; set; }

        public void UpdateState(State state = State.Dead)
        {
            if (state == State.Dead)
            {
                this.Generation = 0;
            }
            else
            {
                this.Generation++;
            }
            this.State = state;
        }
    }

    public enum State
    {
        Dead = 0,
        Alive = 1
    }
}