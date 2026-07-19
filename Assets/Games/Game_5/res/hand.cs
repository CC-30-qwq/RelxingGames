using UnityEngine;

public class hand : MonoBehaviour
{
    [Header("旋转设置")]
    public float targetAngle = 90f;    // 目标旋转角度（Z轴）
    public float maxAngle = 180f;      // 最大旋转角度
    public float speed;           // 旋转速度
    public float maxSpeed;
    public float minSpeed;
    public float lerp;
    private float originalAngle;       // 原始角度
    private float currentTargetAngle;  // 当前目标角度

    private Quaternion previousRotation;
    public float rotationSpeed; // 度/秒

    void Start()
    {
        originalAngle = transform.eulerAngles.z;
        previousRotation = transform.rotation;
    }

    void Update()
    {
        // 根据鼠标状态确定目标角度
        currentTargetAngle = Input.GetMouseButton(0) ?
            Mathf.Clamp(targetAngle, -maxAngle, maxAngle) :
            originalAngle;

        speed = Input.GetMouseButton(0) ? Mathf.Lerp(speed, maxSpeed, lerp * Time.deltaTime) : Mathf.Lerp(speed, minSpeed, lerp * Time.deltaTime);
        // 平滑旋转
        RotateTo(currentTargetAngle);
        calculateSpeed();
    }

    void RotateTo(float angle)
    {
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.Lerp(
            currentRot,
            targetRot,
            speed * Time.deltaTime
        );
    }

    void calculateSpeed()
    {
        // 获取旋转差值的角度
        float angleDifference = Quaternion.Angle(previousRotation, transform.rotation);

        // 确定旋转方向
        Vector3 cross = Vector3.Cross(previousRotation * Vector3.up, transform.rotation * Vector3.up);
        if (cross.z < 0) angleDifference = -angleDifference;

        // 计算角速度
        rotationSpeed = angleDifference / Time.deltaTime;

        // 更新上一帧的旋转
        previousRotation = transform.rotation;

        Debug.Log($"Transform旋转速度: {rotationSpeed} 度/秒");
    }
}
