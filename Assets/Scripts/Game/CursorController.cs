using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Game
{
    public class CursorController : MonoBehaviour
    {
        private void Update()
        {
            //Temporary logic
            if (Input.GetMouseButtonDown(0))
            {
                SetActiveCursor(false);
            }
        }

        public void SetActiveCursor(bool isActive)
        {
            Cursor.visible = isActive;
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Confined;
        }
    }
}