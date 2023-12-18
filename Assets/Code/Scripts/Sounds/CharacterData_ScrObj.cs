using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialog
{
    public class CharacterDataScrObj : MonoBehaviour
    {
        [CreateAssetMenu(fileName = "CharData", menuName = "Dialog/CharData")]
        public class Word : ScriptableObject
        {
            [SerializeField]
            [FormerlySerializedAs("Name")] [Tooltip("Who's data is this?")] 
            public string charName;
        
            [SerializeField]
            [FormerlySerializedAs("CharColor")] [Tooltip("Character color")]
            public Color charColor;
        
            [SerializeField]
            [FormerlySerializedAs("Alphabet")] [Tooltip("Character alphabet audio")] 
            public AlphabetScrObj.Alphabet charAlphabet;
            
        
            [FormerlySerializedAs("showPitch")]
            [SerializeField]
            [FormerlySerializedAs("Set character?")] [Tooltip("Is the character a set character?")]
            public bool setChar = true;
        
            [SerializeField] [HideIf("setChar")]
            [FormerlySerializedAs("Voice Pitch")] [Tooltip("Make the voice sound higher or lower")]
            [Range(0.5f, 2.0f)]
            public float pitch = 1.0f;
        }
    }
}