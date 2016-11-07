using System.Collections.Generic;

namespace PetriNets.Observer
{
    public abstract class ObserverSubject
    {
        private List<StateObserver> _observers = new List<StateObserver>();

        public void Attach(StateObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(StateObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (StateObserver o in _observers)
            {
                o.Update();
            }

        }
    }
}
