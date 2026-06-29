public class Instructor : Profile
{
    private CourseSession _courseSession = null!;
    private Forum _forum = null!;
   
    private InstructorActiveStatus _instructorActiveStatus;
    public Instructor(string name,string instructorId): base(name,instructorId)
    {
        _instructorActiveStatus = InstructorActiveStatus._available;
    }
    private string ValidateInput(string Param)
    {
        if (string.IsNullOrEmpty(Param))
        {
            throw new ArgumentNullException(nameof(Param),"Empty Input.");
        }
        return Param;
    }
    public CourseSession GetCourseSession()
    {
        if (_instructorActiveStatus != InstructorActiveStatus._active)
        {
            throw new InvalidOperationException("Instructor is not active.");
        }
        return _courseSession;
    }
    public void AllocateSession(CourseSession courseSession,Forum forum)
    {
       switch (_instructorActiveStatus)
       {
        case InstructorActiveStatus.revoked:
           throw new InvalidOperationException("Instructor has been revoked");
          case InstructorActiveStatus._active:
             throw new InvalidOperationException("Instructor is in an active courseSession.");
          case InstructorActiveStatus._suspended:
            throw new InvalidOperationException("Instructor is currently suspended.");
          case InstructorActiveStatus._leave:
            throw new InvalidOperationException("Instructor is unavailable. On leave.");
       }
       _courseSession = courseSession;
       _forum = forum;
       _instructorActiveStatus = InstructorActiveStatus._active;
    }
    public void Announcement(string text)
    {
        if (_instructorActiveStatus != InstructorActiveStatus._active)
        {
            throw new InvalidOperationException("Instructor is not active.");
        }
        text = ValidateInput(text);
        Question question = new Question(text,$"Instructor {GetProfileName()}: {GetprofileId()}",DateTime.Now);
        _forum.AddQuestion(question);
    }
    public void RespondToQuestion(string text,int ThreadIndex)
    {
        if (_instructorActiveStatus != InstructorActiveStatus._active)
        {
            throw new InvalidOperationException("Instructor is not active.");
        }
       text = ValidateInput(text);
       Thread thread =  _forum.GetThread(ThreadIndex);
       Response response = new Response(text,$"Instructor {GetProfileName()}: {GetprofileId()}",DateTime.Now);
       thread.AddResponse(response);
       string text1 = $"{response.GetCaller()} Responded to a question. For more details, view Forum";
       _forum.Notify(text1);
    }
    public void LastWeek()
    {
        if (_instructorActiveStatus != InstructorActiveStatus._active)
        {
            throw new InvalidOperationException("Instructor is not active.");
        }
        if (_courseSession.LastWeek())
        {
           List<Dictionary<string,MemberStatus>> stats =  _forum.GetMemberParticipationStatus();
           foreach (Dictionary<string,MemberStatus> item in stats)
           {
               foreach (KeyValuePair<string,MemberStatus> pair in item)
               {
                  if (pair.Value == MemberStatus._dormant)
                   {
                      _courseSession.GetEnrolledById(_forum.GetMember(pair.Key).GetId())!.SetParticipationScore(false);
                   }
                   else
                   {
                    _courseSession.GetEnrolledById(_forum.GetMember(pair.Key).GetId())!.SetParticipationScore(true);
                   }
               } 
           }
            Notification notification = new Notification($"{$"Instructor {GetProfileName()}: {GetprofileId()}"}", "Notice: Last week of course activity, all coursework should be turned in early.",DateTime.Now);
            _courseSession.Notify(notification);
        }
    }
    public string DisplayInstructor()
    {
        return $" {$"Instructor {GetProfileName()}: {GetprofileId()}"}";
    }
   public void Revoke()
    {
        if (_instructorActiveStatus == InstructorActiveStatus.revoked)
        {
            throw new InvalidOperationException("Already revoked");
        }
        _instructorActiveStatus = InstructorActiveStatus.revoked;
    }
    public InstructorActiveStatus GetInstructorActiveStatus()
    {
        return _instructorActiveStatus;
    }
}