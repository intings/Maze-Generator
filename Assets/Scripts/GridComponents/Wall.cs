using UnityEngine;

public class Wall
{
    public bool IsEdge { get; private set; }
    public bool IsDeleted { get; private set; }
    private readonly Transform transform;
    public Wall(Transform transform, bool isEdge)
    {
        this.transform = transform;
        IsEdge = isEdge;
    }
    
    
    public void DeleteWall(bool force = false)
    {
        if (force) IsEdge = false;
        if (IsDeleted || IsEdge) return;
        IsDeleted = true;
        Object.Destroy(transform.gameObject);
    }
}
