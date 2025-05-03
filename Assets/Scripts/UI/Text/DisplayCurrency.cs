using UnityEngine;
using TMPro;
public class DisplayCurrency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI paperclips;
    [SerializeField] private TextMeshProUGUI cards;

    void Update(){
        paperclips.text = "Paperclips: " + AvatarInfo.Paperclips;
        cards.text = "Cards: " + AvatarInfo.Cards;
    }
}
