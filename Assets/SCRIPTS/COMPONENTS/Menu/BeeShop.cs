using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOM.Components.Bees;
using GOM.Components.Workplaces;
using UnityEngine.UI;

namespace GOM.Components.Menu
{
    public class BeeShop : MonoBehaviour
    {
        [SerializeField] private BeeComponent[] Bees;
        private int _beesSelected = 3;
        private WorkplaceManager _workplaceManager;

        private void Start()
        {
            _workplaceManager = WorkplaceManager.Instance;
        }

        public void OnBeeSelected(int index)
        {
            bool active = Bees[index].isActiveAndEnabled;

            if(active)
            {
                if (Bees[index].IsWorking()) return;
                _beesSelected--;
                Bees[index].gameObject.SetActive(false);
                _workplaceManager.DeleteBee(Bees[index]);
                Debug.Log("Abeja" + index + "desactivada");
            }
            else
            {
                if (_beesSelected >= 3) return;
                _beesSelected++;
                Bees[index].gameObject.SetActive(true);
                _workplaceManager.AddBee(Bees[index]);
                Debug.Log("Abeja" + index + "activada");
            }
        }
    }
}
