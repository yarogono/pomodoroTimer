<img src="http://kr.tipsandtricks.tech/images/1417/pomodoro-technique.jpeg" width="450px" alt="Pomodoro Timer"></img><br/>
# 뽀모도로 타이머(Pomodoro Timer)


25분동안 집중하고 5분동안 쉬는 공부 방식인 뽀모도로 타이머 어플리케이션입니다.<br/>
독학으로 공부한 C# 객체지향 언어를 다양하게 경험 및 활용하면서 만들었습니다.<br/>
WPF MVVM패턴을 활용해서 UI디자인과 로직 부분을 따로 관리 할 수 있도록 했습니다.
<br/>
<br/>
<br/>

## 메인 UI 구성

<img src="https://user-images.githubusercontent.com/70641418/121768173-c88b3a80-cb97-11eb-8df2-6d8c7af6a8a3.png" width="450px" alt="Pomodoro Timer"></img><br/>

- 기존의 윈도우 툴바를 숨기고 직접 디자인한 툴바를 삽입했습니다.
- 유저들이 쉽게 사용 할 수 있도록 UI는 최대한 심플하게 구성 했습니다.
- 마크업 언어인 XAML을 통해 MVVM패턴의 View 부분 작성
- View와 ViewModel 부분을 연결하기 
- 데이터 바인딩을 통해 XAML의 UI 요소와 ViewModel로 표현되는 데이터 사이 연결
<br/>
<br/>

## TO-DO LIST 앱 구현

<img src="https://user-images.githubusercontent.com/70641418/123513063-f3ac7880-d6c5-11eb-9554-f017549e2ceb.png" width="450px" alt="TO-DO LIST"></img><br/>
- 간단한 TO-DO-LIST앱 UI 구성했습니다.
- MariaDB를 활용해서 데이터베이스를 구축했습니다.
- 데이터베이스에 데이터 추가, 수정, 삭제관련 쿼리문을 직접 작성하고 다양한 오류를 접해봤습니다.
- DBeaver라는 SQL 클라이언트이자 데이터베이스 관리 도구를 사용했습니다.
<br/>
<br/>

## 로그인창 구현

<img src="https://user-images.githubusercontent.com/70641418/125197666-a4577200-e299-11eb-8c64-7c1973dda02b.JPG"></img><br/>
- xaml을 이용한 간단한 UI 디자인 구현했습니다.
- SQL문을 활용해 MariaDB 데이터베이스와 연동했습니다.
- PasswordBox를 통한 유저 비밀번호 보안
- PasswordBox에 입력된 값을 해시 함수를 이용해 MariaDB 데이터베이스에 Select하게 했습니다.

<br/>
<br/>

## Sign Up창 구현

<img src="https://user-images.githubusercontent.com/70641418/125197745-dff23c00-e299-11eb-8deb-f4e0b43ef807.JPG"></img><br/>
- Login창에서 Sign Up 버튼을 누르면 창이 뜨도록 만들었습니다.
- 패스워드 입력 시 PasswordBox를 이용해 입력되는 패스워드가 노출 되는 것을 방지했습니다.
- PasswordBox에서 받아온 데이터를 해시함수를 통해 암호화 해서 MariaDB에 inser를 하게 됩니다.

