using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookLibrary.EventStore;
using BookLibrary.EventStore.Aggregate;
using BookLibrary.Events;
using NUnit.Framework;

namespace BookLibrary.Tests.Unit.Domain
{
    public class When_applying_an_event_that_is_not_registered : AggregateRootTestFixture<TestAggregateRoot>
    {
        protected override void When()
        {
            AggregateRoot.DoSomethingIllegel();
        }

        [Then]
        public void Then_it_will_throw_an_unregistered_domain_event_exception()
        {
            Assert.AreEqual(typeof(UnregisteredDomainEventException), CaughtException.GetType());
        }

        [Then]
        public void Then_the_exception_will_have_this_message()
        {
            Assert.AreEqual("The requested domain event 'BookLibrary.Tests.Unit.Domain.UnRegisteredEvent' is not registered in 'BookLibrary.Tests.Unit.Domain.TestAggregateRoot'", CaughtException.Message);
        }
    }

    public class When_applying_an_event_that_is_registered : AggregateRootTestFixture<TestAggregateRoot>
    {
        protected override void When()
        {
            AggregateRoot.DoSomething();
        }

        [Then]
        public void Then_a_registeredevent_will_be_raised()
        {
            Assert.AreEqual(typeof(RegisteredEvent), AggregateRoot.GetAppliedEvents().First().GetType());
        }

        [Then]
        public void Then_the_event_version_will_be_incremented()
        {
            Assert.AreEqual(1, AggregateRoot.GetAppliedEvents().First().Version);
        }

        [Then]
        public void Then_the_correct_event_handler_will_be_raised_and_change_the_aggregates_state()
        {
            Assert.AreEqual("Handler raised correctly", AggregateRoot.TestProperty);
        }
    }

    public class When_replaying_historic_events : AggregateRootTestFixture<TestAggregateRoot>
    {
        IEnumerable<DomainEvent> historicEvents;

        protected override void When()
        {
            historicEvents = new List<DomainEvent>
            {
                new HistoricEvent()
            };

            AggregateRoot.LoadFromHistory(historicEvents);
        }

        [Then]
        public void The_historic_event_will_be_replayed_and_handler_run()
        {
            Assert.AreEqual("Historic Handler raised correctly", AggregateRoot.TestProperty);
        }

    }

    public class TestAggregateRoot : BaseAggregateRoot<IDomainEvent>
    {
        public string TestProperty { get; private set; }

        public TestAggregateRoot()
        {
            registerEvents();
        }

        public void DoSomethingIllegel()
        {
            Apply(new UnRegisteredEvent());
        }

        public void DoSomething()
        {
            Apply(new RegisteredEvent());
        }

        private void registerEvents()
        {
            RegisterEvent<RegisteredEvent>(onRegisteredEvent);
            RegisterEvent<HistoricEvent>(onHistoricEvent);
        }

        private void onRegisteredEvent(RegisteredEvent registerEvent)
        {
            TestProperty = "Handler raised correctly";
        }

        private void onHistoricEvent(HistoricEvent historicEvent)
        {
            TestProperty = "Historic Handler raised correctly";
        }
    }

    public class UnRegisteredEvent : DomainEvent { }

    public class RegisteredEvent : DomainEvent { }

    public class HistoricEvent : DomainEvent { }
}
