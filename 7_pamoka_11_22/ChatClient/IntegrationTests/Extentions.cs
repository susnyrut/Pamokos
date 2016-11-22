using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IntegrationTests
{
    public class Extentions
    {
        public static void AssertThrows(Action a)
        {
            try
            {
                a();
                Assert.Fail("It should have thrown an exception.");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
