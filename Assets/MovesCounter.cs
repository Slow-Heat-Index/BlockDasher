using Level.Player.Data;
using TMPro;
using UnityEngine;

public class MovesCounter : MonoBehaviour {
    public TextMeshProUGUI DashText;


    private void Awake() {
        DashText = GetComponent<TextMeshProUGUI>();
        DashText.text = FindObjectOfType<PlayerData>().movements.ToString();
    }

    public void AddMovement(uint move) {
        DashText.text = move.ToString();
    }
}