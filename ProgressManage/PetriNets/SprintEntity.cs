using PetriNets.Observer;

namespace PetriNets
{
    public class SprintEntity : ObserverSubject
    {
        public bool Implemented { get; set; }
        public bool Tested { get; set; }

        public SprintEntity(State state)
        {
            State = state;
        }

        public State State { get; set; }

        public virtual void Close() {}

        public virtual void MoveTo(State state) { }
    }
}
