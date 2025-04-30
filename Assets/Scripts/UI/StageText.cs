using UnityEngine;
using TMPro;

public class StageText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
    
    void Start()
    {
          m_Object.text = "Select a Stage";
    }
    void Update()
    {
        if (GameInfo.SelectedLevel > 0)
        {
            m_Object.text = "Stage " + GameInfo.SelectedLevel;
        }
    }
}