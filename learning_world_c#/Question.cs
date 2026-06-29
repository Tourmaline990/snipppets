using System.ComponentModel.DataAnnotations;

public class Question : Message
{
   private QuestionStatus _questionStatus;
    public Question(string text,string caller,DateTime date):base(text,caller,date)
    {
       _questionStatus = QuestionStatus._unAnswered;
    }
    public string DisplayState()
    {
        if (_questionStatus == QuestionStatus._answered)
        {
            return "[√]";
        }
        return "[X]";
    }
    public override string Display()
    {
        return $"{GetCaller()} Asked: {GetText()} \n {GetDate()}   |  {DisplayState()}";
    }
//  
}