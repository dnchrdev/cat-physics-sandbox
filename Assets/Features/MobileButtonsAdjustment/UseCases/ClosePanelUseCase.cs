using Features.MobileButtonsAdjustment.Entities;
using Features.Shared.Entities;
using Features.Shared.UseCases;

namespace Features.MobileButtonsAdjustment.UseCases
{
    public sealed class ClosePanelUseCase
    {
        private readonly ButtonLayoutEditSession _session;
        private readonly IStorageDataService _storage;
        private readonly IUIPanelsManager _panels;

        public ClosePanelUseCase(
            ButtonLayoutEditSession session,
            IStorageDataService storage,
            IUIPanelsManager panels)
        {
            _session = session;
            _storage = storage;
            _panels = panels;
        }

        public void Execute()
        {
            if (_session.HasChanges)
                _storage.Save();

            _session.End();
            _panels.OpenPanel(PanelMode.Gameplay);
        }
    }
}
