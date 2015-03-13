using UnityEngine;
using System.Collections;

public class CameraZoomScript : MonoBehaviour 
{
    [SerializeField]
    Transform _cameraTransform;

    [SerializeField]
    float _scrollSpeed = 50f;

    [SerializeField]
    int _scrollLimitMin = 0;

    [SerializeField]
    int _scrollLimitMax = 4;

    [SerializeField]
    float _nbScroll = 2f;

    [SerializeField]
    float _nbScrollDefault = 2f;

    [SerializeField]
    CharacterController _characterController;

    void LateUpdate()
    {
        float mouvement = Input.GetAxis("Mouse ScrollWheel");

        if (mouvement > 0)
        {
            if (_nbScroll <= _scrollLimitMax)
            {
                _characterController.Move(_cameraTransform.rotation * Vector3.forward * _scrollSpeed * mouvement);
                _nbScroll += mouvement;
            }
        }
        if (mouvement < 0)
        {
            if (_nbScroll >= _scrollLimitMin)
            {
                _characterController.Move(_cameraTransform.rotation * Vector3.forward * _scrollSpeed * mouvement);
                _nbScroll += mouvement;
            }
        }
    }

    //Fonction utilisé dans le script CameraResetOnCharacter
    public void resetNbScroll()
    {
        _nbScroll = _nbScrollDefault;
    }
}
