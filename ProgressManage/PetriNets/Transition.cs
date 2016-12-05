using System;

namespace PetriNets
{
    public class Transition
    {
        public SprintEntity Entity { get; }
        public Func<SprintEntity, bool> Precondtion { get; }
        public State NextState { get;  }

        public Transition(SprintEntity entity, Func<SprintEntity, bool> precondtion, State nextState)
        {
            Entity = entity;
            Precondtion = precondtion;
            NextState = nextState;
        }

        public bool IsApplicable()
        {
            return Precondtion(Entity);
        }

        public void MoveToNext()
        {
            Entity.State = NextState;
        }
    }
}
