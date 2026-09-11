using UnityEngine;

namespace Features.UI.Presentation.Buttons
{
    public class CheckBoxButton : ImageButton
    {
        [SerializeField] private GameObject _checked;

        public void SetChecked(bool active)
        {
            _checked.SetActive(active);
        }
    }
}
