public class Instructor
{
    private CourseSession _courseSession;
    private Forum _forum;
    private string _name;
    private string _InstructorId;

    public Instructor(Forum forum,string name,string id,CourseSession courseSession)
    {
        _forum  = forum;
        _name = "Instructor" + " " + ValidateInput(name);
        _InstructorId = ValidateInput(id);
        _courseSession = courseSession;
    }
    private string ValidateInput(string Param)
    {
        if (string.IsNullOrEmpty(Param))
        {
            throw new ArgumentNullException(nameof(Param),"Empty Input.");
        }
        return Param;
    }
    public void AddLesson(Lesson lesson)
    {
        if (lesson.ExerciseAreValid())
        {
           _courseSession.GetCourse(true).AddLesson(lesson);  
        }
    }
    public void AddExercise(int LessonIndex, Exercise exercise)
    {
        Lesson lesson =  _courseSession.GetCourse(true).GetLesson(LessonIndex);
        lesson.AddExercise(exercise);
    }
    public void RemoveExercise(int LessonIndex,int exerciseIndex)
    {
        _courseSession.GetCourse(true).GetLesson(LessonIndex).RemoveExercise(exerciseIndex);
    }
    public void RemoveLesson(int Index)
    {
        _courseSession.GetCourse(true).RemoveLesson(Index);  
    }
    public void Announcement(string text)
    {
        text = ValidateInput(text);
        Question question = new Question(text,_name,DateTime.Now);
        _forum.AddQuestion(question);
    }
    public void RespondToQuestion(string text,int ThreadIndex)
    {
       text = ValidateInput(text);
       Thread thread =  _forum.GetThread(ThreadIndex);
       Response response = new Response(text,_name,DateTime.Now);
       thread.AddResponse(response);
       string text1 = $"{response.GetCaller()} Responded to a question. For more details, view Forum";
       _forum.Notify(text1,DateTime.Now);
    }
    public string DisplayInstructor()
    {
        return $"Name: {_name}, Id: {_InstructorId}";
    }
   
    public string GetName()
    {
        return _name;
    }
    public void LastWeek()
    {
        if (_courseSession.LastWeek())
        {
            Notification notification = new Notification($"{_name}", "Notice: Last week of course activity, all coursework should be turned in early.",DateTime.Now);
            _courseSession.Notify(notification);
        }
    }

    ///
}