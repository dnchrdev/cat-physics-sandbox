using System.Collections.Generic;

namespace Features.Quests.InterfaceAdapters.Ports
{
    public interface IQuestHintsView
    {
        void Show(IReadOnlyList<IHintAnchor> targets, IReadOnlyList<IHintAnchor> tips);
        void Hide();
    }
}
