using Level.Player.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Level.UI {
    [RequireComponent(typeof(Text))]
    public class MovementsLeftTextBehaviour : MonoBehaviour {
        private Text _text;
        private PlayerData _player;

        private void Start() {
            _text = GetComponent<Text>();
            _player = FindObjectOfType<PlayerData>();
        }

        private void Update() {
            _text.text = _player.movementsLeft.ToString();
        }
    }
}