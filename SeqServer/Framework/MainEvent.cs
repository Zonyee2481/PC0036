using Infrastructure;
using KAEventPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeqServer
{
    public class MainEvent : IEvent
    {
        private BaseFunc[] m_BaseSequence;
        private EventPool m_EventPool = new EventPool();

        BaseFunc[] IEvent.SetBaseSequence
        {
            set
            {
                m_BaseSequence = value;
            }
        }

        
        void IEvent.UITriggerEvent(EV_TYPE Event, EventArgs eventArgs)
        {
            m_EventPool.FireEvent(Event, eventArgs);
        }
        void IEvent.AddUISubscription(EventHandler seqEvent)
        {
            for (int i = 0; i < (int)TotalModule.MaxModule; i++)
            {
                m_BaseSequence[i].UI_Event += seqEvent;
            }
            //UI_Event += seqEvent;
        }        
        

        void IEvent.EventPubAndSub_UI2Seq()
        {
            PublishEvent();
            SubscribeEventOnInit();
            SubscribeEventOnBegin();

            SubscribeEventOnKill();
            SubscribeEventOnStopReq();
            SubscribeEventOnMCResumeReq();
            SubscribeEventOnRetryReq();
            SubscribeEventOnManualSeq();
            SubscribeEventOnTriggerStartBtn();
            SubscribeEventOnEndLot();
            //SubscribeEventOnReset();
        }

        private void PublishEvent()
        {
            EV_TYPE[] TotalEnum = (EV_TYPE[])Enum.GetValues(typeof(EV_TYPE));
            m_EventPool.Publish(TotalEnum);
        }

#region UI 2 Seq
        private void SubscribeEventOnInit()
        {
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_EventPool.Subscribe(EV_TYPE.InitSeq,
                    new EventHandler(m_BaseSequence[i].OnInit));
            }
        }        

        private void SubscribeEventOnBegin()
        {
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_EventPool.Subscribe(EV_TYPE.BeginSeq,
                    new EventHandler(m_BaseSequence[i].OnBegin));
            }
        }

        private void SubscribeEventOnKill()
        {
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_EventPool.Subscribe(EV_TYPE.Kill,
                    new EventHandler(m_BaseSequence[i].OnKill));
            }
        }

        private void SubscribeEventOnStopReq()
        {
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_EventPool.Subscribe(EV_TYPE.MCStopReq,
                    new EventHandler(m_BaseSequence[i].OnMCStopSeq));
            }
        }

        private void SubscribeEventOnMCResumeReq()
        {
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_EventPool.Subscribe(EV_TYPE.MCResumeSeq,
                    new EventHandler(m_BaseSequence[i].OnMCResumeSeq));
            }
        }

        private void SubscribeEventOnRetryReq()
        {
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_EventPool.Subscribe(EV_TYPE.RetryReq,
                    new EventHandler(m_BaseSequence[i].OnRetryReq));
            }
        }

        private void SubscribeEventOnManualSeq()
        {
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_EventPool.Subscribe(EV_TYPE.ManualSeq,
                    new EventHandler(m_BaseSequence[i].OnManualSeq));
            }
        }

        private void SubscribeEventOnTriggerStartBtn()
        {
            m_EventPool.Subscribe(EV_TYPE.WorkReq,
                new EventHandler(m_BaseSequence[(int)TotalModule.GeneralControl].OnWorkReq));
        }

        private void SubscribeEventOnEndLot()
        {
            m_EventPool.Subscribe(EV_TYPE.FarProcComp,
                new EventHandler(m_BaseSequence[(int)TotalModule.GeneralControl].OnFarProcComp));
        }

        //private void SubscribeEventOnReset()
        //{
        //    m_EventPool.Subscribe(EV_TYPE.WorkReq,
        //        new EventHandler(m_BaseSequence[(int)TotalModule.GeneralControl].OnNewItemReq));
        //}

        #endregion

#region Seq 2 Seq
        void IEvent.EventPubAndSub_SeqtoSeq()
        {
            PublishSeqEvent();
            Seq2SeqSubscription_Customize();
        }
        private void PublishSeqEvent()
        {
            EV_TYPE[] TotalEnum = (EV_TYPE[])Enum.GetValues(typeof(EV_TYPE));

            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                m_BaseSequence[i].m_SeqEventPool.Publish(TotalEnum);
            }            
        }
       
        private void Seq2SeqSubscription_Customize()
        {

            #region GeneralControl                                 

            #endregion

        }

        #endregion
        
        EventPool IEvent.GetEventPool_UI2Seq
        {
            get
            {
                return m_EventPool;
            }
        }
    }
}
