using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Creatures.Player;
using UnityEngine;
using ZerglingPlugins.Windows;

namespace Game.Scripts.UI.Windows.HUD
{
    public class HUDWindow : Window<HUDWindowSetup>
    {
        private PlayerController _playerController;
        
        public override void Setup(HUDWindowSetup setup)
        {
            _playerController = setup.PlayerController;
        }
    }
}

