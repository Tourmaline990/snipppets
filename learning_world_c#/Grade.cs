public class Grade
{
    private Course _course;
    private List<string> _lessonNames = new List<string>();
    private List<int> _lessonScore = new List<int>();
    private int _percentageCompleted;
    private double _presentGrade;
    private double _overallGrade;

    public Grade(Course course)
    {
        _course = course;
    }
    
    public void PopulateGradeInfo()
    {
        for (int i = 0; i < _course.LessonCount(); i++)
        {
            _lessonNames.Add(_course.GetLesson(i).GetTopic());
            _lessonScore.Add(0);
        }
    }
    public void PercentageCompleted(int lessonProgress)
    {
        _percentageCompleted =  lessonProgress  / _course.LessonCount() - 1 * 100; 
    }
    public void CompletedScorePercent(int lessonProgress)
    {
        double val = 0;
        for (int i = 0; i <= lessonProgress; i++)
        {
           val += _lessonScore[i];
        }
        int totalLessonsScore = lessonProgress  * _course.GetLesson(1).ExerciseNumber() * 2;
        double value = val/totalLessonsScore * 100;
        _presentGrade = value;
    }
    public void TotalGrade()
    {
        double val = 0;
        for (int i = 0; i < _lessonScore.Count; i++)
        {
           val += _lessonScore[i];
        }
       int totalLessonsScore = _course.LessonCount() - 1 * _course.GetLesson(1).ExerciseNumber() * 2;
       double value = val/totalLessonsScore * 100;
       _overallGrade = value;
    }
    public string GetTotalGrade()
    {
        return LetterGrade(_overallGrade);
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
    public void updateLessonScore(int lessonProgress,int score)
    {
         _lessonScore[lessonProgress] = score;
    }
    public void ViewGrade()
    {
            if (_lessonNames.Count == 0 && _lessonScore.Count == 0)
            {
                PopulateGradeInfo();
            }
            if (_lessonNames.Count != _lessonScore.Count)
            {
                throw new Exception("Corrupted data. LessonName is not equal to lessonScore");
            }
            for ( int i = 0; i < _lessonNames.Count; i++)
            {
               if (_lessonScore[i] != 0)
                { 
                  Console.WriteLine($"{_lessonNames[i]}                     |      {_lessonScore[i]}");
                }
               else
                {
                 Console.WriteLine($"{_lessonNames[i]}                     |      [X] ");
                 }
            }
            Console.WriteLine($"Grade based on completed lessons:  {_presentGrade}%       |     {LetterGrade(_presentGrade)}");
            Console.WriteLine($"Total Lesson Grade:  {_overallGrade}%       |     {LetterGrade(_overallGrade)}");
            Console.WriteLine($"Percentage Completed:  {_percentageCompleted}%");
       
    }
    ///
}