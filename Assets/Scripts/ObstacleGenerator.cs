using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstacleLinePrefab, goalPrefab, obstacles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(string type, int pos){
        GameObject obstaclePrefab = obstacles;
        if(type == "Line") obstaclePrefab = obstacleLinePrefab;
        else if(type == "Goal") obstaclePrefab = goalPrefab;
        GenerateObstacle(obstaclePrefab, pos);
    }

    void GenerateObstacle(GameObject obstaclePrefab, int pos){
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector2(0, 0), Quaternion.identity);
        obstacle.name = obstaclePrefab.name;
        obstacle.transform.SetParent(obstacles.transform);
        ObstacleController ocs = obstacle.GetComponent<ObstacleController>();
        ocs.BeSetObstacle(pos);
    }
}
