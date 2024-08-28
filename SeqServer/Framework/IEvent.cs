using KAEventPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeqServer
{
    public interface IEvent
    {       
        BaseFunc[] SetBaseSequence
        {
            set;
        }

        void EventPubAndSub_UI2Seq();
        void EventPubAndSub_SeqtoSeq();

        void UITriggerEvent(EV_TYPE Event, EventArgs EventArgs = null);

        void AddUISubscription(EventHandler seqEvent);

        EventPool GetEventPool_UI2Seq { get; }
        
    }
}
