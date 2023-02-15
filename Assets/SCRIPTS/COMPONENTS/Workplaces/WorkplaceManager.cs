using GOM.Components.Bees;
using GOM.Shared;
using UnityEngine;
using System;

namespace GOM.Components.Workplaces {
    public class WorkplaceManager : MonoBehaviour {
        #region Singleton

        public static WorkplaceManager Instance;

        private void Awake() {
            if (Instance != null) return;

            Instance = this;
        }

        #endregion

        [SerializeField] private Workplace[] Workplaces;

        private void Start() {
            int i = 0;

            foreach(Workplace workplace in Workplaces) {
                workplace.SetWorkplaceIndex(i);
                i++;
            }
        }

        public Workplace GetWorkplace(int index) { return Workplaces[index]; }

        public int WorkplaceCount() { return Workplaces.Length; }

        public WorkplaceType GetWorkplaceType(int index) { return Workplaces[index].GetWorkplaceType(); }

        public bool HasBee(int index) { return Workplaces[index].HaveBee(); }

        public void SetBee(BeeComponent bee, int index) {
            int i = 0, beeIndex = 0;
            bool found = false;

            while(!found && i < Workplaces.Length) {
                if(bee == Workplaces[i].GetWorkingBee()) {
                    beeIndex = i;
                    found = true;
                }

                i++;
            }

            if (!found) {
                Debug.LogError("Bee not found");
                return;
            }

            Workplaces[beeIndex].SetWorkingBee(null);

            Workplaces[index].SetWorkingBee(bee);
        }

        public void DeleteBee(BeeComponent bee)
        {
            foreach (Workplace w in Workplaces)
            {
                if (w.GetWorkingBee() == bee)
                {
                    w.SetWorkingBee(null);
                    return;
                }
            }
        }

        public void AddBee(BeeComponent bee)
        {
            foreach (Workplace w in Workplaces)
            {
                if (w.GetWorkingBee() == null)
                {
                    w.SetWorkingBee(bee);
                    return;
                }
            }
        }
    }
}
