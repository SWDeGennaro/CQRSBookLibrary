using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BookLibrary.Tests.Unit.Domain.Memento
{
    [TestFixture]
    public class All_memento_must_be_serializable
    {
        [Test]
        public void All_domain_mementos_will_have_the_Serializable_attribute_assigned()
        {
            var domainMementoTypes =
                typeof(global::BookLibrary.EventStore.Storage.Memento.IMemento)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(global::BookLibrary.EventStore.Storage.Memento.IMemento))
                .ToList();

            foreach (var mementoType in domainMementoTypes)
            {
                if (mementoType.IsSerializable)
                    continue;

                throw new Exception(string.Format("Domain memento '{0}' is not Serializable", mementoType.FullName));
            }
        }
    }
}
