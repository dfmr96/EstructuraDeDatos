namespace Assets.Scripts.Estructura_de_Datos
{
    using System;

    public class EventNode : IComparable<EventNode>
    {
        public float TriggerTime { get; set; }
        public Action EventAction { get; set; }

        public EventNode(float triggerTime, Action eventAction)
        {
            TriggerTime = triggerTime;
            EventAction = eventAction;
        }

        public int CompareTo(EventNode other)
        {
            return TriggerTime.CompareTo(other.TriggerTime);
        }
    }

}