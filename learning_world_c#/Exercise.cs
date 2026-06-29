using System.Globalization;
using System.IO.Pipes;
using System.Runtime.CompilerServices;

public class Exercise : LearningMaterial
{
    private string _question;
    private List<ExerciseOption> _options = new List<ExerciseOption>();
  
    public Exercise(string question)
    {
        _question = question;
    }
    public int ExerciseOptionCount()
    {
        return _options.Count;
    }
    public void AddOption(ExerciseOption option)
    {
        if (GetStatus() != CourseMaterialStatus._draft)
        {
            throw new Exception("Failed. Unable to alter exercise, modify state.");
        }
         _options.Add(option); 
    }
    public bool Evaluate(int answer)
    {
        if (answer < 0 || answer >= _options.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(answer),"Answer out of range.");
        }
        if (_options[answer].GetCorrectStatus() == true)
        {
           return true;
        }
        return  false;
    }
    protected override void ValidatePublish()
    {
        if (_options.Count == 0)
        {
            throw new Exception("Cannot publish an exercise without options.");
        }
        ExerciseOption? foundCorrectAnswer = _options.Find(O => O.GetCorrectStatus() == true);
        if (foundCorrectAnswer == null)
        {
            throw new Exception("No correct answer for exercise yet.");
        }
        if (string.IsNullOrEmpty(_question))
        {      
            throw new ArgumentNullException(nameof(_question),"Empty Input.");
        }
        int num = 0;
        foreach (ExerciseOption item in _options)
        {
            if (item.GetCorrectStatus())
            {
                num ++;
            }
        }
        if (num > 1)
        {
        throw new Exception($"Only one option should be valid,{num ++} found.");
        }
        
    }
    public void DisplayExercise()
    {
        int num = 1;
        Console.WriteLine($">>> {_question}");
        foreach (ExerciseOption option in _options)
        {
            Console.WriteLine($"{num}. {option.DisplayOption()}");
            num++;
        }
    }
    public void RemoveOption(int optionIndex)
    {
         if (GetStatus() != CourseMaterialStatus._draft)
        {
            throw new Exception("Failed. Unable to alter exercise, modify state.");

        }
        if(optionIndex < 0 || optionIndex >= _options.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(optionIndex),"Specified index out of range.");
        }
         _options.RemoveAt(optionIndex);
    }  
    //
}