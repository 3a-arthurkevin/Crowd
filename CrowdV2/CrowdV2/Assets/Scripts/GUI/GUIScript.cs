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
    GameObject _minusUnitRowButton;

    [SerializeField]
    GameObject _plusUnitRowButton;

    [SerializeField]
    Text _unitRowLabel;

    [SerializeField]
    Text _nbUnitRowLabel;



    [SerializeField]
    int _nbUnitMin;

    [SerializeField]
    int _nbUnitMax;

    [SerializeField]
    int _nbUnitStep;

    [SerializeField]
    int _initialNbUnit;



    [SerializeField]
    int _nbUnitMinRow;

    [SerializeField]
    int _nbUnitMaxRow;

    [SerializeField]
    int _nbUnitStepRow;

    [SerializeField]
    int _initialNbUnitRow;

    int _nbUnit;
    int _nbUnitRow;

	// Use this for initialization
	void Start () 
    {
        EnableScript(false);

        if (_initialNbUnit < _nbUnitMin)
            _nbUnit = _nbUnitMin;
        else if (_initialNbUnit > _nbUnitMax)
            _nbUnit = _nbUnitMax;
        else
            _nbUnit = _initialNbUnit;

        if (_initialNbUnitRow < _nbUnitMinRow)
            _nbUnitRow = _nbUnitMinRow;
        else if (_initialNbUnitRow > _nbUnitMaxRow)
            _nbUnitRow = _nbUnitMaxRow;
        else
            _nbUnitRow = _initialNbUnitRow;

        SetNbUnitLabel();
        SetNbUnitRowLabel();
	}

    void EnableScript(bool value)
    {
        _cameraFollowMouseScript.enabled = value;
        _cameraZoomScript.enabled = value;
        _houndMoveScriptArmyA.enabled = value;
        _houndMoveScriptArmyB.enabled = value;
    }

    void PassingUiParameters()
    {
        _houndMoveScriptArmyA.NbTotalUnits = _nbUnit;
        _houndMoveScriptArmyB.NbTotalUnits = _nbUnit;

        _houndMoveScriptArmyA.NbUnitInOneRow = _nbUnitRow;
        _houndMoveScriptArmyB.NbUnitInOneRow = _nbUnitRow;

    }

    void SetNbUnitLabel()
    {
        _nbUnitLabel.text = _nbUnit.ToString();
    }

    void SetNbUnitRowLabel()
    {
        _nbUnitRowLabel.text = _nbUnitRow.ToString();
    }

    void DisableUiElementsAfterRun()
    {
        _runButton.SetActive(false);
        _minusUnitButton.SetActive(false);
        _plusUnitButton.SetActive(false);
        _unitLabel.enabled = false;
        _nbUnitLabel.enabled = false;

        _minusUnitRowButton.SetActive(false);
        _plusUnitRowButton.SetActive(false);
        _unitRowLabel.enabled = false;
        _nbUnitRowLabel.enabled = false;
    }

    public void MinusNbUnit()
    {
        if(_nbUnit > _nbUnitMin)
            _nbUnit -= _nbUnitStep;

        if (_nbUnit < _nbUnitMin)
            _nbUnit = _nbUnitMin;

        SetNbUnitLabel();
    }

    public void PlusNbUnit()
    {
        if (_nbUnit < _nbUnitMax)
            _nbUnit += _nbUnitStep;

        if (_nbUnit > _nbUnitMax)
            _nbUnit = _nbUnitMax;

        SetNbUnitLabel();
    }

    public void MinusNbUnitRow()
    {
        if (_nbUnitRow > _nbUnitMinRow)
            _nbUnitRow -= _nbUnitStepRow;

        if (_nbUnitRow < _nbUnitMinRow)
            _nbUnitRow = _nbUnitMinRow;

        SetNbUnitRowLabel();
    }

    public void PlusNbUnitRow()
    {
        if (_nbUnitRow < _nbUnitMaxRow)
            _nbUnitRow += _nbUnitStepRow;

        if (_nbUnitRow > _nbUnitMaxRow)
            _nbUnitRow = _nbUnitMaxRow;

        SetNbUnitRowLabel();
    }

    public void Run()
    {
        DisableUiElementsAfterRun();

        PassingUiParameters();

        EnableScript(true);
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
