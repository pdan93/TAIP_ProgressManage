namespace PetriNets
{
    public abstract class State
    {
        public string Name { get; set; }

        public abstract void Handle(SprintEntity sprintEntity);
    }
}
