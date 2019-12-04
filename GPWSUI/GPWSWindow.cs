using System;
using UnityEngine;
using UnityEngine.UI;


namespace GPWSUI
{
    [RequireComponent(typeof(RectTransform))]
    public class GPWSWindow: MonoBehaviour{
        [SerializeField]
        private Toggle stallToggle = null;
        [SerializeField]
        private Toggle bankToggle = null;
        [SerializeField]
        private Toggle descentToggle = null;
        [SerializeField]
        private Toggle terrainToggle = null;

    }


}
