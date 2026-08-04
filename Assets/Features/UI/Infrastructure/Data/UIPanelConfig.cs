using System;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.UI
{
    [Serializable]
    public struct PanelState
    {
        public int Order;
        public UIPanelTag PanelTag;
        public bool CursorVisible;
        public CursorLockMode LockState;
        public bool Pause;
    }

    [Serializable]
    public enum UIPanelTag
    {
        Gameplay,
        Settings,
        Knockout,
        MobileButtonAdjustment,
        TutorialCompleted,
        AllQuests
    }

    [CreateAssetMenu(fileName = "GameplayPanelConfig", menuName = "SO/GameplayPanelConfig")]
    public class UIPanelConfig : ScriptableObject
    {
        [SerializeField] private List<PanelState> _configs;

        public PanelState GetPanelState(UIPanelTag tag)
        {
            foreach (var config in _configs)
            {
                if (config.PanelTag == tag)
                {
                    return config;
                }
            }
            return _configs[0];
        }
    }
}