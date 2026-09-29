using System;
using System.Collections.Generic;
using Features.Shared.Entities;
using Features.UI.UseCases.Ports;
using UnityEngine;

namespace Features.UI.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "Panels", menuName = "Configs/Panels")]
    public class PanelsConfig : ScriptableObject, IPanelsConfigProvider
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
            throw new Exception($"Panel config for mode '{mode}' not found");
        }
    }
}
