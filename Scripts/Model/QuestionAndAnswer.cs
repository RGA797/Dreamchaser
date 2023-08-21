[System.Serializable]
public class QuestionAndAnswer
{
    public string Question;
    public string[] Answers;
    public int CorrectAnswerIndex;

    public QuestionAndAnswer()
    {
        Question = "";
        Answers = new string[] { "", "", "", "" };
        CorrectAnswerIndex = 0;
    }
}