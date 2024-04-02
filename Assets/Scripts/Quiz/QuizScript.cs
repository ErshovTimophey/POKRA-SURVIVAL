using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quiz 
{
    public class QuizScript : MonoBehaviour
    {
        public int answersAmount = 2;

        public TextMeshProUGUI questionNumText;
        public TextMeshProUGUI questionText;
        public List<TextMeshProUGUI> answerTexts = new();
        public List<Button> answerButtons = new();
        public List<SpriteRenderer> hearts = new();
        public Animator animator;
        public LevelChanger levelChanger;
        public SceneData sceneData;
        
        private List<Question> questions = new()
        {
            new("Правда ли, что любой студент может заходить во все здания Вышки, включая любые общежития и корпусы ВШЭ во всех городах?", true),
            new("Правда ли, что каждый студент обязан оценивать работу своих преподавателей, а иначе его могут даже отчислить?", true),
            new("Правда ли, что чтобы не получить пересдачу по предмету, студент должен получить по нему хотя бы 3 балла из 10?", false),
            new("Правда ли, что в Вышке может вообще не быть экзамена по какому-то предмету?", true),
            new("Правда ли, что в формуле оценивания по предмету экзамен может весить максимум 50% от всей оценки?", false),
            new("Правда ли, что первый кирпичик этого университета заложил сам Пётр I?", false),
        };

        private Question curQuestion;
        private int questionNum = 1;
        private int heartsAmount = 3;

        private void SetButtonsStatus(bool status)
        {
            for (int i = 0; i < answersAmount; ++i)
            {
                answerButtons[i].interactable = status;
            }
        }

        private void SetButtonsColorAfterAnswer()
        {
            string correctAnswerText = curQuestion.isStatementCorrect ? "Верю!" : "Не верю!";
            for (int i = 0; i < answersAmount; ++i)
            {
                ColorBlock colorBlock = answerButtons[i].colors;
                if (answerTexts[i].text == correctAnswerText)
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

            SetButtonsColorAfterAnswer();

            string correctAnswerText = curQuestion.isStatementCorrect ? "Верю!" : "Не верю!";
            if (answerTexts[pressedAnswerNum].text != correctAnswerText)
            {
                SpriteRenderer heart = hearts[3 - heartsAmount];
                heart.color = Color.black;
                --heartsAmount;
            }
            if (heartsAmount == 0)
            {
                yield return new WaitForSeconds(1.6f);
                StartCoroutine(ShowNewQuestion());
                yield return new WaitForSeconds(0.55f);
                SceneManager.LoadScene(6);
            }

            yield return new WaitForSeconds(1.6f);

            questions.Remove(curQuestion);

            if (questions.Count == 0)
            {
                levelChanger.FadeOnLevel();
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

        private void OnApplicationQuit()
        {
            sceneData.isAtriumLoadedFirst = true;
        }
    }
}
