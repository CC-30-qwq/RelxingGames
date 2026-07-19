using UnityEngine;

public class RandomPositionAssigner : MonoBehaviour
{
    [Header("分配设置")]
    [SerializeField] private Transform[] objectsToAssign; // 要分配的10个物体
    [SerializeField] private Transform[] positions; // 两个位置（数组长度为2）

    void Start()
    {
        // 确保有10个物体和2个位置
        if (objectsToAssign.Length != 10)
        {
            Debug.LogError("需要恰好10个物体！");
            return;
        }

        if (positions.Length != 2)
        {
            Debug.LogError("需要恰好2个位置！");
            return;
        }

        AssignRandomPositions();
    }

    void AssignRandomPositions()
    {
        foreach (Transform obj in objectsToAssign)
        {
            // 随机选择0或1（两个位置）
            int randomIndex = Random.Range(0, 2);

            // 将物体移动到选中的位置
            obj.position = positions[randomIndex].position;

            // 如果需要记录每个物体的位置，可以添加到这里
            // Debug.Log($"{obj.name} 被分配到了位置 {randomIndex}");
        }

        Debug.Log("分配完成！");
    }
}