using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//OnClick의 인자로 Letter.text를 못넘겨주는걸로 보이므로 만듬
//애초에 더블클릭 때문에 필요하긴 했읅임
public class LetterScript : MonoBehaviour {

    //델리게이트를 통해 더블클릭만해도 실행에 가깝게, 커플링이 낮게 하고 싶었으나 해도 arg의 게임오브젝트를 찾음
//    public List<EventDelegate> actionMethod;
    public GameObject inputWord;//입력받을 문장위치
	// Use this for initialization
	void Start () {
        if (inputWord == null)
            inputWord = transform.parent.parent.parent.Find("TopItems").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public char GetChar()
    {
       return GetComponent<UILabel>().text[0];
    }
    //이벤트 딜리게이트의 연습?을 위해서 인자로 받지않고해봄
    void OnDoubleClick()
    {
        if (inputWord != null)
        {
            if(GetComponent<UILabel>().text.Length >= 2)
                inputWord.GetComponent<TopMenuScript>().FillBlankWords(' ');
            else
            inputWord.GetComponent<TopMenuScript>().FillBlankWords(GetComponent<UILabel>().text[0]);
        }
    }
}
