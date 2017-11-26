using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//OnClick의 인자로 Letter.text를 못넘겨주는걸로 보이므로 만듬
//애초에 더블클릭 때문에 필요하긴 했읅임
public class LetterScript : MonoBehaviour {

    public GameObject inputWord;//입력받을 문장위치
    public GameMode mode;//현재 게임모드
    private LetterSetter gameHead;
//    public string dragAnswer;//드래그로 얻은 문장
	void Start () {
        if (inputWord == null)
            inputWord = transform.parent.parent.parent.Find("TopItems").gameObject;
        if (gameHead == null)
            gameHead = transform.parent.parent.GetComponent<LetterSetter>();
        //바로위는 그리드, 그위는 스크롤뷰 그리고 게임모드는 스크롤뷰에 있음
        mode = gameHead.mode;
    }
	
	// Update is called once per frame
    public char GetChar()
    {
       return GetComponent<UILabel>().text[0];
    }
    void OnDoubleClick()
    {//파인드 워드일때만 행함
        if (inputWord != null && mode == GameMode.FINDWORD)
        {
            if(GetComponent<UILabel>().text.Length >= 2)
                inputWord.GetComponent<TopMenuScript>().FillBlankWords(' ');
            else
            inputWord.GetComponent<TopMenuScript>().FillBlankWords(GetComponent<UILabel>().text[0]);
        }
    }
    //드래그시작 -> 드래그오버 동시에 되니 그냥 드래그시작은 제외
    void OnDragOver()
    {
        if (mode == GameMode.DRAGWORD && gameHead.pipe.Contains(transform.GetSiblingIndex() ) == false)
        {
//            gameHead.answerWord += GetComponent<UILabel>().text[0];
            gameHead.pipe.Add(transform.GetSiblingIndex() );//현재 자기가 몇번째 자식인지를 반환
        }
        Debug.Log("드랙오버");
    }
    void OnDragEnd()
    {
        Debug.Log("드래그 끝");
        //드래그 끝나면 제출
        string temp ="";//이걸 여기서 할당하고 제출함
        gameHead.pipe.Sort();

        for (int i =0; i < gameHead.pipe.Count; i++)
        {
            temp += transform.parent.GetChild(gameHead.pipe[i]).GetComponent<UILabel>().text;
        }
        inputWord.GetComponent<TopMenuScript>().FillBlankWords(temp);

        gameHead.pipe.Clear();
    }
}
