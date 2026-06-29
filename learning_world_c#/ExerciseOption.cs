public class ExerciseOption 
{
    private string _option;
    private bool _correct;
    
    public ExerciseOption(string option)
    {
        _option = ValidateInput(option);
        _correct = false;
    }
    public ExerciseOption(string option,bool correct)
    {
        _option = ValidateInput(option);
        _correct = correct;
    }
    private string ValidateInput(string Param)
    {
        if (string.IsNullOrEmpty(Param))
        {
            throw new ArgumentNullException(nameof(Param),"Empty Input.");
        }
        return Param;
    }
    public string DisplayOption()
    {
        return $">>> {_option}";
    }
    public bool GetCorrectStatus()
    {
        return _correct;
    }
   
 
}