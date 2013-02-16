using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore.Storage.Memento;

namespace BookLibrary.EventStore.Storage.Mementos
{
    /// <summary>
    /// Used to create snapshots of objects
    /// this allows the ability to rollback to certain
    /// points in time
    /// </summary>
    public interface IOriginator
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}
