using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {

        private OpenAIApi openai = new OpenAIApi("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");      // OpenAI API와 상호 작용하기 위한 OpenAIApi 객체

        private List<ChatMessage> messages = new List<ChatMessage>();  // 채팅 메시지 목록을 저장하기 위한 리스트

        //private static int speed = 3; // 실험용 변수

        private static int ExistHeight = 100; // 실험용 변수

        private static int ExistWidth = 100; //실험용 변수

        public static float TargetHeight; // 목표물 높이

        public static float TargetWidth; // 목표물 너비

        public static float TargetSpeed; // 목표물 스피드

        public string prompt;

        // public void Start(){

        //     LevelBalancing();

        // }
        public async void LevelBalancing()
        {
            SetPrompt();
            // string prompt2 ="정확한 수치를 말해달라니까?";

            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = prompt
            };

            // var newMessage2 = new ChatMessage()
            // {
            //     Role = "user",
            //     Content = prompt2
            // };

            if (messages.Count == 0) newMessage.Content = prompt; //내용 출력

            messages.Add(newMessage);

            // OpenAI에 요청을 보내 AI 응답을 받아옴
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                string message_str = message.Content;
                Debug.Log(message_str);
                // 메시지에서 크기와 속도를 추출하는 메소드 호출
                extract_size(message_str);
                extract_speed(message_str);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

        }

        private void extract_size(string message)
        {
            string input = message;

            string sizePattern = @"(\d+)x(\d+)픽셀";  // 크기 패턴 정의: 숫자 2개, 'x', 숫자 2개, '픽셀' 형식
            string sizePattern2 = @"(\d+)x(\d+) 픽셀";  // 크기 패턴에서 픽셀이 띄어쓰기 되어있을 경우 사용
            Regex sizeRegex = new Regex(sizePattern);  // 크기 패턴을 이용하여 정규식 객체 생성
            Regex sizeRegex2 = new Regex(sizePattern2);
            MatchCollection sizeMatches = sizeRegex.Matches(input);  // 주어진 입력 문자열에서 크기 패턴과 일치하는 부분을 찾음
            MatchCollection sizeMatches2 = sizeRegex2.Matches(input);
            if (sizeMatches.Count > 0)  // 크기 패턴과 일치하는 부분이 있을 경우
            {
                Match lastSizeMatch = sizeMatches[sizeMatches.Count - 1];  // 가장 마지막으로 매칭된 패턴을 추출
                float width = float.Parse(lastSizeMatch.Groups[1].Value);  // 첫 번째 그룹에서 width 값을 추출하여 정수로 변환
                float height = float.Parse(lastSizeMatch.Groups[2].Value);  // 두 번째 그룹에서 height 값을 추출하여 정수로 변환
                TargetHeight = height;
                TargetWidth = width;
                StartGame.currentwidth =  TargetWidth / ExistWidth;
                StartGame.currentheight = TargetHeight / ExistHeight;

                Debug.Log($"크기: {width}x{height}픽셀");  // 추출된 width와 height를 출력
                Debug.Log($"크기: {TargetWidth}x{TargetHeight}픽셀"); 
            }
            else if (sizeMatches2.Count > 0)
            {
                Match lastSizeMatch = sizeMatches2[sizeMatches2.Count - 1];  // 가장 마지막으로 매칭된 패턴을 추출
                float width = float.Parse(lastSizeMatch.Groups[1].Value);  // 첫 번째 그룹에서 width 값을 추출하여 정수로 변환
                float height = float.Parse(lastSizeMatch.Groups[2].Value);  // 두 번째 그룹에서 height 값을 추출하여 정수로 변환
                TargetHeight = height;
                TargetWidth = width;
                StartGame.currentwidth =  TargetWidth/ExistWidth;
                StartGame.currentheight =  TargetHeight/ExistHeight;
                
                Debug.Log($"크기: {width}x{height}픽셀");  // 추출된 width와 height를 출력
                Debug.Log($"크기: {TargetWidth}x{TargetHeight}픽셀"); 
            }
            else{
                LevelBalancing();
            }
        }

        private void extract_speed(string message)
        {
            string input = message;

            string speedPattern = @"(\d+)픽셀/초";  // 속도 패턴 정의: 숫자 1개 이상, '픽셀/초' 형식
            string speedPattern2 = @"(\d+) 픽셀/초";  // 속도 패턴 정의, 띄어쓰기가 되어있을 경우 사용
            Regex speedRegex = new Regex(speedPattern);  // 속도 패턴을 이용하여 정규식 객체 생성
            Regex speedRegex2 = new Regex(speedPattern2);
            MatchCollection speedMatches = speedRegex.Matches(input);  // 주어진 입력 문자열에서 속도 패턴과 일치하는 부분을 찾음
            MatchCollection speedMatches2 = speedRegex2.Matches(input);
            if (speedMatches.Count > 0)  // 속도 패턴과 일치하는 부분이 있을 경우
            {
                Match lastSpeedMatch = speedMatches[speedMatches.Count - 1];  // 가장 마지막으로 매칭된 패턴을 추출
                int speed = int.Parse(lastSpeedMatch.Groups[1].Value);  // 첫 번째 그룹에서 speed 값을 추출하여 정수로 변환
                TargetSpeed = speed;
                TargetCtrl.speed = TargetSpeed;
                Debug.Log($"속도: {speed}픽셀/초");  // 추출된 speed를 출력
                Debug.Log($"속도: {TargetSpeed}픽셀/초");
            }
            else if(speedMatches2.Count > 0)
            {
                Match lastSpeedMatch = speedMatches2[speedMatches2.Count - 1];  // 가장 마지막으로 매칭된 패턴을 추출
                int speed = int.Parse(lastSpeedMatch.Groups[1].Value);  // 첫 번째 그룹에서 speed 값을 추출하여 정수로 변환
                TargetSpeed = speed;
                TargetCtrl.speed = TargetSpeed;
                Debug.Log($"속도: {speed}픽셀/초");  // 추출된 speed를 출력
                Debug.Log($"속도: {TargetSpeed}픽셀/초");
            }
            else{
                LevelBalancing();
            }

            LoadGameScene();
        }

        private void SetPrompt(){
            if (Fire.HitCount<40){

                prompt = $"Aim hero와 비슷한 게임이며 FHD 해상도에서 1초에 한번씩 생성되며 {TargetCtrl.speed}픽셀/초로 움직이는 {ExistWidth}x{ExistHeight}픽셀의 목표물을 60초동안 {Fire.HitCount}번 맞추었어. 난이도를 쉽게 낮추려면 다음 스테이지에서의 목표물의 크기와 움직임의 속도를 어떻게 지정하면 될지 정확한 수치로 말해줄래?";  // 기본 프롬프트
            }
            else{
                prompt = $"Aim hero와 비슷한 게임이며 FHD 해상도에서 1초에 한번씩 생성되며 {TargetCtrl.speed}픽셀/초로 움직이는 {ExistWidth}x{ExistHeight}픽셀의 목표물을 60초동안 {Fire.HitCount}번 맞추었어. 다음 스테이지에서의 목표물의 크기와 움직임의 속도를 어떻게 지정하면 될지 정확한 수치로 말해줄래?";  // 기본 프롬프트
            }
        }

        private void LoadGameScene(){
            int Round = PlayerPrefs.GetInt("Round");
            switch(Round){
                case 1:
                    SceneManager.LoadScene("Game 1");
                    break;
                case 2:
                    SceneManager.LoadScene("Game 2");
                    break;
                case 3:
                    SceneManager.LoadScene("Game 3");
                    break;
                case 4:
                    SceneManager.LoadScene("Game 4");
                    break;
                default:
                    SceneManager.LoadScene("Game");
                    break;
            }
        }
    }
}
