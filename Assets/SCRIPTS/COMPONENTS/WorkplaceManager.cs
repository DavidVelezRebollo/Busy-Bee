using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOM.Components.Workplaces
{
    abstract class Workplace {
        protected string name;
        protected string description;
        protected int honeyProduction;

        protected Sprite sprite;

        public abstract void Work();
    }

    public class WorkplaceManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
