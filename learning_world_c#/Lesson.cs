using System.ComponentModel;
using System.IO.Pipes;

public class Lesson : LearningMaterial
{
    private string _topic;
    private string _text;
    private string _example;
    private List<Exercise> _exercises = new List<Exercise>();
    

    public Lesson(string text,string example,string Topic)
    {
        _text = text;
        _example = example;
        _topic = Topic;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    protected override void ValidatePublish()
    {
        ValidateInput(_text);
        ValidateInput(_example);
        ValidateInput(_topic);
        if (_exercises.Count == 0)
        {
            throw new Exception("No exercise in lesson.");
        }
        if (_exercises.Count > 5 || _exercises.Count != 5)
        {
            throw new Exception($"5 exercises per lesson. {_exercises.Count} found.");
        }
        int num = 1;
        foreach (Exercise item in _exercises)
        {
            if (item.GetStatus() != CourseMaterialStatus._published)
            {
                throw new Exception($"Exercise {num} is not published.");
            }
        }
    }
    public void AddExercise(Exercise exercise)
    {
       AddMaterial(_exercises,exercise);
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
    public bool ExerciseAnswer(int Exercisenum,int optionnum)
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
       return exercise.Evaluate(optionnum);
    }
    public int ExerciseNumber()
    {
        return _exercises.Count;
    }
    public string GetTopic()
    {
        return _topic;
    }
   
    public void RemoveExercise(int Index)
    {
       RemoveMaterial(Index,_exercises);
    }
   //
}