using System;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers.DataTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "Vector2Data", menuName = "CookieHelpers/DataType/Vector2")]
    public class Vector2Data : ScriptableObject
    {
        [SerializeField]private Vector2 value;

        public void SetVector2(Vector2 value)
        {
            this.value = value;
        }

        public Vector2 GetVector2()
        {
            return value;
        }
    }
}
