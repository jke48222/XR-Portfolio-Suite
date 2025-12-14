using NUnit.Framework;
using XrShared.Core.Services;

namespace XrShared.Tests.Runtime
{
    public class XrSharedRuntimeTests
    {
        [Test]
        public void ServiceLocator_RegisterResolve_Works()
        {
            var sl = new ServiceLocator();
            sl.Register<string>("ok");
            Assert.AreEqual("ok", sl.Resolve<string>());
        }
    }
}
