using IdleMachinery.Extensions.Framework.Tests.Stubs.WcfService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IdleMachinery.Extensions.Framework.Tests
{
    [TestClass]
    [Ignore("Build/run the WcfService stub and remove this attribute before running these tests")]
    public class WcfClientTests
    {
        [TestMethod]
        public void CallWcf()
        {
            var response = string.Empty;
            using (var client = new Service1Client())
            {
                response = client.GetData(10);
            }
            Assert.AreEqual("You entered: 10", response);
        }

        [TestMethod]
        public void CallWcfWithException_InUsingBlock()
        {
            var response = new CompositeType();
            using (var client = new Service1Client())
            {
                try
                {
                    response = client.GetDataUsingDataContract(null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.FullMessage());
                }

            }
        }

        [TestMethod]
        public void CallWcfWithException()
        {
            var response = new CompositeType();
            var client = new Service1Client();
            try
            {
                response = client.GetDataUsingDataContract(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.FullMessage());
            }
            finally
            {
                ((IDisposable)client).Dispose();
            }
        }

        // Uncomment the wsHttpBinging in the WcfService stub, then rebuild & 
        // update service reference in Tests to introduce error
        [TestMethod]
        public void CallWcfWithDisposeSafely()
        {
            var response = new CompositeType();
            var client = new Service1Client();
            try
            {
                response = client.GetDataUsingDataContract(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.FullMessage());
            }
            finally
            {
                client.DisposeSafely();
            }
        }
    }
}
