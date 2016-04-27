using System;
using System.Collections.Generic;
using Zenject;
using NUnit.Framework;
using System.Linq;
using ModestTree;
using Assert=ModestTree.Assert;

namespace Zenject.Tests.Conditions
{
    [TestFixture]
    public class TestConditionsTarget : TestWithContainer
    {
        class Test0
        {
        }

        class Test1
        {
            public Test1(Test0 test)
            {
            }
        }

        class Test2
        {
            public Test2(Test0 test)
            {
            }
        }

        public override void Setup()
        {
            base.Setup();
            Container.Bind<Test0>().AsSingle().When(r => r.ObjectType == typeof(Test2));
        }

        [Test]
        public void TestTargetConditionError()
        {
            Container.Bind<Test1>().AsSingle().NonLazy();

            Assert.Throws(() => Container.Validate());

            Assert.Throws(
                delegate { Container.Resolve<Test1>(); });
        }

        [Test]
        public void TestTargetConditionSuccess()
        {
            Container.Bind<Test2>().AsSingle().NonLazy();

            Container.Validate();

            var test2 = Container.Resolve<Test2>();

            Assert.That(test2 != null);
        }
    }
}


