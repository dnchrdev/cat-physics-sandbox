using System;
using System.Collections;
using UnityEngine;

namespace Feature.Input
{
    public interface IInteractionInput
    {
        public bool IsHit { get; }
        public bool IsGrab { get; }
        public bool IsThrow { get; }
        public bool IsRelease { get; }

        public void InvokeHit();
        public void InvokeGrab();
        public void InvokeThrow();
        public void InvokeRelease();
    }
}