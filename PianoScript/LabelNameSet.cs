using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelNameSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void HorizontalLabel()
    {
       SoundManager sounder = GameObject.Find("Sounder").GetComponent<SoundManager>();
        var list = sounder.octaveKeys;
       var bottom = transform.Find("DownLabels");
//        int downLength = transform.Find("DownLabels").childCount;
        for(int i = 0; i< bottom.childCount; i++)
        {
            bottom.GetChild(i).GetComponent<UILabel>().text = list[i].ToString();
        }
        int cur = 0;
        var top = transform.Find("TopLabels");
        for(int i =0; i < top.childCount; i++)
        {
            if (top.GetChild(i).gameObject.activeSelf == false) continue;

            top.GetChild(i).GetComponent<UILabel>().text = list[bottom.childCount +cur].ToString();
            cur++;
        }
       
        var another = transform.Find("AnotherKeys");
        for(int i = 0; i < another.childCount; i++)
        {
            string temp= "";
            if(another.GetChild(i).name == "OctaveUP")
            {
                temp = another.GetChild(i).GetComponent<UILabel>().text + " : " + sounder.nextKey;
            }
            else if (another.GetChild(i).name == "OctaveDown")
            {
                temp = another.GetChild(i).GetComponent<UILabel>().text + " : " + sounder.prevKey;
            }
            else if(another.GetChild(i).name == "Up")
            {
                temp = another.GetChild(i).GetComponent<UILabel>().text + " : " + sounder.plusKey;
            }
            else if(another.GetChild(i).name == "Down")
            {
                temp = another.GetChild(i).GetComponent<UILabel>().text + " : " + sounder.minusKey;
            }
            another.GetChild(i).GetComponent<UILabel>().text = temp;
        }
       
    }
    void OnEnable()
    {
        HorizontalLabel();
    }
    // Update is called once per frame
    void Update () {
	}
}
