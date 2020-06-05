using UnityEngine;

public class WallPrefab2d : IWallInstantiable
{
    private readonly Quaternion _quarterUp = Quaternion.Euler(new Vector3(0, 0, 90));
    private readonly Quaternion _identity = Quaternion.identity;
    public Transform WallPrefab { get; set; }
    public Transform Instantiate(float x, float y, Transform parent, bool isVertical = false)
    {
        return Object.Instantiate(WallPrefab, new Vector3(x, y), isVertical ? _quarterUp : _identity, parent);
    }
}
