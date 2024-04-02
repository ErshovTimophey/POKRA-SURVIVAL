using System;

[Serializable]
public class Question
{
    public string question;
    public bool isStatementCorrect;

    public Question(string question, bool isStatementCorrect)
    {
        this.question = question;
        this.isStatementCorrect = isStatementCorrect;
    }
}
