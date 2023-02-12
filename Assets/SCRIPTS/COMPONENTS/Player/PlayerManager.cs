using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOM.Components.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public float TimeSurvived;
        public int LavanderHoney;
        public int StrawberryTreeHoney;
        public int HexagonsFilled;

        private void Start()
        {
            TimeSurvived = 0f;
            LavanderHoney = 0;
            StrawberryTreeHoney = 0;
            HexagonsFilled = 0;
        }
    }
}
