using Mina.Core.Service;
using Mina.Core.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMinaDotNet
{
    class TimeServerHandler : IoHandlerAdapter
    {
        public override void ExceptionCaught(IoSession session, Exception cause)
        {
            Console.WriteLine(cause);
        }

        public override void MessageReceived(IoSession session, object message)
        {
            base.MessageReceived(session, message);
            Console.WriteLine(message);
            Console.WriteLine("숙소PC에서 코딩했어요!!");
        }

        public override void MessageSent(IoSession session, object message)
        {
            base.MessageSent(session, message);
        }

        public override void SessionClosed(IoSession session)
        {
            base.SessionClosed(session);
        }

        public override void SessionCreated(IoSession session)
        {
            base.SessionCreated(session);
        }

        public override void SessionIdle(IoSession session, IdleStatus status)
        {
            base.SessionIdle(session, status);
        }

        public override void SessionOpened(IoSession session)
        {
            base.SessionOpened(session);
        }
    }
}
