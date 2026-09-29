using Features.Interaction.Entities;
using Features.Interaction.InterfaceAdapters.Ports;

namespace Features.Interaction.InterfaceAdapters.Presenters
{
    public sealed class InteractableHighlightPresenter
    {
        private readonly IInteractableHighlightView _view;

        public InteractableHighlightPresenter(InteractableEntity entity, IInteractableHighlightView view)
        {
            _view = view;

            entity.Focused += _view.ShowFocus;
            entity.Unfocused += _view.Clear;
            entity.Grabbed += OnGrabbed;
            entity.Released += _view.Clear;
            entity.Threw += _view.Clear;
        }

        private void OnGrabbed()
        {
            _view.Clear();
            _view.ShowGrab();
        }
    }
}
