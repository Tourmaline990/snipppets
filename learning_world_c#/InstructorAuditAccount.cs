public class InstructorAuditAccount : Account
{
    private string _briefintro;
    private InstructorStatus _instructorStatus;

    public InstructorAuditAccount(string name, string briefIntro,string email) : base(name,email)
    {
        _briefintro = briefIntro;
        _instructorStatus = InstructorStatus._draft;
    }
    private string ValidateInput(string Param)
    {
        if (string.IsNullOrEmpty(Param))
        {
            throw new ArgumentNullException(nameof(Param),"Empty Input.");
        }
        return Param;
    }
    public void Apply()
    {
        if (_instructorStatus != InstructorStatus._draft)
        {
            throw new InvalidOperationException("Cannot process application, not drafted.");
        }
        ValidateInput(_briefintro);
        _instructorStatus = InstructorStatus._applicant;
    }
    public void Prospect()
    {
        if (_instructorStatus != InstructorStatus._applicant)
        {
            throw new InvalidOperationException("Not an applicant");
        }
        _instructorStatus = InstructorStatus._prospect;
    }
    public void UnderReview()
    {
        if (_instructorStatus != InstructorStatus._prospect)
        {
            throw new InvalidOperationException("Not a prospect");
        }
        _instructorStatus = InstructorStatus._underReview;
    }
    public void Endorse()
    {
        if (_instructorStatus != InstructorStatus._underReview)
        {
            throw new InvalidOperationException("User is not under review");
        }
        _instructorStatus = InstructorStatus._endorsed;
    }
    public void OnBoard()
    {
        if (_instructorStatus != InstructorStatus._endorsed)
        {
            throw new InvalidOperationException("User has no active endorsement");
        }
        _instructorStatus = InstructorStatus._onBoarded;
    }
    public void Certify()
    {
         if (_instructorStatus != InstructorStatus._onBoarded)
        {
            throw new InvalidOperationException("User has not been onboarded");
        }
        _instructorStatus = InstructorStatus._certified;
    }
    public void Revoke()
    {
        if (_instructorStatus == InstructorStatus._revoked)
        {
            throw new Exception("Already revoked.");
        }
        _instructorStatus = InstructorStatus._revoked;
    }
    public InstructorStatus GetInstructorStatus()
    {
        return _instructorStatus;
    }
    public override string Display()
    {
        return $"{Display()} | {_briefintro} | {_instructorStatus}";
    }
}
    // public void AddLesson(Lesson lesson)
    // {
    //     if (lesson.ExerciseAreValid())
    //     {
    //        _courseSession.GetCourse(true).AddLesson(lesson);  
    //     }
    // }
    // public void AddExercise(int LessonIndex, Exercise exercise)
    // {
    //     Lesson lesson =  _courseSession.GetCourse(true).GetLesson(LessonIndex);
    //     lesson.AddExercise(exercise);
    // }
    // public void RemoveExercise(int LessonIndex,int exerciseIndex)
    // {
    //     _courseSession.GetCourse(true).GetLesson(LessonIndex).RemoveExercise(exerciseIndex);
    // }
    // public void RemoveLesson(int Index)
    // {
    //     _courseSession.GetCourse(true).RemoveLesson(Index);  
    // }
    
   
   
   
  //  
