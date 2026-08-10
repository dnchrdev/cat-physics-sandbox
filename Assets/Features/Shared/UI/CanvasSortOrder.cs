using System;
using UnityEngine;

namespace Feature.Shared
{
    public class CanvasSortOrder : MonoBehaviour
    {
        [SerializeField] private int _sortOrder = 0;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            if (_canvas == null) throw new NullReferenceException("Not attached to canvas");
            _canvas.sortingOrder = _sortOrder;
        }
    }
}

