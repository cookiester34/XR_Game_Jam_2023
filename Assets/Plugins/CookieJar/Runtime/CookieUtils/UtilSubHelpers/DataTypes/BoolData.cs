using System;
using UnityEngine;

namespace CookieUtils.UtilSubHelpers.DataTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "BoolData", menuName = "CookieHelpers/DataType/Bool")]
    public class BoolData : ScriptableObject
    {
        public bool value = false;
    }
}
