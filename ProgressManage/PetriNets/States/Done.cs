
namespace PetriNets
{
    public class Done : State
    {
        public Done()
        {
            Name = "Done";
        }

        public override void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Done();
        }
    }
}
