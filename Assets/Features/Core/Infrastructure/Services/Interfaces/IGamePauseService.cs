using System.Collections;
using UnityEngine;

namespace Feature.Core
{
    public interface IGamePauseService
    {
        public bool Paused { get; }
    }
}