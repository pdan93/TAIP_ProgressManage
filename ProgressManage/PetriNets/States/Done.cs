
namespace PetriNets
{
    public class Done : State
    {
        public override void Handle(SprintEntity sprintEntity)
        {
            sprintEntity.State = new Done();
        }
    }
}
