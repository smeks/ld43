using UnityEngine;

namespace Assets.Scripts
{
    public class Camera : MonoBehaviour
    {

        public GameObject Player;

        public Vector3 _followPosition = Vector3.zero;

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update ()
        {
            _followPosition.x = Player.transform.position.x;
            _followPosition.y = Player.transform.position.y;
            _followPosition.z = transform.position.z;

            transform.position = _followPosition;
        }
    }
}
