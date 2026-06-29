using System.Linq;
public class Grade
{
    private List<string> _lessonNames = new List<string>();
    private List<List<int>> _lessonScore = new List<List<int>>();
    private GradingPolicy _gradingPolicy = null!;
    private string _examTitle = null!;
    private List<int>  _examScore = new List<int>();
    private int _participationScore = 0;
    private CourseMaterialStatus _gradeStatus = CourseMaterialStatus._draft;

    public void Publish(List<string> value, int lessonCount,string examtitle,GradingPolicy gradingPolicy)
    {
        if (_gradeStatus != CourseMaterialStatus._draft)
        {
            throw new InvalidOperationException("Not drafted.");
        } 
        if(string.IsNullOrEmpty(examtitle))
        {
            throw new ArgumentNullException(nameof(examtitle),"Empty Input");
        }
        _examTitle = examtitle;
        if (lessonCount <= 0 || value.Count != lessonCount)
        {
            throw new ArgumentException(nameof(lessonCount),"Invalid value: lesson count should be equal to the number of values in the list, and greater than 0 ");
        }
        _lessonNames = value;
        for (int i = 0; i < lessonCount; i++)
        {
            _lessonScore[i] = new List<int>();
        }
        if (gradingPolicy.GetStatus() != CourseMaterialStatus._published)
        {
            throw new InvalidOperationException("Grading policy is not published");
        }
        _gradeStatus = CourseMaterialStatus._published;
    }
    public void SetClassParticipationScore(bool value)
    {
        if (value)
        {
            _participationScore = _gradingPolicy.GetClassParticipationScore();
        }
        else
        {
         _participationScore = 0;
        }
    }
    public void SetExamScore(bool value,int exerciseNumber)
    {
        if (value)
        {
            _examScore[exerciseNumber] = _gradingPolicy.GetScorePerExamExercise();
        }
        else
        {
          _examScore[exerciseNumber] = 0;
        }

    }
    public double PercentageLessonCompleted()
    {
        double num = 0;
        foreach (List<int> item in _lessonScore)
        {
            if (item.Count > 0)
            {
                num ++;
            }
        }
       return  num / _lessonScore.Count - 1 * 100; 
    }
    public double PresentLessonGrade()
    {
        int exerciseCount = 0;
        double val = 0;
        foreach (List<int> item in _lessonScore)
        {
            if (item.Count > 0)
            {
                exerciseCount += item.Count;
                foreach (int i in item)
                {
                    val += i;
                }
            }
        }
        int totalLessonsScore =  exerciseCount  * _gradingPolicy.GetScorePerExercise();
        double value = val/totalLessonsScore * 100;
        return value;
    }
    public double TotalGrade()
    {
        double earnedScore = 0;
        for (int i = 0; i < _lessonScore.Count; i++)
        { 
            for (int x = 0; x <_lessonScore[i].Count; x++)
            {
                 earnedScore += x;
            }
        }
       int totalExpectedLessonsScore = _lessonScore.Count  * _gradingPolicy.GetScorePerExercise() * 5; 
       double earnedScorePercent = earnedScore/totalExpectedLessonsScore * 100;
       double lessonWeight = earnedScorePercent * (_gradingPolicy.GetLessonWeight() /100);
       double examWeight = _examScore.Sum()/_gradingPolicy.GetScorePerExamExercise()*_examScore.Count * 100 * (_gradingPolicy.GetExamWeight()/100);
       double participationWeight = _participationScore * (_gradingPolicy.GetParticipationWeight()/100);
       return lessonWeight + examWeight + participationWeight;
    }
    public string LetterGrade(double value)
    {
        if (value >= 90)
        {
            return "A";
        }
        if (value >= 80 && value <= 89.9)
        {
            return "B";
        }
        if (value >= 70 && value <= 79.9)
        {
            return "C";
        }
        if (value >= 60 && value <= 69.9)
        {
            return "D";
        }
        return "F";
    }
    public void updateLessonScore(int lessonProgress,bool value,int exerciseNumber)
    {
        if (value)
        {
             _lessonScore[lessonProgress][exerciseNumber] = _gradingPolicy.GetScorePerExercise();  
        }
        else
        {
            _lessonScore[lessonProgress][exerciseNumber] = 0; 
        }
    }
    public void ViewGrade()
    {
        if (_lessonScore[0].Count == 0)
        {
            Console.WriteLine("Submission has not been made.");
        }
            for (int i = 0; i < _lessonNames.Count; i++)
            {
               if (_lessonScore[i].Count != 0)
                { 
                  Console.WriteLine($"{_lessonNames[i]}                     |      {_lessonScore[i].Sum()}");
                }
               else
                {
                 Console.WriteLine($"{_lessonNames[i]}                     |      [X] ");
                 }
            }
            if (_examScore.Count == 0)
             {
                 Console.WriteLine($"{_examTitle}                |  [X] ");
             }
             else
             {
                Console.WriteLine($"{_examTitle}                |  {_examScore.Sum()} ");
             }
            Console.WriteLine($"Participation Score | {_participationScore} ");
            Console.WriteLine($"Grade based on completed lessons:  {PresentLessonGrade()}%       |     {LetterGrade(PresentLessonGrade())}");
            Console.WriteLine($"Percentage Completed:  {PercentageLessonCompleted()}%");
            Console.WriteLine($"Final Grade:  {TotalGrade()}%       |     {LetterGrade(TotalGrade())}");
       
    }
    public CourseMaterialStatus GetGradeStatus()
    {
        return _gradeStatus;
    }
}