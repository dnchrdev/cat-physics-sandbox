using Cysharp.Threading.Tasks;
using Feature.Core;
using Feature.Scene;
using System;
using System.Collections;
using UnityEngine;

namespace Feature.MainMenu
{
    public interface IView
    {
        event Action StartGameButtonClicked;
        event Action<bool> IsMobileControlChosedEvent;
        void ShowControlChosePanel(bool active);
    }
}