public class ForumMember
{
    private Enrolled _enrolled;
    private Forum _forum;
    public ForumMember(Enrolled enrolled,Forum forum )
    {
        _enrolled = enrolled;
        _forum = forum;
    }
    public Enrolled GetEnrolled()
    {
        return _enrolled;
    }
    public void AskQuestion(string question)
    {
        Question question1 = new Question(question,_enrolled.GetLearner().GetLearnerId(),DateTime.Now);
        _forum.AddQuestion(question1);
    }
    public void AnswerQuestion(int threadIndex,string text)
    {
        if (threadIndex < 1 || threadIndex >= _forum.ThreadCount())
        {
            throw new ArgumentException(nameof(threadIndex),"Index out of range");
        }
       Response response = new Response(text,_enrolled.GetLearner().GetLearnerId(),DateTime.Now);
       Thread thread  = _forum.GetThread(threadIndex);
       thread.AddResponse(response);
       string text1 = $"{response.GetCaller()} Responded to a question. For more details, view Forum";
       _forum.Notify(text1,DateTime.Now);
       
    }
    public void ViewForum()
    {
        _forum.DisplayForum();
    }
    public void ViewYourResponses()
    {
        _forum.GetResponsesByCaller(_enrolled.GetLearner().GetLearnerId());
    }
    public void ViewYourQuestions()
    {
        _forum.GetQuestionByCaller(_enrolled.GetLearner().GetLearnerId());
    }
}