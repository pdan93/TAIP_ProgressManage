namespace PetriNets.States
{
    public class InProgress : State
    {
        public InProgress()
        {
            Name = "InProgress";
        }

        public void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new InProgress();
        }
    }
}
