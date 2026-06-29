using System.Net.Mail;

public class Enrollment
{
     private CourseSession _courseSession;
    private string _learnerId;
    private Grade _grade;
    private List<Notification> _notifications = new List<Notification>();
    private int _lesson_progress = 0;
    private List<List<int>> _exerciseRecord = new List<List<int>>();
    private EnrollmentStatus _enrollmentStatus;
    private EvaluationStatus _evaluationStatus;
    private InterruptionStatus _interruptionStatus;

    public Enrollment(CourseSession courseSession,string learnerId)
    {
        _courseSession = courseSession;
        _learnerId = learnerId;
        _grade = new Grade();
        _enrollmentStatus = EnrollmentStatus._available;
        _evaluationStatus = EvaluationStatus._NA;
        _interruptionStatus = InterruptionStatus._NA;
    }
    public EnrollmentStatus GetEnrollmentStatus()
    {
        return _enrollmentStatus;
    }
    public EvaluationStatus GetEvaluationStatus()
    {
        return _evaluationStatus;
    }
    public void Withdraw(AccountManager accountManager,ForumManager manager)
    {
        switch (_enrollmentStatus)
        {
            case EnrollmentStatus._deleted:
               throw new InvalidOperationException("Account deactivated");
            case EnrollmentStatus._dropped:
              throw new InvalidOperationException("failed! Candidate has been 'dropped'.");
            case EnrollmentStatus._completed:
               throw new InvalidOperationException("Candidate has completed course session, failed!.");
            case  EnrollmentStatus._withdrawn:
                throw new InvalidOperationException("Already withdrawn");
            case EnrollmentStatus._available:
               _enrollmentStatus = EnrollmentStatus._withdrawn;
               Profile profile =  accountManager.GetAccountProfile(_learnerId)!;
                switch (profile)
                {
                  case Learner learner:
                  learner.AddNotification(new Notification("Enrollment Team", $"This is to inform you that your withdrawal from {_courseSession.GetSessionId()} has been completed. The outcome is recorded as 'withdrawned'.",DateTime.UtcNow));
                  SyncForumData(manager);
                  break; 
                  throw new InvalidOperationException("Failed!");  
                }
               break;
            case EnrollmentStatus._inProgress:
               _enrollmentStatus = EnrollmentStatus._withdrawn;
                Profile profile1 = accountManager.GetAccountProfile(_learnerId)!;
                switch (profile1)
                {
                   case Learner learner1: 
                      learner1.AddNotification(new Notification("Enrollment Team",$"This is to inform you that your withdrawal from {_courseSession.GetSessionId()} has been completed. The outcome is recorded as 'failed'.",DateTime.UtcNow));
                      SyncForumData(manager);
                      break;
                    throw new InvalidOperationException("Failed!"); 
                }
                break;
        }
    }
    
