using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileControl : MonoBehaviour {
    private MapEditor editor;

    private Button button;

    public int x, y;
    public int status;

    public Sprite land, wall;

    void Awake()
    {
        editor = MapEditor.instance;

        button = gameObject.GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        editor.UpdateStatus(x, y, status);
    }

    public void SetState(int status)
    {
        if (this.status != status)
        {
            this.status = status;
            Invalidate();
        }
    }

    public void Invalidate()
    {
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
