namespace PetriNets.States
{
    public class New : State
    {
        public New()
        {
            Name = "New";
        }

        public void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new New();
        }
    }
}
