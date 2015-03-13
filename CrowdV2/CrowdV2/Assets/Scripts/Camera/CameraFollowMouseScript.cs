using UnityEngine;
using System.Collections;

public class CameraFollowMouseScript : MonoBehaviour 
{
	[SerializeField]
    Vector2 _mousePosition;

    [SerializeField]
    float _activeZoneBegin = 0.90f;

    [SerializeField]
    float _activeZoneEnd = 0.99f;

    [SerializeField]
    float _moveSpeed = 3f;

    [SerializeField]
    CharacterController m_characterManager;

    // Update is called once per frame
    void LateUpdate()
    {
        _mousePosition = Vector3.zero;
        _mousePosition.Set(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        if ((_mousePosition.x <= 1 - _activeZoneBegin) && (_mousePosition.x >= 1 - _activeZoneEnd))
        {
            m_characterManager.Move(Vector3.left * Time.deltaTime * _moveSpeed);
        }
        else if ((_mousePosition.x >= _activeZoneBegin) && (_mousePosition.x <= _activeZoneEnd))
        {
            m_characterManager.Move(Vector3.right * Time.deltaTime * _moveSpeed);
        }


        if ((_mousePosition.y <= 1 - _activeZoneBegin) && (_mousePosition.y >= 1 - _activeZoneEnd))
        {
            m_characterManager.Move(Vector3.back * Time.deltaTime * _moveSpeed);
        }
        else if ((_mousePosition.y >= _activeZoneBegin) && (_mousePosition.y <= _activeZoneEnd))
        {
            m_characterManager.Move(Vector3.forward * Time.deltaTime * _moveSpeed);
        }
    }
}
