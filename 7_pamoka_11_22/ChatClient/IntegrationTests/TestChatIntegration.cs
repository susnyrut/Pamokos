using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;

namespace IntegrationTests
{
    [TestClass]
    public class TestChatIntegration
    {
        private ChatClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new ChatClient();
        }

        [TestMethod]
        public void Register_WhenCorrectPasswordIsProvided_RegisterMethodSucceeds()
        {
            _client.Register("testA", "testA");
            _client.Register("testB", "testB");
        }

        [TestMethod]
        public void Register_WhenIncorrectPasswordIsProvided_RegisterMethodThrowsAnException()
        {
            Extentions.AssertThrows(() => _client.Register("testA", "incorrect password"));
        }

        [TestMethod]
        public void SendMessage_WhenCorrectUserProvided_MessageIsSent()
        {
            _client.Register("testA", "testA");

            _client.SendMessage("testB", "This is my message!");

            _client.Register("testB", "testB");
            var messages = _client.GetAllMessages(1);
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual("testA", messages[0].From);
            Assert.AreEqual("This is my message!", messages[0].Text);
        }

        [TestMethod]
        public void SendMessage_LithuanianLettersAreSupported()
        {
            _client.Register("testA", "testA");
            _client.SendMessage("testB", "ĄČĘĖĮŠŲŪŽąčęėįšųūž");

            _client.Register("testB", "testB");
            var messages = _client.GetAllMessages(1);
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual("testA", messages[0].From);
            Assert.AreEqual("ĄČĘĖĮŠŲŪŽąčęėįšųūž", messages[0].Text);
        }
    }
}
