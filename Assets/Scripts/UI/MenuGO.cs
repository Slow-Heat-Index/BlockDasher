using System;
using UnityEngine;

namespace UI {
    public class MenuGO : MonoBehaviour {
        public static MenuGO Instance { get; private set; }

        private void Start() {
            Instance = this;
        }
    }
}