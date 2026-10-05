using UnityEngine;
using UnityEngine.UI;

public class UIClickSound : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlayClick());
    }
}
