using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;

namespace BookLibrary.Tests.Unit
{
    [Specification]
    public abstract class AggregateRootTestFixture<TAggregateRoot> where TAggregateRoot 
        : BaseAggregateRoot<IDomainEvent>, new()
    {
        protected TAggregateRoot AggregateRoot;
        protected IEnumerable<IDomainEvent> PublishedEvents;
        protected Exception CaughtException;

        public AggregateRootTestFixture()
        {

        }
        protected virtual IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }
        protected virtual void Finally() { }
        protected abstract void When();

        [Given]
        public void Setup()
        {
            CaughtException = new ThereWasNoExceptionButOneWasExpectedException();
            AggregateRoot = new TAggregateRoot();
            AggregateRoot.LoadFromHistory(Given());

            try
            {
                When();
                PublishedEvents = AggregateRoot.GetAppliedEvents();
            }
            catch (Exception exception)
            {
                CaughtException = exception;
            }
            finally
            {
                Finally();
            }
        }
    }
}
