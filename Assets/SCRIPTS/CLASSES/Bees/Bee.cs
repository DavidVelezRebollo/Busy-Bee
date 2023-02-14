using GOM.Shared;
using UnityEngine;

namespace GOM.Classes.Bees {
    [CreateAssetMenu(fileName = "Bee", menuName = "Game/Bee")]
    public class Bee : ScriptableObject {
        public string Name;
        public string Description;
        public string SFX;

        public Sprite BeeSprite;

        public WorkplaceType EffectiveWorkplace;

        public float WorkSpeed = 1; 
        public float EffectiveWorkSpeed = 2;
    }
}
