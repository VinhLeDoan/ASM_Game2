using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class randomGold : MonoBehaviour
{
    public List<GameObject> listGold;
    public List<GameObject> listGoldOld;

    Vector3 endPos; //vi tri cuoi cung
    Vector3 nextPos; //vi tri tiep theo

    int goldLen;

    public float rangeToDestroyObject = 60f;
    public Transform player;




    // Start is called before the first frame update
    void Start()
    {

        endPos = new Vector3(18.0f, -2.0f, 0.0f);
        GenerateGold();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.position, endPos) < rangeToDestroyObject)
        {
            GenerateGold();
        }

        GameObject getOneGold = listGoldOld.FirstOrDefault();
        if (getOneGold != null && Vector2.Distance(player.position, getOneGold.transform.position) > rangeToDestroyObject)
        {
            listGoldOld.Remove(getOneGold);
            Destroy(getOneGold);
        }
    }


    private void GenerateGold()
    {
        for (int i = 0; i < 5; i++)
        {
            float khoangcachGold = Random.Range(2f, 5f); // khoang cach ngau nhien giua cac coin
            nextPos = new Vector3(endPos.x + khoangcachGold, -2f, 0f);

            //tao so nguyen ngau nhien trong khoang tu a-b, ko bao gom b
            int goldID = Random.Range(0, listGold.Count);

            //tao ra block ban do ngau nhien
            GameObject newGold = Instantiate(listGold[goldID], nextPos, Quaternion.identity, transform);
            listGoldOld.Add(newGold); //THêm coin vừa tạo vào mảng

            switch (goldID)
            {
                case 0: goldLen = 1; break;
                case 1: goldLen = 1; break;
            }

            endPos = new Vector3(nextPos.x + goldLen, -2f, 0f);
        }
    }
}
