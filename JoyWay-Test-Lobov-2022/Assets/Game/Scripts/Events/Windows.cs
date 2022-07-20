using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Events
{
    public struct WindowOpenedEvent : IEvent
    {
        public Type WindowType { get; private set; }

        public WindowOpenedEvent(Type windowType)
        {
            WindowType = windowType;
        }
    }

    public struct WindowClosedEvent : IEvent
    {
        public Type WindowType { get; private set; }

        public WindowClosedEvent(Type windowType)
        {
            WindowType = windowType;
        }
    }

    public struct AllWindowClosedEvent : IEvent
    {
    }
}

