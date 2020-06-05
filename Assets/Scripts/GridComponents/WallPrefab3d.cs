using UnityEngine;

public class WallPrefab3d : IWallInstantiable
{
    private readonly Quaternion _quarterUp = Quaternion.Euler(new Vector3(0, 90, 90));
    private readonly Quaternion _identity = Quaternion.identity;
    public Transform WallPrefab { get; set; }
    public Transform Instantiate(float x, float y, Transform parent, bool isVertical = false)
    {
        if (isVertical)
            y += 0.5f;
        else
            x += 0.5f;
        return Object.Instantiate(WallPrefab, new Vector3(x, 0, y), isVertical ? _quarterUp : _identity, parent);
    }
}