    public void Delete(ForumManager manager)
    {
        if (_enrollmentStatus == EnrollmentStatus._deleted)
        {
            throw new InvalidOperationException("Already deleted");
        }
        SyncForumData(manager);
        _enrollmentStatus = EnrollmentStatus._deleted;
    }
    public void Drop(AccountManager accountManager, ReasonToDropLearner reasonToDrop,ForumManager manager)
    {
        switch (_enrollmentStatus)
        {
            case EnrollmentStatus._deleted:
               throw new InvalidOperationException("Account deactivated");
            case EnrollmentStatus._completed:
               throw new InvalidOperationException("Candidate has completed course session, failed!.");
            case  EnrollmentStatus._withdrawn:
                throw new InvalidOperationException("Already withdrawned, failed!");
            case EnrollmentStatus._dropped:
              throw new InvalidOperationException("Already dropped.");
            case EnrollmentStatus._inProgress:
               _enrollmentStatus = EnrollmentStatus._dropped;
                Profile profile =  accountManager.GetAccountProfile(_learnerId)!;
                switch (profile)
                {
                  case Learner learner:
                     learner.AddNotification(new Notification("Enrollment Team",$"This is to inform you that your enrollment in {_courseSession.GetSessionId()} has been dropped. Reason {reasonToDrop}. The outcome is recorded as 'failed'.",DateTime.UtcNow));
                     _evaluationStatus = EvaluationStatus._failed;
                     SyncForumData(manager);
                     break; 
                  throw new InvalidOperationException("Failed!");  
                }
                break; 
        }
        if (_enrollmentStatus == EnrollmentStatus._available && _courseSession.GetActiveStatus() == ActiveStatus._inActive)
        {
            _enrollmentStatus = EnrollmentStatus._dropped;
            Profile profile =  accountManager.GetAccountProfile(_learnerId)!;
            switch (profile)
            {
                case Learner learner1:
                   learner1.AddNotification(new Notification("Enrollment Team",$"This is to inform you that your enrollment in {_courseSession.GetSessionId()} has been dropped.{reasonToDrop} Your final evaluation outcome is not affected.",DateTime.UtcNow));
                    _evaluationStatus = EvaluationStatus._NA;
                    SyncForumData(manager);
                    break;
            }
            return;
        }
        else
        {
            _enrollmentStatus = EnrollmentStatus._dropped;
            Profile profile =  accountManager.GetAccountProfile(_learnerId)!;
            switch (profile)
            {
                case Learner learner1:
                    SyncForumData(manager);
                    learner1.AddNotification(new Notification("Enrollment Team",$"This is to inform you that your enrollment in {_courseSession.GetSessionId()} has been dropped.{reasonToDrop} Your final evaluation outcome is recorded as 'failed'.",DateTime.UtcNow));
                    break;
            }
            return;
        }
    }
    public string GetLearnerId()
    {
        return _learnerId;
    }
    public string GetSessonId()
    {
        return _courseSession.GetSessionId();
    }
    public void ViewGrade()
    {
        if (_courseSession.GetActiveStatus() == ActiveStatus._inActive)
        {
            throw new InvalidOperationException("Session is yet to begin.");
        }
        if(_enrollmentStatus == EnrollmentStatus._deleted)
        {
            throw new InvalidOperationException("Account deactivated");
        }
        if (_grade.GetGradeStatus() != CourseMaterialStatus._published)
        {
             _grade.Publish(_courseSession.GetCourse().GetLessonNames(),_courseSession.GetCourse().LessonCount(),_courseSession.GetCourse().GetExam().GetTitle(),_courseSession.GetCourse().GetGradingPolicy());
        }
        _grade.ViewGrade();  
    }
    public void Submit(int Exercisenum,int optionnum)
    {
        if (_courseSession.GetActiveStatus() != ActiveStatus._ongoing)
        {
            throw new InvalidOperationException("Not available.");
        }
        if(_enrollmentStatus == EnrollmentStatus._deleted)
        {
            throw new InvalidOperationException("Account deactivated");
        }
        if (_enrollmentStatus != EnrollmentStatus._inProgress || _enrollmentStatus != EnrollmentStatus._available)
        {
            throw new InvalidOperationException("Not available");
        }
        if (_interruptionStatus != InterruptionStatus._NA)
        {
            throw new InvalidOperationException("Not available");
        }
        if (_courseSession.SubmissionDeadline(_lesson_progress))  
        {
            throw new ArgumentException(" Submission Failed. Assignment is locked.");
        } 
        if (_lesson_progress > _courseSession.GetCourse().LessonCount())
        {
            _enrollmentStatus = EnrollmentStatus._completed;
            if (_grade.TotalGrade() >= 70)
            {
                _evaluationStatus = EvaluationStatus._passed;
            }
            else
             {
                _evaluationStatus = EvaluationStatus._failed;
            }
            throw new Exception("All lessons and assesment are completed.");
        } 
        if (_exerciseRecord.Count == 0)
        {
            for (int i = 0; i <= _courseSession.GetCourse().LessonCount(); i++)
            {
                _exerciseRecord.Add(new List<int>()); 
            }
            _exerciseRecord[_lesson_progress].Add(Exercisenum);
        } 
        else
        {
            if (_exerciseRecord[_lesson_progress].Count == 0)
            {
                _exerciseRecord[_lesson_progress].Add(Exercisenum); 
            }
            else
            {
              int? exerciseIsSubmitted = _exerciseRecord[_lesson_progress].Find(num => num == Exercisenum);
               if (exerciseIsSubmitted != null)
               {
                throw new ArgumentException(nameof(Exercisenum),"Exercise has been submitted");
               }
               else
               {
                 _exerciseRecord[_lesson_progress].Add(Exercisenum); 
                } 
            }

        }
        if (_lesson_progress == _courseSession.GetCourse().LessonCount())
        {
           _grade.SetExamScore(_courseSession.GetCourse().GetExam().ExerciseAnswer(Exercisenum,optionnum),Exercisenum);
            if (_exerciseRecord[_lesson_progress].Count == _courseSession.GetCourse().GetExam().ExamExerciseCount())
            {
                 _lesson_progress ++; 
            }
           return;
        }
        _grade.updateLessonScore(_lesson_progress,_courseSession.GetCourse().GetLesson(_lesson_progress).ExerciseAnswer(Exercisenum,optionnum),Exercisenum);
        if (_exerciseRecord[_lesson_progress].Count == _courseSession.GetCourse().GetLesson(_lesson_progress).ExerciseNumber())
        {
            _lesson_progress ++;
        }
        if (_enrollmentStatus == EnrollmentStatus._available)
        {
            _enrollmentStatus = EnrollmentStatus._inProgress;
        }
    }
    public void ViewAllLessons()
    {
        if(_enrollmentStatus == EnrollmentStatus._deleted)
        {
            throw new InvalidOperationException("Account deactivated");
        }
            for (int i = 0; i < _courseSession.GetCourse().LessonCount(); i++)
             {
                if (i  <=  _lesson_progress)
                {
                   Console.WriteLine($"{_courseSession.GetCourse().GetLesson(i).GetTopic()}      |  [√]");
                    if (_exerciseRecord[i].Count != 0)
                     {
                        for (int x = 0; x < _exerciseRecord[i].Count; x++)
                        {
                            Console.WriteLine($"Exercise {_exerciseRecord[_lesson_progress][x]}     |  [√] ");
                        }
                     }
                }
                else
                {
                    Console.WriteLine($"{_courseSession.GetCourse().GetLesson(i).GetTopic()}      |  [X]");
                }
             } 
    }
    public Lesson TakeLesson()
    {
        if (_courseSession.GetActiveStatus() != ActiveStatus._ongoing)
        {
            switch(_courseSession.GetActiveStatus())
            {
                case ActiveStatus._inActive:
                  throw new InvalidOperationException("Session is yet to begin");
                case ActiveStatus._completed:
                  throw new InvalidOperationException("Session has been completed.");
                case ActiveStatus._paused:
                  throw new InvalidOperationException("Failed! currently unavailable.");
            }
        }
        if(_enrollmentStatus != EnrollmentStatus._available || _enrollmentStatus != EnrollmentStatus._inProgress)
        {
            switch (_enrollmentStatus)
            {
                case EnrollmentStatus._dropped:
                  throw new InvalidOperationException("Not available.");
                case EnrollmentStatus._withdrawn:
                   throw new InvalidOperationException("Learner is withdrawned from session.");
                case EnrollmentStatus._completed:
                   throw new InvalidOperationException("All lessons has been completed");
                case EnrollmentStatus._deleted:
                     throw new InvalidOperationException("Account deactivated");
            }
        }
        if ( _interruptionStatus != InterruptionStatus._NA)
        {
            switch (_interruptionStatus)
            {
                case InterruptionStatus._suspended:
                   throw new InvalidOperationException("You are currently suspended");
                case InterruptionStatus._onLeave:
                   throw new InvalidOperationException("Cannot take lesson while on leave.");
                case InterruptionStatus._break:
                   throw new InvalidOperationException("General break.");
            }
        }
        if (_lesson_progress == _courseSession.GetCourse().LessonCount())
        {
           throw new Exception("All lessons are completed, take exam.");
        }
        if (_courseSession.SubmissionDeadline(_lesson_progress))
        {
            _lesson_progress ++;
            return _courseSession.GetCourse().GetLesson(_lesson_progress);
        }
        return _courseSession.GetCourse().GetLesson(_lesson_progress);
       
    }
    public Exam TakeExam()
    {
         if (_courseSession.GetActiveStatus() != ActiveStatus._ongoing)
        {
            switch(_courseSession.GetActiveStatus())
            {
                case ActiveStatus._inActive:
                  throw new InvalidOperationException("Session is yet to begin");
                case ActiveStatus._completed:
                  throw new InvalidOperationException("Session has been completed.");
                case ActiveStatus._paused:
                  throw new InvalidOperationException("Failed! currently unavailable.");
            }
        }
        if(_enrollmentStatus != EnrollmentStatus._available || _enrollmentStatus != EnrollmentStatus._inProgress)
        {
            switch (_enrollmentStatus)
            {
                case EnrollmentStatus._dropped:
                  throw new InvalidOperationException("Not available.");
                case EnrollmentStatus._withdrawn:
                   throw new InvalidOperationException("Learner is withdrawned from session.");
                case EnrollmentStatus._completed:
                   throw new InvalidOperationException("All lessons has been completed");
                case EnrollmentStatus._deleted:
                     throw new InvalidOperationException("Account deactivated");
            }
        }
        if ( _interruptionStatus != InterruptionStatus._NA)
        {
            switch (_interruptionStatus)
            {
                case InterruptionStatus._suspended:
                   throw new InvalidOperationException("You are currently suspended");
                case InterruptionStatus._onLeave:
                   throw new InvalidOperationException("Cannot take lesson while on leave.");
                case InterruptionStatus._break:
                   throw new InvalidOperationException("General break.");
            }
        }
        return _courseSession.GetCourse().GetExam();
    }
    public Lesson RevisitOldLesson(int index)
    {
        if (_courseSession.GetActiveStatus() != ActiveStatus._ongoing)
        {
            throw new InvalidOperationException("Session is not active");
        }
        if (_enrollmentStatus != EnrollmentStatus._inProgress)
        {
            throw new InvalidOperationException("Not available");
        }
        if (_interruptionStatus != InterruptionStatus._NA)
        {
            throw new InvalidOperationException("Not available");
        }
        if (index >= _lesson_progress || index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index),"Lesson at that index  is unavailable.");
        }
        return _courseSession.GetCourse().GetLesson(index);
    }
    public void ViewNotifications()
    {
        foreach (Notification item in _notifications)
        {
            Console.WriteLine(item.Display());
        }
    }
    public void AddNotification(Notification notification) 
    {
        _notifications.Add(notification);
    }
    public void Suspend(ForumManager manager)
    {
        if (_interruptionStatus != InterruptionStatus._NA)
        {
            throw new InvalidOperationException("Failed! Suspension is unapplicable to candidate");
        }
        SyncForumData(manager,true);
        _interruptionStatus = InterruptionStatus._suspended;
    } 
    public void SetParticipationScore(bool value)
    {
        _grade.SetClassParticipationScore(value);
    }
    public void SyncForumData(ForumManager manager, bool freeze = false)
    {
        if (freeze == true)
        {
            manager.GetForum(_courseSession.GetSessionId()).GetMember(_learnerId).FreezeAccount();
        }
        else
        {
            manager.GetForum(_courseSession.GetSessionId()).GetMember(_learnerId).Remove();
        }
    }
}