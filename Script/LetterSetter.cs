using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSetter : MonoBehaviour {

    public Transform field;//단어장 필드
    // Use this for initialization
    void AutoSetField()//prefab뽑고 거기에 넣고 할까 했으나 하드코딩스럽게 함
    {
        int asciiCode = 65;//65~90하고 남은 1개는 ' '으로
        int test = 65;
        int i = 0;
        while((char)asciiCode <= 'Z')
        {
//            Debug.Log((char)test);
            field.GetChild(i).GetComponent<UILabel>().text = ((char)asciiCode).ToString();
            i++;
            asciiCode++;
        }
        field.GetChild(i).GetComponent<UILabel>().text = "공백";
    }
    void Start () {
        field = transform.Find("Field");

        AutoSetField();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnEnable()
    {
    }
}
