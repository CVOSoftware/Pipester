using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

using Pipester.Test.Message;

namespace Pipester.Test.Case
{
    public class PingPongTestCase
    {
        [Fact]
        public void TestMessaging()
        {
            var ping = false;
            var pong = false;
            var tokenSource = new CancellationTokenSource();
            var input = Guid.NewGuid();
            var output = Guid.NewGuid();

            var firstConnector = new Connector(input, output);
            var secondConnector = new Connector(output, input);

            firstConnector.Connect();
            secondConnector.Connect();

            firstConnector.Subscriber.Subscribe<PongMessage>(message =>
            {
                pong = true;
                tokenSource.Cancel();
            });

            secondConnector.Subscriber.Subscribe<PingMessage>(message =>
            {
                ping = true;
                secondConnector.Sender.Send(new PongMessage());
            });

            firstConnector.Sender.Send(new PingMessage());

            while(tokenSource.IsCancellationRequested == false)
            {
                // wait...
            }

            Assert.True(ping);
            Assert.True(pong);
        }
    }
}
