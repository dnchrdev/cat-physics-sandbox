using Feature.Core;
using System.Collections;
using UnityEngine;

namespace Feature.Shared
{
    public interface ITarget
    {
        Result RecieveHit(AttackInfo attackInfo);
    }
}