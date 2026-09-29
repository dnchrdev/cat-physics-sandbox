using Features.Shared.Entities;

namespace Features.Shared.UseCases
{
    public interface ICursorController
    {
        void Apply(bool isVisible, CursorLockMode lockMode);
    }
}
