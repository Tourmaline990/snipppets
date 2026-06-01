using System.ComponentModel;

public class Lesson
{
    private string _topic;
    private string _text;
    private string _example;
    private List<Exercise> _exercises = new List<Exercise>();
    private bool _isCompleted = false;

    public Lesson(string text,string example,string Topic)
    {
        _text = text;
        _example = example;
        _topic = Topic;
    }
    public void AddExercise(Exercise exercise)
    {
        if (_exercises.Count == 5)
        {
            throw new ArgumentOutOfRangeException(nameof(exercise),"5 exercises per lesson.");
        }
        _exercises.Add(exercise);
    }
    public void DisplayLesson()
    {
        int num = 1;
        Console.WriteLine(_topic);
        Console.WriteLine(_text);
        Console.WriteLine();
        Console.WriteLine($"Example: {_example}");
        Console.WriteLine();
        foreach (Exercise item in _exercises)
        {
            Console.WriteLine($">>> {num}"); 
            item.DisplayExercise();
          
        }
    }
    public void ExerciseAnswer(int Exercisenum,int optionnum)
    {
        if (Exercisenum >= _exercises.Count || Exercisenum < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(Exercisenum),"No such Exercise.");
        }
        Exercise exercise = _exercises[Exercisenum - 1];
        if (optionnum > exercise.ExerciseOptionCount() || optionnum < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(optionnum),"Invalid option");
        }
        exercise.Evaluate(optionnum);
    }
    public int CalculateLessonScore()
    {
        int score = 0;
        foreach (Exercise exercise in _exercises)
        {
           if (!exercise.GetIsCompleted())
            {
                throw new ArgumentOutOfRangeException("Exercise Incompleted.");
            } 
            score += exercise.GetScore();
        }
        return score;
    }
    public int ExerciseNumber()
    {
        return _exercises.Count;
    }
    public string GetTopic()
    {
        return _topic;
    }
    public bool GetIsCompleted()
    {
        foreach (Exercise exercise in _exercises)
        {
            if (!exercise.GetIsCompleted())
            {
                return _isCompleted;
            }
        }
        return _isCompleted = true;
    }
    public void RemoveExercise(int Index)
    {
        Index = Index - 1;
        if (Index >= ExerciseNumber())
        {
            throw new ArgumentException("Index out of range");
        }
        _exercises.RemoveAt(Index);
    }
    
}