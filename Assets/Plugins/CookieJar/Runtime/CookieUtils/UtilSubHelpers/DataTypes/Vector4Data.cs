using System;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers.DataTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "Vector4Data", menuName = "CookieHelpers/DataType/Vector4")]
    public class Vector4Data  : ScriptableObject
    {
        [SerializeField]private Vector4 value;

        public void SetVector4(Vector4 value)
        {
            this.value = value;
        }

        public Vector4 GetVector4()
        {
            return value;
        }
    }
}
