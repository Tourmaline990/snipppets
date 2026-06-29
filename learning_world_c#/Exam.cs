using System.Security;

public class Exam : LearningMaterial
{
    private string _title;
    private string _instruction;
    private List<Exercise> _exercises = new List<Exercise>();
    private int _timeLimit;
    public Exam(string instruction,int timeLimit,string title)
    {
        _instruction = instruction;
        _timeLimit = timeLimit;
        _title = title;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {      
            throw new ArgumentNullException(nameof(param),"Empty Input.");
        }
        return param;
    }
    public void AddExercise(Exercise exercise)
    {
        AddMaterial(_exercises,exercise);
    }
    public void Display()
    {
        Console.WriteLine(_title);
        Console.WriteLine(_instruction);
        Console.WriteLine(_timeLimit);
        foreach (Exercise item in _exercises)
        {
            item.DisplayExercise();
        }
    }
    public int ExamExerciseCount()
    {
        return _exercises.Count;
    }
    public string GetTitle()
    {
        return _title;
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
    protected override void ValidatePublish()
    {
        ValidateInput(_title);
        ValidateInput(_instruction);
        if (_timeLimit <= 0 || _timeLimit > 60)
        {
            throw new Exception("Allowed Limit: 60 minute");
        }
        int num = 1;
        foreach (Exercise item in _exercises)
        {
            if (item.GetStatus() != CourseMaterialStatus._published)
            {
                throw new Exception($"Exercise {num} not published");
            }
            num ++;
        }
        
    }
    //
}