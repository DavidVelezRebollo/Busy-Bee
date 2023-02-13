using GOM.Shared;
using UnityEngine;

namespace GOM.Classes.Bees {
    [CreateAssetMenu(fileName = "Bee", menuName = "Game/Bee")]
    public class Bee : ScriptableObject {
        public string Name;
        public string Description;

        public Sprite BeeSprite;

        public WorkplaceType[] EffectiveWorkplaces;

        public int WorkSpeed = 1; 
        public int EffectiveWorkSpeed = 2;
    }
}
