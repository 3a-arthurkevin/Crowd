using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour 
{
    [SerializeField]
    GameObject _unit;

    [SerializeField]
    int _maxLifePoint;

    [SerializeField]
    int _currentLifePoint;

	// Use this for initialization
	void Start () 
    {
        _currentLifePoint = _maxLifePoint;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void ApplyDamage(int damage)
    {
        if (_currentLifePoint > 0)
        {
            _currentLifePoint -= damage;

            if (_currentLifePoint <= 0)
            {
                _currentLifePoint = 0;
                _unit.SetActive(false);
            }
        }
    }
}
