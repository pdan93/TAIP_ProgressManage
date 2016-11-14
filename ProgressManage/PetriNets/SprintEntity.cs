using PetriNets.Observer;

namespace PetriNets
{
    public class SprintEntity : ObserverSubject
    {
        public SprintEntity(State state)
        {
            State = state;
        }

        public State State { get; set; }

        public void Request()
        {
            State.Handle(this);
        }

        public virtual void Close() {}

        public virtual void MoveTo(State state) { }
    }
}
