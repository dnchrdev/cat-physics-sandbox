using UnityEngine;
using UnityEngine.EventSystems;

namespace Feature.BehaviourTree
{
    public class Tree /*: IBehaviourTree*/
    {
        private Node _root = null;

        private bool _isInitialized;

        public void Initialize(Node root)
        {
            _root = root;
            _isInitialized = true;
        }

        public void Tick(float dt)
        {       
            if (_root != null && _isInitialized)
            {
                _root.Evaluate(dt);
            }
        }

    }
}