using UnityEngine;

public interface IWallInstantiable
{
    Transform WallPrefab { get; set; }
    Transform Instantiate(float x, float y, Transform parent, bool isVertical = false);
}