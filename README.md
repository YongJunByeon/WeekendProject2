# WeekendProject2

1. 피아노만들기 asdf jkl; *3 으로 대략 24개의 건반으로 되있고 record기능과 듣기 기능등이 가능했으면함

2.이거 먼저만들듯
ngui + 단어공부용 프로그램을 만들게 될것 
단어 뜻 이렇게 있게될거같은데 문제는 looking for~ 같은 공백임 유력 후보는 looking_for임 아니면 =나 ,로 구분 지을수도
id(안쓸지도)| 단어 | 뜻 이렇게 될것 |로 구분짓는 이유는 안쓸거같아서
방식1.게임(?)시작하면 저기서 임의의 단어를 꺼내고 ui상에서는 뜻하고 ____언더바만 보여줌 그럼 나는 알파벳을 찾아서
드래그-드랍으로 꽂아 넣어야함 < 퍼즈도라처럼 단어가 떨어지고 임의의 단어가 나오도록? 아니면 알파벳a~z만 나오도록?
퍼즈도라가 좋겠으나 일단 만들고봄

방식2. 정답단어 abc를 두고 필드에 랜덤한 문장들 사이에 끼어 넣음, 그리고 드래그로abc를 찾아야함
 이건 top-down , left-right 2가지방식 으로 맞추면 됨
1,2둘다 공백의 경우는 체크안함 구현 때문이 아니라 랜덤한 단어사이에서 공백이 나오는건 좀 우습다고봄
그리고 가능하면 이미 푼 문제는 안나오면 좋겠음

1.파일읽기

2.딕셔너리? queue가 좋을듯 쨋든 자료구조로 문제를 받고 사이즈도 미리 입력받기

3.임의의 시간 정하기

4.임의의 시간동안 자기가 정한 queue를 비우기

5.잘못된 것을 더블클릭? 드래그 드랍? 했다면 일시적으로 행동불능(단 시간은 돌아가게)

이미지 구성은 대략과같음
진행도/총 사이즈	뜻	남은 시간 (00 : 00)	문제워드		//발음재생(아마 네이버나 그런곳에서 따서 실행인데 될지?) <<안될듯 따는게 복잡함

|	알파벳 필드 알파벳이 빠져나가는 식 무한정으로 갱신? 아니면 그냥 필드에서 무한정으로 공급?
|	전자면 항상 필드에 내가 찾는 알파벳이 존재해야하기에 필드를 매번 갈아야 할것
|	전자를 하고 우측 하단에 새로고침을 넣을수도 있겠음
|	참고로 이거 드래그로 그리드를 조정 가능하면 좋겠다
|	 그리드로 조정이 가능하게되면드래그를 해야하니 아이템이 드래그안되게 클릭식이될것
|	확실한건 문제 갯수까지 만들고 할듯


6.끝난경우 ( 진행도 == 총사이즈 or 남은시간이 0)이면 수고 하셨습니다와 함께 메뉴, 다시하기를 소환
7.메뉴면 메인으로 다시하기면 2에서 사이즈를 뺀 나머지를 다시하기
8.욕심이 생긴다면 6에서 못푼 문제의 경우(현재문제만 )정답 보여주는게 있을수도 있겠음 <구성은 상자 2개겹치고 안쪽위에 단어

에서 2
