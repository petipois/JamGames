using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] dangerToTeeth;

    [SerializeField]
    int randomDanger, maxX,maxY;
    [SerializeField]
    float waitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDanger());

    }
    IEnumerator SpawnDanger()
    {

        yield return new WaitForSeconds(waitTime);
        CreateDanger();
        StartCoroutine(SpawnDanger());

    }
    void CreateDanger()
    {
        randomDanger = Random.Range(0, dangerToTeeth.Length);
        int randX = Random.Range(maxX, -maxX);
        GameObject danger = Instantiate(dangerToTeeth[randomDanger], new Vector2(randX,transform.position.y), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {

        
    }
}
