using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Quiz 
{
    public class FinalTestScript : MonoBehaviour
    {
        public int answersAmount = 4;

        public TextMeshProUGUI questionNumText;
        public TextMeshProUGUI questionText;
        public List<TextMeshProUGUI> answerTexts = new();
        public List<Button> answerButtons = new();
        public Animator animator;
        public GameObject canvas;
        public GameObject resultCanvas;
        public TextMeshProUGUI result;
        public TextMeshProUGUI finalTitle;
        public SceneData sceneData;
        
        private List<FinalQuestion> questions = new()
        {
            new("Сколько сессий у студентов ВШЭ в учебном году?", new() {"4", "1", "2", "3"}),
            new("Какое самое раннее возможное время начала экзамена?", new() {"9:00", "8:00", "9:30", "11:00"}),
            new("Как называется платформа, которая используется для доступа к материалам занятий, обмена сообщения с преподователями и прохождения тестирований?", new() {"LMS", "FBA", "LSN", "SPS"}),
            new("Какой минимальный балл нужно набрать по предмету, чтобы не получить пересдачу?", new() {"4", "1", "2", "3"}),
            new("Сколько максимум может весить один элемент в формуле оценивания по предмету?", new() {"70%", "30%", "50%", "без ограничений"}),
            new("До какого времени можно купить комплексный обед в столовой?", new() {"16:30", "16:00", "18:00", "21:00"}),
            new("До скольки работает библиотека на Покре?", new() {"круглосуточно", "до 18:00", "до 21:00", "до 23:00"}),
            new("Как называется несвязанная с основной программой обучения дисциплина, которую все студенты выбирают для общего расширения кругозора?", new() {"Майнор", "НИС", "БЖД", "Философия"}),
            new("Как называется связанная с основной программой обучения дисциплина, которую студенты выбирают сами?", new() {"НИС", "Майнор", "БЖД", "Философия"}),
            new("Какая система оценивания принята во ВШЭ?", new() {"10-балльная", "5-балльная", "69-балльная", "100-балльная"}),
        };

        private FinalQuestion curQuestion;
        private int questionNum = 1;
        private int score = 0;

        private void SetButtonsStatus(bool status)
        {
            for (int i = 0; i < answersAmount; ++i)
            {
                answerButtons[i].interactable = status;
            }
        }

        private void SetButtonsColorAfterAnswer()
        {
            for (int i = 0; i < answersAmount; ++i)
            {
                ColorBlock colorBlock = answerButtons[i].colors;
                if (answerTexts[i].text == curQuestion.answers[0])
                {
                    colorBlock.normalColor = Color.green;
                    colorBlock.disabledColor = Color.green;
                    colorBlock.selectedColor = Color.green;
                    colorBlock.pressedColor = Color.green;
                    colorBlock.highlightedColor = Color.green;
                }
                else
                {
                    colorBlock.normalColor = Color.red;
                    colorBlock.disabledColor = Color.red;
                    colorBlock.selectedColor = Color.red;
                    colorBlock.pressedColor = Color.red;
                    colorBlock.highlightedColor = Color.red;
                }
                answerButtons[i].colors = colorBlock;
            }
        }

        private void ResetButtonColors()
        {
            for (int i = 0; i < answersAmount; ++i)
            {
                ColorBlock colorBlock = answerButtons[i].colors;
                colorBlock.normalColor = Color.white;
                colorBlock.disabledColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.highlightedColor = Color.white;
                colorBlock.pressedColor = new Color(200, 200, 200);
                answerButtons[i].colors = colorBlock;
            }
        }

        private void UpdateQuestionNumText()
        {
            questionNumText.text = $"ВОПРОС {questionNum}";
            ++questionNum;
        }

        private void GenerateQuestion()
        {
            UpdateQuestionNumText();
            int questionNum = Random.Range(0, questions.Count);
            curQuestion = questions[questionNum];
            questionText.text = curQuestion.question;
            List<string> curAnswers = new();
            curAnswers.AddRange(curQuestion.answers);
            for (int i = 0; i < answersAmount; ++i)
            {
                int answerNum = Random.Range(0, curAnswers.Count);
                answerTexts[i].text = curAnswers[answerNum];
                curAnswers.RemoveAt(answerNum);
            }
        }

        private IEnumerator ShowFirstQuestion()
        {
            SetButtonsStatus(false);
            GenerateQuestion();
            yield return new WaitForSeconds(0.6f);
            SetButtonsStatus(true);
        }

        private IEnumerator ShowNewQuestion()
        {
            animator.SetBool("isActive", true);
            yield return new WaitForSeconds(0.5f);
            GenerateQuestion();
            ResetButtonColors();
            yield return new WaitForSeconds(0.6f);
            animator.SetBool("isActive", false);
        }

        private IEnumerator OnClickAnswerCoroutine(int pressedAnswerNum)
        {
            SetButtonsStatus(false);

            if (answerTexts[pressedAnswerNum].text == curQuestion.answers[0])
            {
                ++score;
            }

            SetButtonsColorAfterAnswer();
            yield return new WaitForSeconds(1.6f);

            questions.Remove(curQuestion);

            if (questions.Count == 0)
            {
                canvas.SetActive(false);
                resultCanvas.SetActive(true);
                result.text = $"Твой результат - {score}/10";
                if (score <= 3)
                {
                    finalTitle.text = "Твоё звание - Двоечник!";
                }
                if (score >= 4 && score <= 7)
                {
                    finalTitle.text = "Твоё звание - Достойный студент!";
                }
                if (score >= 8)
                {
                    finalTitle.text = "Твоё звание - Отличник!";
                }
            }
            else
            {
                StartCoroutine(ShowNewQuestion());
                yield return new WaitForSeconds(0.6f);
            }

            SetButtonsStatus(true);
        }

        public void OnClickAnswer(int pressedAnswerNum)
        {
            StartCoroutine(OnClickAnswerCoroutine(pressedAnswerNum));
        }

        private void Start()
        {
            StartCoroutine(ShowFirstQuestion());
        }

        public void OnStartingAgain()
        {
            sceneData.isAtriumLoadedFirst = true;
            sceneData.numOfTaskInNFirst = 0;
            sceneData.isRFirstLoadedFirst = true;
            sceneData.isCuratorSceneLoadedFirst = true;
            sceneData.numOfTaskInRSecond = 0;
            sceneData.isLibraryLoadedFirst = true;
            sceneData.numOfCanteenTask = 0;
            sceneData.numOfLibraryTask = 0;
            sceneData.numOfFTask = 0;
            sceneData.isFGuyHappy = false;
            SceneManager.LoadScene(0);
        }

        private void OnApplicationQuit()
        {
            sceneData.isAtriumLoadedFirst = true;
        }
    }
}
