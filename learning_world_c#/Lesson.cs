using System.ComponentModel;

public class Lesson
{
    private string _topic;
    private string _text;
    private string _example;
    private List<Exercise> _exercises = new List<Exercise>();
    

    public Lesson(string text,string example,string Topic)
    {
        _text = ValidateInput(text);
        _example = ValidateInput(example);
        _topic = ValidateInput(Topic);
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
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
        Console.WriteLine();
        Console.WriteLine(_text);
        Console.WriteLine();
        Console.WriteLine($"Example: \n{_example}");
        Console.WriteLine();
        foreach (Exercise item in _exercises)
        {
            Console.WriteLine($">>> {num}"); 
            item.DisplayExercise();
            num ++;
        }
    }
    public void ExerciseAnswer(int Exercisenum,int optionnum)
    {
        if (Exercisenum >= _exercises.Count || Exercisenum < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Exercisenum),"No such Exercise, Index out of range.");
        }
        Exercise exercise = _exercises[Exercisenum];
        if (optionnum >= exercise.ExerciseOptionCount() || optionnum < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(optionnum),"Invalid option");
        }
        exercise.Evaluate(optionnum);
    }
    public int GetLessonScore()
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
                return false;
            }
        }
        return true;
    }
    public void RemoveExercise(int Index)
    {
        if (Index >= ExerciseNumber() || Index < 0)
        {
            throw new ArgumentException("Index out of range");
        }
        _exercises.RemoveAt(Index);
    }
    public bool ExerciseAreValid()
    {
        int num = 1;
        foreach (Exercise item in _exercises)
        {
            if (!item.CorrectAnswerInOptions())
            {
                throw new Exception($"Exercise {num} has no correct answer");
            }
            num++;
        }
        return true;
    }
    ///
}