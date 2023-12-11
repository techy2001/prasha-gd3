using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GD.Examples
{
    [CreateAssetMenu(fileName = "Levels",
    menuName = "DkIT/Scriptable Objects/Game/Levels")]
    public class Levels : SerializedScriptableObject
    {
        public Dictionary<string, List<Object>> Contents = new Dictionary<string, List<Object>>();
    }
}