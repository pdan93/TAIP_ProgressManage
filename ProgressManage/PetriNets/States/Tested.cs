namespace PetriNets.States
{
    public class Tested : State
    {
        public Tested()
        {
            Name = "Tested";
        }

        public void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Tested();
        }
    }
}
