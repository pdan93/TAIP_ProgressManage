
namespace PetriNets
{
    public class Done : State
    {
        public Done()
        {
            Name = "Done";
        }

        public void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Done();
        }
    }
}
