using DG.Tweening;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public GameObject rig;
    [SerializeField] private float rotationDuration = 0.5f;
    public float orignDir;
    public bool rotate;
    public float forceStrangth;
    bool fire;
    float forceDir;

    private void Start()
    {
        fire = false;
    }

    private void Update()
    {
        if (!rotate)
        {
            transform.DORotate(new Vector3(0, 0, orignDir), rotationDuration)
         .SetEase(Ease.OutBack);
        }

        if (fire)
        {
            rig.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceDir, Vector2.up.y) * forceStrangth * Time.deltaTime);
        }
        else
        {
            forceDir = 0;
        }
    }

    // 使用DoTween旋转到指定角度
    public void RotateToAngleDoTween(float targetAngle)
    {
        transform.DORotate(new Vector3(0, 0, targetAngle), rotationDuration)
                 .SetEase(Ease.OutBack);
    }

    public void AddFireForce(float Dir)
    {
        fire = true;
        forceDir = Dir;
    }

    public void SetRotate(bool isRotate)
    {
        fire = isRotate;
        rotate = isRotate;
    }
}