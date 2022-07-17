using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public bool IsActive
    {
        get => Obj.activeInHierarchy;

        set => Obj.SetActive(value);
    }
    public Vector3 LocalPos
    {
        get => RectTrans.localPosition;
        set => RectTrans.localPosition = value;
    }
    public Vector3 LocalScale
    {
        get => RectTrans.localScale;
        set => RectTrans.localScale = value;
    }
    public Vector2 SizeDelta
    {
        get => RectTrans.sizeDelta;
        set => RectTrans.sizeDelta = value;
    }
    public float Width
    {
        get => RectTrans.rect.width;
    }
    protected GameObject Obj => _obj == null ? gameObject : _obj;
    protected RectTransform RectTrans => _rect == null ? GetComponent<RectTransform>() : _rect;
    private GameObject _obj;
    private RectTransform _rect;
}
