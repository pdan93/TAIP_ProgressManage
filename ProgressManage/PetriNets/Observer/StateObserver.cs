namespace PetriNets.Observer
{
    public class StateObserver : IObserver
    {
        private State _state;
        public SprintEntity SprintEntity { get; set; }

        public StateObserver(SprintEntity sprintEntity)
        {
            this.SprintEntity = sprintEntity;
        }

        public void Update()
        {
            _state = SprintEntity.State;
        }
    }


}
