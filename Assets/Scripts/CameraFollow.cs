using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform LowerBound, UpperBound;
    [SerializeField]
    private Player player;

    // Update is called once per frame
    void Update()
    {
        float xDir = Mathf.Clamp(player.transform.position.x, LowerBound.position.x, UpperBound.position.x);
        float yDir = Mathf.Clamp(player.transform.position.y, LowerBound.position.y, UpperBound.position.y);

        transform.position = new Vector3(xDir, yDir, -10);

    }
}
