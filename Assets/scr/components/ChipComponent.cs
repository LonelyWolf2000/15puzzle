using System.Collections;
using System.Collections.Generic;
using puzzle15;
using UnityEngine;

namespace puzzle15
{
    public class ChipComponent : MonoBehaviour
    {
        public IChip Chip;
        public IGameController GameController;
        private Vector3 startPosition;

        void Start()
        {
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        void OnMouseDown()
        {
            //Debug.Log("Click on " + Chip.Value);
            GameController.OnClickChip(transform, Chip);
        }

        public void ResetPosition()
        {
            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
        }
    }
}