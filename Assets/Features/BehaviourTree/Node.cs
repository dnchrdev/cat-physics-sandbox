using System.Collections.Generic;

namespace Feature.BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node
    {
        protected NodeState State;

        public Node parent;
        protected List<Node> Children = new();

        //private Dictionary<string, object> _dataContext = new();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (var child in children)
            {
                Attach(child);
            }
        }

        private void Attach(Node child)
        {
            child.parent = this;
            Children.Add(child);
        }

        public virtual NodeState Evaluate(float dt) => NodeState.FAILURE;

        //public void SetData(string key, object value)
        //{
        //    _dataContext[key] = value;
        //}

        //public object GetData(string key)
        //{
        //    object value = null;

        //    if (_dataContext.TryGetValue(key, out value))
        //    {
        //        return value;
        //    }

        //    Node node = parent;
        //    while (node != null)
        //    {
        //        value = node.GetData(key);
        //        if(value != null)
        //            return value;
        //        node = node.parent;
        //    }
        //    return null;
        //}

        //public bool ClearData(string key)
        //{
        //    if (_dataContext.ContainsKey(key))
        //    {
        //        _dataContext.Remove(key);
        //        return true;
        //    }

        //    Node node = parent;
        //    while (node != null)
        //    {
        //        bool cleared = node.ClearData(key);
        //        if (cleared)
        //            return true;
        //        node = node.parent;
        //    }
        //    return false;
        //}

    }
}