public class Instructor
{
    private CourseSession _courseSession;
    private Forum _forum;
    private string _name;
    private string _InstructorId;

    public Instructor(Forum forum,string name,string id,CourseSession courseSession)
    {
        _forum  = forum;
        _name = "Instructor" + " " + name;
        _InstructorId = id;
        _courseSession = courseSession;
    }
    public void AddLesson(Lesson lesson)
    {
        _courseSession.GetCourse().AddLesson(lesson);
        Notification notification = new Notification($"Instructor {_name}", $"A new lesson has been added to  Course: {_courseSession.GetCourse().GetId()}.",DateTime.Now);
        _courseSession.Notify(notification);

    }
    public void AddExercise(int LessonIndex, Exercise exercise)
    {
        Lesson lesson =  _courseSession.GetCourse().GetLesson(LessonIndex - 1);
        lesson.AddExercise(exercise);
        Notification notification = new Notification($"Instructor {_name}",$"A new Exercise has been added to Lesson {LessonIndex} in Course: {_courseSession.GetCourse().GetId()}",DateTime.Now);
        _courseSession.Notify(notification);
    }
    public void RemoveExercise(int LessonIndex,int exerciseIndex)
    {
        _courseSession.GetCourse().GetLesson(LessonIndex - 1).RemoveExercise(exerciseIndex - 1);
        Notification notification = new Notification($"Intructor {_name}",$"Removed an exercise from Lesson {LessonIndex} in Course: {_courseSession.GetCourse().GetId()}",DateTime.Now);
        _courseSession.Notify(notification);

    }
    public void RemoveLesson(int Index)
    {
        _courseSession.GetCourse().RemoveLesson(Index - 1);
        Notification notification = new Notification($"Intructor {_name}",$"Removed  Lesson {Index} in Course: {_courseSession.GetCourse().GetId()}",DateTime.Now);
        _courseSession.Notify(notification);
    }
    
    public void Announcement(string text)
    {
        Question question = new Question(text,_name,DateTime.Now);
        _forum.AddQuestion(question);
    }
    public void RespondToQuestion(string text,int ThreadIndex)
    {
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
        if (_courseSession.IsLastWeek())
        {
            Notification notification = new Notification($"{_name}", "Notice: Last week of course activity, all coursework should be turned in early.",DateTime.Now);
            _courseSession.Notify(notification);
        }
    }

    
}