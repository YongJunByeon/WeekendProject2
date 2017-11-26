using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode { FINDWORD, DRAGWORD};
public class LetterSetter : MonoBehaviour {

    public Transform field;//단어장 필드
    public GameMode mode;
    public List<int> pipe;
    // Use this for initialization
    void AutoSetField()//prefab뽑고 거기에 넣고 할까 했으나 하드코딩스럽게 함
    {
        
            int asciiCode = 65;//65~90하고 남은 1개는 ' '으로
            int i = 0;
            while ((char)asciiCode <= 'Z')
            {
                //            Debug.Log((char)test);
                field.GetChild(i).GetComponent<UILabel>().text = ((char)asciiCode).ToString();
                i++;
                asciiCode++;
            }
            field.GetChild(i).GetComponent<UILabel>().text = "공백";
    }
    //드래그 워드 모드 일단 정답을 킵해두고 임의의 번째수 까지를 공백으로 두기
    public void AutoSetField(string answer)
    {
        int answerPos = 0;
        //최대 가로수
        field = transform.Find("Field");
        int col = field.GetComponent<UIGrid>().maxPerLine;
        Debug.Log(col);
        while(true)
        {
            answerPos = Random.Range(0, field.childCount - answer.Length);//정답범위는 아이의수 - 정답길이만큼으로
            //여기에다가는 answer길이가 n 이상이면 걍 나가도록하는거
            if (answer.Length > col) break;
            if (answerPos % col > (answerPos + answer.Length) % col)//만약 정답 위치 + answerLength를 햇을때 >12 이상(다음줄)이면 안되 이를 해결하기위한 해결책으로 answer % n < stringlength% n만 나가도록
                continue;
            else
                break;
        }
        Debug.Log("정답 위치 " + answerPos);
        //        int ascii = 65;
        int stringCur = 0;
        for(int i = 0; i < field.childCount; i++)
        {
            //만약 i의 길이가 정답위치 ~ 정답위치+정답까지인경우
            if (i >= answerPos && i < answerPos + answer.Length)
            {
                field.GetChild(i).GetComponent<UILabel>().text = answer[stringCur].ToString().ToUpper();
                stringCur++;
            }
            else
            {
                field.GetChild(i).GetComponent<UILabel>().text = ((char)Random.Range(65, 90)).ToString();
            }
        }
    }
    void Start () {


        if (mode == GameMode.FINDWORD)
            AutoSetField();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnEnable()
    {
    }
}
