using UnityEngine;

    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private void Start()
        {
            Destroy(gameObject, 15f);
        }

        private void Update()
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }



}  