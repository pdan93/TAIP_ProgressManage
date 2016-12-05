namespace PetriNets.States
{
    public class Fixed : State
    {
        public Fixed()
        {
            Name = "Fixed";
        }

        public void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Done();
        }
    }
}
