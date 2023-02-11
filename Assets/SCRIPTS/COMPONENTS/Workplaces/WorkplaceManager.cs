using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOM
{
    abstract class Workplace {
        public abstract void Work();
    }

    class Polen : Workplace {
        private string name;
        private string description;
        private int honeyProduction;

        private Sprite sprite;

        public override void Work() {
            Debug.Log("Polen");
        }
    }

    class Crush : Workplace {
        private string name;
        private string description;
        private int honeyProduction;
        
        private Sprite sprite;

        public override void Work() {
            Debug.Log("Crush");
        }
    }

    class Mix : Workplace {
        private string name;
        private string description;
        private int honeyProduction;

        private Sprite sprite;
        
        public override void Work() {
            Debug.Log("Mix");
        }
    }

    class Bottling : Workplace {
        private string name;
        private string description;
        private int honeyProduction;

        private Sprite sprite;

        public override void Work() {
            Debug.Log("Bottling");
        }
    }

    class Cover : Workplace {
        private string name;
        private string description;
        private int honeyProduction;

        private Sprite sprite;

        public override void Work() {
            Debug.Log("Cover");
        }
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
