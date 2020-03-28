using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infos;
using Units;
using UnityEngine;

namespace Spawners
{
    public abstract class AnyObjectSpawner<TObject, TObjectEnumType, TObjectInfo> : MonoBehaviour
        where TObjectInfo : AnyObjectInfo<TObject, TObjectEnumType>
        where TObject : AnyObject
    {
        public abstract T Spawn<T>(TObjectEnumType type) where T : TObject;


        protected Dictionary<TObjectEnumType, TObjectInfo> ObjectDict = new Dictionary<TObjectEnumType, TObjectInfo>();
    }
}