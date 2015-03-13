using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour 
{
    [SerializeField]
    Transform _unitTransform;

    [SerializeField]
    int _damage;

    [SerializeField]
    float _range;

    [SerializeField]
    int _layerEnemy;

    [SerializeField]
    float _timeBetweenDetection;

    [SerializeField]
    float _coolDownDuration;

    bool _canAttack;

    public bool CanAttack
    {
        get { return _canAttack; }
        set { _canAttack = value; }
    }

	// Use this for initialization
	void Start () 
    {
        _canAttack = true;
        StartCoroutine(DetectEnemies());
	}
	
    void OnEnable()
    {
        _canAttack = true;
    }

	// Update is called once per frame
	void Update () 
    {

	}

    public void AttackEnemies()
    {
        StartCoroutine(DetectEnemies());
    }

    IEnumerator DetectEnemies()
    {
        if (_canAttack)
        {
            Collider[] enemies = Physics.OverlapSphere(_unitTransform.position, _range, 1 << _layerEnemy);

            foreach (Collider collider in enemies)
            {
                HealthManagerScript healthManagerScript = collider.gameObject.GetComponent<HealthManagerScript>();
                if (healthManagerScript.isActiveAndEnabled)
                    healthManagerScript.ApplyDamage(_damage);
            }

            if (enemies.Length > 0)
            {
                _canAttack = false;
                yield return new WaitForSeconds(_coolDownDuration);
            }
            else
                yield return new WaitForSeconds(_timeBetweenDetection);

            if (_unitTransform.gameObject.activeSelf)
            {
                _canAttack = true;
                StartCoroutine(DetectEnemies());
            }
        }
    }


}
