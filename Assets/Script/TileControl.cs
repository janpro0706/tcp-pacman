using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileControl : MonoBehaviour {
    private Button button;

    public int x, y;
    public int status;

    public Sprite land, wall;

    void Awake()
    {
        button = gameObject.GetComponent<Button>();

        button.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        status = (status + 1) % 2;

        switch (status)
        {
            case 0:
                gameObject.GetComponent<Image>().sprite = land;
                break;
            case 1:
                gameObject.GetComponent<Image>().sprite = wall;
                break;
        }
    }
}
