using System;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.UI
{
    [CreateAssetMenu(fileName = "Panels", menuName = "Configs/Panels")]
    public class PanelsConfig : ScriptableObject
    {
        [SerializeField] private List<PanelState> _panelsConfigs;

        public PanelState GetPanelConfig(PanelMode mode)
        {
            foreach (var config in _panelsConfigs)
            {
                if (config.PanelMode == mode)
                {
                    return config;
                }
            }
            
            throw new Exception("Panel not found");
        }
    }
}