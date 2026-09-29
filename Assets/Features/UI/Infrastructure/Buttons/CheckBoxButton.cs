using UnityEngine;

namespace Features.UI.Infrastructure.Buttons
{
    public class CheckBoxButton : ImageButton
    {
        [SerializeField] private GameObject _checkedCoverImage;

        public void SetChecked(bool active)
        {
            _checkedCoverImage.SetActive(active);
        }
    }
}
