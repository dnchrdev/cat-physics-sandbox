using System.Collections;
using UnityEngine;

namespace Feature.Core
{
    public sealed class DestroyService : MonoBehaviour
    {
        public void Destroy(GameObject obj)
        {
            Object.Destroy(obj);
        }
        
        public void Destroy(Joint joint)
        {
            Object.Destroy(joint);
        }
    }
}