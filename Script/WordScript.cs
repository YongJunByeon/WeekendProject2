using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//word 파일에서 글자를 뽑고 문제를 내주는 프로그램, 
public class WordScript : MonoBehaviour {
//    [SerializeField]
//    TextAsset filePath;

    static public WordScript inst;//게임 내에서 이걸 따올 방법이 한정적이고 Find하기 귀찮기에 이걸로함


    public UILabel labelInputer;//메뉴에 넣을까 싶었는데 그냥 여기에 넣음 관리가 편할것

    int time;//남은시간
    public int count = 0;//quizs의 남은문제
    public int Time
    { get { return time; } }
    public int Count
    {
        get
        {
            return count;
        }
    }
    string answerText;//정답텍스트, 다른곳에 있을이유는 없다고 생각
    public string AnswerText
    {
        get
        {
            return answerText;
        }
    }

    List<KeyValuePair<string, string>> words = new List<KeyValuePair<string, string>>();//딕셔너리를 하자니 찾는 일은 안할꺼같음
    Queue<KeyValuePair<string, string>> quizs = new Queue<KeyValuePair<string, string>>();//맨앞에 것만 쏙쏙 뺄거니깐 큐로 충분
    //키밸류 페어는 별수 없다고 생각함
    void Awake()
    {
        if (inst == null) inst = this;
    }
	// Use this for initialization
	void Start () {
        if(labelInputer == null)
        labelInputer = GameObject.Find("UI Root").transform.Find("Main").Find("WordCount").GetComponent<UILabel>();
        TextAsset wordAsset = Resources.Load("FindLetter\\wordbook") as TextAsset;

        ReadToText(wordAsset);
    }
    void ReadToText(TextAsset WA)
    {
        StringReader reader = new StringReader(WA.text);
        string line = reader.ReadLine();
        string[] temp;

        do
        {
            temp = line.Split('=');

            words.Add (new KeyValuePair<string, string>(temp[0], temp[1]) );
            line = reader.ReadLine();
        } while (line != null);
    }
    public int WriteText(string text =null, string mean =null)
    {
        Debug.Log(text.Length);
        if (text == null || mean == null || text.Length <= 0 || mean.Length <= 0) return 2;
 //       Debug.Log(filePath.name);
        FileStream FS = new FileStream("Assets\\Resources\\FindLetter\\wordbook.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(FS);
        //int인 이유는 오류코드를 만들어 보고 싶으
        for (int i = 0; i < text.Length; i++)
            //!로해서 반대의 경우를 꾀함
            if (!((text[i] >= 65 && text[i] <= 90) || (text[i] >= 97 && text[i] <= 122)))
            {
                writer.Close();
                FS.Close();
                return 1;
            }
        //mean은 건드리지 않음

        writer.WriteLine(text +"=" +mean);
        //워드내에 추가
        words.Add(new KeyValuePair<string, string>(text, mean));
        writer.Close();
        FS.Close();

        return 0;
    }
    // Update is called once per frame
    void Update () {
        labelInputer.text = "저장된 단어수" + words.Count;
    }
    public void QuizSet(int num,int timeInSec)//시간을 초로 가능하면 분으로하고싶음
    {
        if (num <= 0) num = 1;//0이면 1개만 하게
        else if (num > words.Count) num = words.Count;
        count = num;

        if (timeInSec <= 0) timeInSec = 60;
        else if (timeInSec > (120 * 60)) timeInSec = 120 * 60;
        time = timeInSec;
        Debug.Log(time);

        if (quizs.Count != 0)
            quizs.Clear();

        var copyCat = words.GetRange(0,words.Count);//단순복사
        
        for(int i = 0; i< num; i++)
        {
            int cur = Random.Range(0, copyCat.Count);
            quizs.Enqueue(copyCat[cur]);//퀴즈값 하나넣고
            copyCat.Remove(copyCat[cur]);//그값을 빼서 중복을 방지한다
        }
    }
    public KeyValuePair<string,string> GetQuiz()//퀴즈 1개뱉음 
    {
        return quizs.Dequeue();
    }
    //이겼거나 졌거나 이걸 실행
}
