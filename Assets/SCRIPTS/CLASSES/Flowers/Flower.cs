using UnityEngine;

namespace GOM.Classes.Flowers {
    [CreateAssetMenu(fileName = "Flower", menuName = "Game/Flower")]
    public class Flower : ScriptableObject {
        public string Name;
        public Sprite Sprite;
        public int ProcessTime;
    }
}
