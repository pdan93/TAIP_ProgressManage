using PetriNets.Observer;

namespace PetriNets
{
    public class SprintEntity : ObserverSubject
    {
        private State _state;

        public SprintEntity(State state)
        {
            this.State = state;
        }

        public State State { get; set; }

        public void Request()
        {
            _state.Handle(this);
        }


    }
}
