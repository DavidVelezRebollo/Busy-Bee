using GOM.Shared;
using UnityEngine;

namespace GOM.Classes.Flowers {
    [CreateAssetMenu(fileName = "Flower", menuName = "Game/Flower")]
    public class Flower : ScriptableObject {
        public FlowerType Type;
        public GameObject PolenParticle;
        public Sprite[] Sprites;
        public float ProcessTime;
    }
}
