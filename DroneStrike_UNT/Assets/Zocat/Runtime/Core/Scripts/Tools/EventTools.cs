using System;
using System.Collections;
using Opsive.Shared.Events;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;


namespace Zocat
{
    public static class EventTools
    {
        public static void RegisterEvent(EventType eventType, Action action)
        {
            EventHandler.RegisterEvent(eventType.ToString(), action);
        }

        public static void RegisterEvent(object obj, EventType eventType, Action action)
        {
            EventHandler.RegisterEvent(obj, eventType.ToString(), action);
        }

        public static void RegisterEvent<T1>(EventType eventType, Action<T1> action)
        {
            EventHandler.RegisterEvent(eventType.ToString(), action);
        }

        public static void RegisterEvent<T1>(object obj, EventType eventType, Action<T1> action)
        {
            EventHandler.RegisterEvent(obj, eventType.ToString(), action);
        }

        /*--------------------------------------------------------------------------------------*/
        public static void ExecuteEvent(EventType eventType)
        {
            EventHandler.ExecuteEvent(eventType.ToString());
        }

        public static void ExecuteEvent(object obj, EventType eventType)
        {
            EventHandler.ExecuteEvent(obj, eventType.ToString());
        }

        public static void ExecuteEvent<T1>(EventType eventType, T1 t1)
        {
            EventHandler.ExecuteEvent(eventType.ToString(), t1);
        }

        public static void ExecuteEvent<T1>(object obj, EventType eventType, T1 t1)
        {
            EventHandler.ExecuteEvent(obj, eventType.ToString(), t1);
        }
    }
}