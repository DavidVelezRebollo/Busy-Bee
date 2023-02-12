using UnityEngine;
using System.Collections;
using GOM.Components.Flowers;
using GOM.Classes.Bees;

namespace GOM.Components.Workplaces {
    public abstract class Workplace : MonoBehaviour {
        protected string Name;
        protected string Description;
        protected int HoneyProduction;
        protected GameObject working_bee;
        protected Queue process_queue;
        protected float elapsed;

        protected Sprite Sprite;
        public void Start()
        {
            process_queue = new Queue();
            elapsed = 0f;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Honey"))
            {
                process_queue.Enqueue(collision);
                collision.gameObject.GetComponent<Flower>()._waiting = true;
            }
        }

        public void Update()
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 1)
            {
                elapsed = elapsed % 1f;
                Work();
            }
        }
        public void Work()
        {
            foreach (Flower flower in process_queue)
            {
                flower.process_time_passed += working_bee.GetComponent<Bee>().EffectiveWorkSpeed;
                if (flower.process_time_passed >= flower.process_time)
                {
                    process_queue.Dequeue();
                    flower._waiting = false;
                }
            }
        }

        public abstract void TransformPolen();
    }
}

