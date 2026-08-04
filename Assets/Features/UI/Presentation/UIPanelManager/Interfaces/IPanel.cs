using System;
using System.Collections.Generic;

namespace Feature.UI
{
    public interface IPanel
    {
        void InitPanel();
        List<UIPanelTag> PanelTags { get; }
        void OnEnterPanel();
        void OnExitPanel();
    }
}