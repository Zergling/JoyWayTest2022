using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Basic;
using UnityEngine;

namespace Game.Scripts.Events
{
    public struct CreatureValuesChangedEvent: IEvent
    {
        public CreatureController CreatureController { get; private set; }
        public int NewHPValue { get; private set; }
        public int NewWetValue { get; private set; }

        public CreatureValuesChangedEvent(CreatureController creatureController, int newHP, int newWet)
        {
            CreatureController = creatureController;
            NewHPValue = newHP;
            NewWetValue = newWet;
        }
    }

    public struct CreatureParticleCollisionEvent: IEvent
    {
        public CreatureController ThisCreatureController { get; private set; }
        public int FromInstanceId { get; private set; }

        public CreatureParticleCollisionEvent(CreatureController thisCreatureController, int fromInstanceId)
        {
            ThisCreatureController = thisCreatureController;
            FromInstanceId = fromInstanceId;
        }
    }
}

