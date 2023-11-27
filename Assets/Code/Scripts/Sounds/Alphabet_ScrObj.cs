using UnityEngine;
using UnityEngine.Serialization;

namespace Dialog
{
    public class AlphabetScrObj : MonoBehaviour
    {
        [CreateAssetMenu(fileName = "Alphabet", menuName = "Dialog/Alphabet")]
        public class Alphabet : ScriptableObject
        {
            [FormerlySerializedAs("Description")] [Tooltip("Who's alphabet is this?")]
            public string description;

            [System.Serializable]
            public class Sounds
            {
                public AudioClip a;
                public AudioClip b;
                public AudioClip c;
                public AudioClip d;
                public AudioClip e;
                public AudioClip f;
                public AudioClip g;
                public AudioClip h;
                public AudioClip i;
                public AudioClip j;
                public AudioClip k;
                public AudioClip l;
                public AudioClip m;
                public AudioClip n;
                public AudioClip o;
                public AudioClip p;
                public AudioClip q;
                public AudioClip r;
                public AudioClip s;
                public AudioClip t;
                public AudioClip u;
                public AudioClip v;
                public AudioClip w;
                public AudioClip x;
                public AudioClip y;
                public AudioClip z;
            }

            public Sounds sounds;
        }
    }
}
