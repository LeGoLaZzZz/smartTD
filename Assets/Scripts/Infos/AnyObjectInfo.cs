using Units;
using UnityEngine;

namespace Infos
{
    public abstract class AnyObjectInfo : ScriptableObject
    {
    }

    public abstract class AnyObjectInfo<TObject, TObjectEnumType> : AnyObjectInfo
        where TObject : AnyObject
    {

        public abstract void SetParametersTo(TObject objectScript, int level = 0);
        
        
        
        public TObject prefab;
        public TObjectEnumType enumType;
    }
}