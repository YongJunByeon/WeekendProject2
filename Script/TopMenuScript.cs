using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMenuScript : MonoBehaviour {

    float timeLimit;//Time형으로?
    int remainingCount;//남은문제
    int curCount;//맞춘 문제
    string quizAnswer;//문제정답, 해석은 필요 없다고 판단
    Transform before;
    LetterSetter field;//hasa관계를 꾀함 드래그,드롭식의 퀴즈존을 쓰기 위함

   public UILabel timeLabel;
   public UILabel countLabel;
   public UILabel meaning;//현재 문제의 뜻
   public UILabel blankWords;//문제 _ _ _ <<이거, 맞추면 _ a _ 이런식으로 바뀜

	// Use this for initialization
	void Start () {
        if(field == null)
        field = transform.parent.Find("WordScrollView").GetComponent<LetterSetter>();
        timeLabel = transform.Find("Time").GetComponent<UILabel>();
        countLabel = transform.Find("Progress").GetComponent<UILabel>();
        meaning = transform.Find("Mean").GetComponent<UILabel>();
        blankWords = transform.Find("Blanks").GetComponent<UILabel>();
        if (before == null)
            before = transform.Find("Before");
    }
	
	// Update is called once per frame
	void Update () {
        if (remainingCount < curCount)
            MenuScript.inst.WinOrLose(true);
        if (timeLimit <= 0)
            MenuScript.inst.WinOrLose(false);
        else NextQuiz();
        DrawTime();
	}
    // 1/1이되면 즉 remaing?그거랑 같아지면 이긴거임 추가로 진척도 값 갱신시키기

    //블랭크를 만들면서 a가 있을경우(단어를 클릭했을경우) 그걸 보여주기
    //근데 블랭크따로, 채우기 따로 할지도 모르겠음 그게더 이쁠거같기도
    void SetBlankWords()
    {
        blankWords.text = "";//일단 초기화
        for (int i = 0; i< quizAnswer.Length *2 -1; i++)
        {
            if (i % 2 == 0) blankWords.text += "_";
            else blankWords.text += " ";//너무 붙으면 안이쁨
        }
    }
    //여기에 빈칸이 없는지 체크하기
    private bool FillCheck()
    {
        for(int i = 0; i < blankWords.text.Length; i++)
        {
            if(blankWords.text[i] == '_')
            {
                return false;
            }
        }
        //여길 왔다는건 _를 다했단뜻
        curCount++;
        countLabel.text = curCount.ToString() + " / " + remainingCount.ToString();
        return true;
    }
    //퀴즈를 새로받음
    private void NextQuiz()
    {
        if (FillCheck() == false || remainingCount < curCount) return;

        var temp = WordScript.inst.GetQuiz();
        //
        before.Find("Word").GetComponent<UILabel>().text = quizAnswer;
        before.Find("Mean").GetComponent<UILabel>().text = meaning.text;

        meaning.text = temp.Value;
        quizAnswer = temp.Key;

        SetBlankWords();
    }
    //스트링을 받는게 아니라 리스트를 받는다 그리고 거기의 맨앞자리부터 꺼내서비교함
    //드래그하면서 리스트는 위치가 바뀌는 불상사가 생길거긴함
    public void FillBlankWords(string s)
    {
        Debug.Log("조건 1: " + s.IndexOf(quizAnswer.ToUpper() ) );
        Debug.Log("조건2 : " + quizAnswer.Length);
        Debug.Log("조건2의 다른수 "  +field.transform.Find("Field").GetComponent<UIGrid>().maxPerLine );
        if (s.Length <= 0) return;
        //불일치 혹은 길이가 최대 행길이를 넘겼다면 index상으로 0번째 위치가 아닌가?
        if( s != quizAnswer.ToUpper() ||
            (quizAnswer.Length <= field.transform.Find("Field").GetComponent<UIGrid>().maxPerLine &&
            s.IndexOf(quizAnswer.ToUpper() ) != 0 ) )
        {
            timeLimit -= 5f;
            return;
        }
        else
        {
            Debug.Log("정ㄷ");
            curCount++;
            if (remainingCount < curCount) return;

            countLabel.text = curCount.ToString() + " / " + remainingCount.ToString();            
            var temp = WordScript.inst.GetQuiz();
            //
            before.Find("Word").GetComponent<UILabel>().text = quizAnswer;
            before.Find("Mean").GetComponent<UILabel>().text = meaning.text;

            meaning.text = temp.Value;
            quizAnswer = temp.Key;

            field.AutoSetField(temp.Key);
            SetBlankWords();

        }


    }
    public void FillBlankWords(char a)
    {
        //a p p l e
        //_ _ _ _ _ l이 들어가면 4번째 자리니깐 *2 -1해야함
        int index = quizAnswer.ToUpper().IndexOf(a);
        //다시찾기 //2개째도 이미 있다면? 재귀로? 반복문으로?
        //재귀는 호출을 반복하니 힘들어질것 반복문으로
        while (index != -1 )
        {
            if (blankWords.text[index * 2 ] != '_')
            {//index위치로하면 index위치를 찾기에 바로 답이 나와서 예상(전진)을 안함
                index = quizAnswer.ToUpper().IndexOf(a, index +1);
            }
            else break;//값이 '_'가 맞다면 탈출시킴
        }
        //값없음 패널티로 5초깎고 종료
        if (index == -1)
        {
            timeLimit -= 5f;
            return;
        }

        //있다면 빈칸의 인덱스 *2 -1 번째값을 a로 보이게 함
 //값변경 그냥 replace만 하니깐 _가 전부 a가 됨 다라서 이렇게 text를 재할당 하는데 remove로 위치를 지우고 Insert로 거기에 값을넣는식으로함
        blankWords.text = blankWords.text.Insert(index * 2, a.ToString()).Remove(index * 2 + 1, 1);

        //        blankWords.text = blankWords.text.Replace( blankWords.text[index*2 ],a);
    }

    void DrawTime()
    {
        int hour = (int)timeLimit / 3600;
        int min = (int)(timeLimit % 3600 )/ 60;
        int sec = (int)(timeLimit % 3600) % 60;

        timeLimit -= Time.deltaTime;
        timeLabel.text = hour.ToString("00") +":" +min.ToString("00") + ":" +sec.ToString("00");
    }
    void OnEnable()
    {
        Start();//enable이 먼저 시작됨 따라서 임의로 이렇게 호출시킴
        curCount = 1;//새로시작이니 1로 초기화
        
        var temp = WordScript.inst.GetQuiz();
        timeLimit = WordScript.inst.Time;
        Debug.Log(timeLimit +" --'");
        remainingCount = WordScript.inst.Count;
//        timeLabel.text = timeLimit.ToString();//임시
        countLabel.text = curCount.ToString() + " / " + remainingCount.ToString();
        meaning.text = temp.Value;
        quizAnswer = temp.Key;

        if (field.mode == GameMode.DRAGWORD)
            field.AutoSetField(temp.Key);
        SetBlankWords();
        Debug.Log("정답 : " + quizAnswer);

    }
}
