using System.Runtime.InteropServices.Marshalling;

public class GradingPolicy
{
    private int _scorePerExercise;
    private int _scorePerExamExercise;
    private int _classParticipationScore;
    private  int _examWeight;
    private int _lessonWeight;
    private int _classParticipationWeight;
    private CourseMaterialStatus _gradingPolicyStatus;

    public GradingPolicy(int scorePerExercise,int scorePerExamExercise, int paricipationScore, int examWeight,int lessonWeight,int classParticipationWeight)
    {
        _scorePerExercise = scorePerExercise;
        _scorePerExamExercise  = scorePerExamExercise;
        _classParticipationScore  =  paricipationScore;
        _examWeight = examWeight;
        _lessonWeight = lessonWeight;
        _classParticipationWeight = classParticipationWeight;
        _gradingPolicyStatus = CourseMaterialStatus._draft;

    }
    public void Publish()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._draft)
        {
            throw new InvalidOperationException("Failed! cannot publish an undrafted course");
        }
        if (_scorePerExercise <= 0 || _scorePerExamExercise <= 0 || _classParticipationScore <= 0)
        {
            throw new ArgumentOutOfRangeException("Scores should be above 0");
        }
        if (_examWeight <= 0 || _lessonWeight  <= 0 || _classParticipationWeight <= 0)
        {
            throw new ArgumentOutOfRangeException("Scores should be above 0");
        }
        if (_examWeight + _lessonWeight + _classParticipationWeight != 100)
        {
            throw new Exception("Total weight should equal 100");
        }
        _gradingPolicyStatus = CourseMaterialStatus._published;
    }
    public int GetScorePerExercise()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._published)
        {
            throw new InvalidOperationException("Not Published");
        }
        return _scorePerExercise;
    }
    public int GetClassParticipationScore()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._published)
        {
            throw new  InvalidOperationException("Not Published");
        }
        return _classParticipationScore;
    }
    public int GetScorePerExamExercise()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._published)
        {
            throw new  InvalidOperationException("Not Published");
        }
        return _scorePerExamExercise;
    }
    public int GetExamWeight()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._published)
        {
            throw new  InvalidOperationException("Not Published");
        }
        return _examWeight;
    }
    public int GetLessonWeight()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._published)
        {
            throw new  InvalidOperationException("Not Published");
        }
        return _lessonWeight;
    }
    public int GetParticipationWeight()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._published)
        {
            throw new  InvalidOperationException("Not Published");
        }
        return _classParticipationWeight;
    }
    public void Archive()
    {
        if (_gradingPolicyStatus == CourseMaterialStatus._archived)
        {
            throw new InvalidOperationException("Already archived");
        }
        _gradingPolicyStatus = CourseMaterialStatus._archived;
    }
    public void Recover()
    {
        if (_gradingPolicyStatus != CourseMaterialStatus._archived)
        {
            throw new  InvalidOperationException("Not deleted or archived cannot recover");
        }
        _gradingPolicyStatus = CourseMaterialStatus._draft;
    }
    public string DisplayGradingPolicy()
    {
        return $"Exam Weight: {_examWeight}%\n Lesson Weight: {_lessonWeight}%\n Class Participation: {_classParticipationWeight} ";
    }
    public CourseMaterialStatus GetStatus()
    {
        return _gradingPolicyStatus;
    }
}