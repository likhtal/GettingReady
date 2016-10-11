using System;
using System.Collections.Generic;
using System.Linq;

namespace GettingReady
{
    public abstract class StateSet 
    {
        private HashSet<string> set = new HashSet<string>();

        public StateSet() { }

        public abstract HashSet<string> Allowed { get; }

        public StateSet(IEnumerable<string> states) 
        {
            set = new HashSet<string>(states);
            if (!this.set.IsSubsetOf(Allowed)) throw new ArgumentException("You are passing states that are not allowed.");
        }

        public virtual bool Add(string token)
        {
            if (!Allowed.Contains(token)) throw new ArgumentException("You are adding state that are not allowed.");
            return set.Add(token);
        }

        public virtual bool Remove(string token)
        {
            if (!Allowed.Contains(token)) throw new ArgumentException("You are removing state that are not allowed.");
            return set.Remove(token);
        }

        public virtual bool Contains(string token)
        {
            if (!Allowed.Contains(token)) throw new ArgumentException("You are checking for state that are not allowed.");
            return set.Contains(token);
        }

        public bool IsSubsetOf(StateSet states)
        {
            return this.set.IsSubsetOf(states.set);
        }

        public bool IsSupersetOf(StateSet states)
        {
            return this.set.IsSupersetOf(states.set);
        }

        public bool Overlaps(StateSet states)
        {
            return this.set.Overlaps(states.set);
        }

        public bool ContainsAll(StateSet states)
        {
            return states.IsSubsetOf(this);
        }

        public bool ContainsAny(StateSet states)
        {
            return Overlaps(states);
        }

        public void Clear()
        {
            this.set.Clear();
        }
    }
}
