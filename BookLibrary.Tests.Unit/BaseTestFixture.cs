using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;

namespace BookLibrary.Tests.Unit
{

    [Specification]
    public abstract class BaseTestFixture<TSubject>
    {
        protected TSubject SubjectUnderTest;
        protected Exception CaughtException;
        protected virtual void SetupDependencies() { }
        protected abstract void Given();
        protected abstract void When();
        protected virtual void Finally() { }

        [Given]
        public void SetUp()
        {
            CaughtException = new ThereWasNoExceptionButOneWasExpectedException();
            SetupDependencies();
            Given();

            try
            {
                When();
            }
            catch (Exception e)
            {
                CaughtException = e;
            }
            finally
            {
                Finally();
            }
        }
    }

    public class ThereWasNoExceptionButOneWasExpectedException : Exception { }

    public class GivenAttribute : SetUpAttribute { }

    public class ThenAttribute : TestAttribute { }

    public class SpecificationAttribute : TestFixtureAttribute { }
}
