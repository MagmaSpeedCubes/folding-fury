using UnityEngine;

public class LevelButtonScript : MonoBehaviour
{
    [SerializeField] private int level; // Level number assigned in the Inspector
    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;
    public void OnMouseClick()
    {
        GameInfo.SelectedLevel = level;
    }

}