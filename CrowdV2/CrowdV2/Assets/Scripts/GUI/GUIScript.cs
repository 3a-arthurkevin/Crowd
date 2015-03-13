using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour 
{
    [SerializeField]
    HoundMoveScript _houndMoveScriptArmyA;

    [SerializeField]
    HoundMoveScript _houndMoveScriptArmyB;

    [SerializeField]
    CameraFollowMouseScript _cameraFollowMouseScript;

    [SerializeField]
    CameraZoomScript _cameraZoomScript;


    [SerializeField]
    GameObject _runButton;

    [SerializeField]
    GameObject _closeButton;

    [SerializeField]
    GameObject _restartButton;

    [SerializeField]
    GameObject _minusUnitButton;

    [SerializeField]
    GameObject _plusUnitButton;

    [SerializeField]
    Text _unitLabel;

    [SerializeField]
    Text _nbUnitLabel;

    [SerializeField]
    int _nbUnitMin;

    [SerializeField]
    int _nbUnitMax;

    [SerializeField]
    int _nbUnitStep;

    [SerializeField]
    int _initialNbUnit;

    int nbUnit;

	// Use this for initialization
	void Start () 
    {
        enableScript(false);

        if (_initialNbUnit < _nbUnitMin)
            nbUnit = _nbUnitMin;
        else if (_initialNbUnit > _nbUnitMax)
            nbUnit = _nbUnitMax;
        else
            nbUnit = _initialNbUnit;

        SetNbUnitLabel();
	}

    void enableScript(bool value)
    {
        _cameraFollowMouseScript.enabled = value;
        _cameraZoomScript.enabled = value;
        _houndMoveScriptArmyA.enabled = value;
        _houndMoveScriptArmyB.enabled = value;
    }

    void PassingNbUnitParameter()
    {

    }

    void SetNbUnitLabel()
    {
        _nbUnitLabel.text = nbUnit.ToString();
    }

    void DisableUiElementsAfterRun()
    {
        _runButton.SetActive(false);
        _minusUnitButton.SetActive(false);
        _plusUnitButton.SetActive(false);
        _unitLabel.enabled = false;
        _nbUnitLabel.enabled = false;
    }

    public void MinusNbUnit()
    {
        if(nbUnit > _nbUnitMin)
            nbUnit -= _nbUnitStep;

        if (nbUnit < _nbUnitMin)
            nbUnit = _nbUnitMin;

        SetNbUnitLabel();
    }

    public void PlusNbUnit()
    {
        if (nbUnit < _nbUnitMax)
            nbUnit += _nbUnitStep;

        if (nbUnit > _nbUnitMax)
            nbUnit = _nbUnitMax;

        SetNbUnitLabel();
    }

    public void Run()
    {
        DisableUiElementsAfterRun();

        enableScript(true);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
