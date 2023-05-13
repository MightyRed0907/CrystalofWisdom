using DG.Tweening;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public MapParameters mapParameters;
    public GameObject[] levels;
    public GameObject symbol;
    public static int level = 1, crystal_level = 1;
    public static bool restartF, victory, passed;

    private int[,] map;
    private GameObject levelBackground;

    private void Start()
    {
        passed = false;
        GameObject[] gs = GameObject.FindGameObjectsWithTag("Map");
        foreach (GameObject go in gs)
        {
            Destroy(go);
        }
        if(GameObject.Find("Pharaoh's words"))
            GameObject.Find("Pharaoh's words").GetComponent<Text>().text = "Who dares enter my pyramid?\r\nYou Idiot human\r\nSurvive from my mummies\r\nThen the crystal is yours";

        GenerateMap();
        PlaceTraps();
        PlaceMummies();
        PlaceExtractPoint();

        if(level <= 4)
        {
            levelBackground = Instantiate(levels[level - 1], GameObject.Find("position").transform);
            levelBackground.transform.name = "Level" + level.ToString();
            levelBackground.transform.Find("Crystal").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(crystal_level.ToString() + "-Crystal");
        }
    }

    private void Update()
    {
        if(restartF)
        {
            
            Start();
            restartF = false;
        }
        if(victory)
        {
            switch(crystal_level)
            {
                case 1:
                    levelBackground.transform.Find("Crystal").transform.DOLocalMove(new Vector3(2.819f, 8.735f, 0f), 1.5f).SetEase(Ease.Linear);
                    StartCoroutine(WaitForSec());
                    break;
                case 2:
                    levelBackground.transform.Find("Crystal").transform.DOLocalMove(new Vector3(3.23f, 8.72f, 0f), 1.5f).SetEase(Ease.Linear);
                    StartCoroutine(WaitForSec());
                    break;
                case 3:
                    levelBackground.transform.Find("Crystal").transform.DOLocalMove(new Vector3(2.92f, 8.2f, 0f), 1.5f).SetEase(Ease.Linear);
                    StartCoroutine(WaitForSec());
                    break;
                case 4:
                    levelBackground.transform.Find("Crystal").transform.DOLocalMove(new Vector3(3.27f, 8.15f, 0f), 1.5f).SetEase(Ease.Linear);
                    StartCoroutine(WaitForSec());
                    break;
                //Destroy(levelBackground.transform.Find("Crystal").gameObject, 1.5f);
                
            }

            GameObject.Find("Pharaoh's words").GetComponent<Text>().text = "You win human\r\nGet the crystal";
            victory = false;
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(levelBackground.transform.Find("Crystal").gameObject);
        GameObject crystalSector = Instantiate(symbol, GameObject.Find("Canvas").transform);
        crystalSector.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(crystal_level.ToString());

        if (crystal_level < 4)
        {
            GameObject.Find("Window2").transform.DOLocalMoveY(-80f, 0.5f).SetEase(Ease.Linear);
            passed = true;
        }
        else
        {
            GameObject.Find("Pharaoh's words").GetComponent<Text>().text = "You win human\r\n\r\nGet the wisdom\r\nAnd my greetings";
            GameObject.Find("Announce").transform.DOLocalMoveY(-12f, 1f).SetEase(Ease.Linear);
        }
    }

    private void GenerateMap()
    {
        map = new int[mapParameters.mapSize.x, mapParameters.mapSize.y];

        // Fill the map with empty spaces
        for (int x = 0; x < mapParameters.mapSize.x; x++)
        {
            for (int y = 0; y < mapParameters.mapSize.y; y++)
            {
                map[x, y] = 0;
            }
        }

        // Place the walls on the map - 1
        foreach (Vector2Int wallPosition in mapParameters.wallPositions)
        {
            map[wallPosition.x, wallPosition.y] = 1;
        }

        // Place the static obstacles on the map - 2
        foreach (Vector2Int obstaclePosition in mapParameters.obstaclePositions)
        {
            map[obstaclePosition.x, obstaclePosition.y] = 2;
        }

    }

    private void PlaceTraps()
    {
        // Shuffle the list of trap types
        Shuffle(mapParameters.trapTypes);

        // Place the traps randomly on the map
        foreach (TrapType trapType in mapParameters.trapTypes)
        {
            Vector2Int trapPosition = GetRandomEmptyPosition();
            map[trapPosition.x, trapPosition.y] = (int)trapType;
        }

        // Ensure that the traps are placed in a balanced way
        // ...
    }

    private void PlaceMummies()
    {
        // Shuffle the list of mummy types
        Shuffle(mapParameters.mummyTypes);

        // Place the mummies randomly on the map
        foreach (MummyType mummyType in mapParameters.mummyTypes)
        {
            Vector2Int mummyPosition = GetRandomEmptyPosition();
            map[mummyPosition.x, mummyPosition.y] = (int)mummyType;
        }

        // Ensure that the mummies are placed in a balanced way
        // ...
    }

    private void PlaceExtractPoint()
    {
        // Place the extract point randomly on the map
        Vector2Int extractPointPosition = GetRandomEmptyPosition();
        //map[extractPointPosition.x, extractPointPosition.y] = 3;

        // Ensure that the extract point is placed in a balanced way
        // ...
    }

    private Vector2Int GetRandomEmptyPosition()
    {
        Vector2Int position = new Vector2Int(Random.Range(0, mapParameters.mapSize.x), Random.Range(0, mapParameters.mapSize.y));

        while (position.x < mapParameters.mapSize.x && position.y < mapParameters.mapSize.y && map[position.x, position.y] != 0)
        {
            position = new Vector2Int(Random.Range(0, mapParameters.mapSize.x), Random.Range(0, mapParameters.mapSize.y));
        }

        return position;
    }

    private void Shuffle<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}

[System.Serializable]
public class MapParameters
{
    public Vector2Int mapSize = new Vector2Int(6, 6);
    public Vector2Int[] wallPositions;
    public Vector2Int[] obstaclePositions;
    public TrapType[] trapTypes;
    public MummyType[] mummyTypes;
}

public enum TrapType
{
    Spike,
    Arrow,
    RollingBoulder,
    PoisonGas
}

public enum MummyType
{
    Type1,
    Type2,
    Type3,
    Type4
}
