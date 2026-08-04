using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.UI
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
