using System;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers.DataTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "Vector3Data", menuName = "CookieHelpers/DataType/Vector3")]
    public class Vector3Data : ScriptableObject
    {
        [SerializeField] private Vector3 value;

        public void SetVector3(Vector3 value)
        {
            this.value = value;
        }

        public Vector3 GetVector3()
        {
            return value;
        }
    }
}
