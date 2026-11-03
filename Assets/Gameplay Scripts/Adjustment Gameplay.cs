using Unity.VisualScripting;
using UnityEngine;

public class AdjustmentGameplay : MonoBehaviour
{
    public enum ScreenSide {  Left, Right };

    [Header("ScreenAdjust")]
    [Tooltip("Screen side adjustment")]
    public ScreenSide screenside;

    [Tooltip("positive = insidescreen, negative = outside screen")]

    public float margen = 1f;


    void Start()
    {
        if (Camera.main == null) return;

        float viewportX = screenside == ScreenSide.Left ? 0f : 1f;

        Vector3 worldlimit = Camera.main.ViewportToScreenPoint(new Vector3(viewportX, 0f, Mathf.Abs(Camera.main.transform.position.z)));

        float newPosX = screenside == ScreenSide.Left ? worldlimit.x + margen : worldlimit.x - margen;

        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }
}
