using UnityEngine;

public class Rotation : ExtraComponent
{
    // public float Speed = 1;

    public Vector3 Direction = new(0, 0, -.2f);
    private bool _enabled;

    // Start is called before the first frame update
    private void Start()
    {
        _enabled = true;
    }

    private void FixedUpdate()
    {
        if (!_enabled) return;
        transform.Rotate(Direction, Space.Self);
    }

    public void Stop()
    {
        _enabled = false;
    }

    public void Restart()
    {
        _enabled = true;
    }
}