using System.Collections;
using UnityEngine;

namespace Feature.BehaviourTree
{ 
    public interface IBehaviourTree 
    {
        void Initialize(Node root);
        void Tick(float dt);
    }
}