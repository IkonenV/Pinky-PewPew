using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject[] dropList;
    public GameObject currentDrop;
    private int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(dropList.Length > 0)
        {
            currentDrop = dropList[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextOnList()
    {
        currentIndex = (currentIndex + 1) % dropList.Length;
        currentDrop = dropList[currentIndex];
    }

}
