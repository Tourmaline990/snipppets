using System.Globalization;
using System.Runtime.CompilerServices;

public class Exercise
{
    private string _question;
    private List<ExerciseOption> _options = new List<ExerciseOption>();
    private int _score;
    private bool _isCompleted = false;
    
    public Exercise(string question)
    {
        _question = question;
        
    }
    public int ExerciseOptionCount()
    {
        return _options.Count - 1;
    }
    public void AddOption(ExerciseOption option)
    {
        int num = 0;
        List<bool> bools = new List<bool>();
        if (_options.Count != 0)
         {
           foreach (ExerciseOption item in _options)
           {
            bools.Add(item.GetCorrectStatus());
           } 
           foreach (bool item in bools)
            {
             if (item == true)
                {
                    num += 1;
                }
            }
            if (num > 1)
            {
                throw new ArgumentException("Only one option should be true");
            }
            _options.Add(option);
         }
         else
         {
           _options.Add(option);   
         }
        
    }
    public void Evaluate(int answer)
    {
        if (answer < 1 || answer >= _options.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(answer),"Answer out of range.");
        }
        if (_options[answer].GetCorrectStatus() == true)
        {
           _score = 2; 
        }
        else
        {
            _score = 0;
        }
        _isCompleted = true;
    }
    public int GetScore()
    {
        return _score;
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
        if(optionIndex < 1 || optionIndex >= _options.Count){
            throw new ArgumentOutOfRangeException(nameof(optionIndex),"Specified index out of range.");
        }
        _options.RemoveAt(optionIndex);
    }
    public bool GetIsCompleted()
    {
        return _isCompleted;
    }
   
}