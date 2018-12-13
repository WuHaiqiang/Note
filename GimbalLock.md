### 解决unity中万向节锁问题
```
public Vector3 ShowRotationLikeInspector(Transform t)
    {
        var type = t.GetType();
        var mi = type.GetMethod("GetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrder = rotationOrderPro.GetValue(t, null);
        var EulerAnglesInspector = mi.Invoke(t, new[] {rotationOrder});
        return (Vector3) EulerAnglesInspector;
    }

    public void SetRotationLikeInspector(Transform t, Vector3 v)
    {
        var type = t.GetType();
        var mi = type.GetMethod("SetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrder = rotationOrderPro.GetValue(t, null);
        mi.Invoke(t, new[] {v, rotationOrder});
    }
```
