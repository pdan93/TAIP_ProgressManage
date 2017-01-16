using System;
using System.Collections.Generic;
using PetriNets;

namespace Workflow
{
    internal class TransitionData
    {
        public string Name { get; set; }
        public string PrevState { get; set; }
        public string NextState { get; set; }
        public string Precondition { get; set; }

        public static Transition ToTransition(TransitionData transitionData, SprintEntity sprintEntity,
            Dictionary<string, State> statesDictionary)
        {
            var preconditionMethodInfo = typeof(Preconditions).GetMethod(transitionData.Precondition);
            var precondition =
                (Func<SprintEntity, bool>)Delegate.CreateDelegate(typeof(Func<SprintEntity, bool>), preconditionMethodInfo);
            var nextState = statesDictionary[transitionData.NextState];
            return new Transition(sprintEntity, precondition, nextState);
        }
    }
}
