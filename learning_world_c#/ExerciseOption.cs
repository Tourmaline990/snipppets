public class ExerciseOption
{
    private string _option;
    private bool _correct;
    
    public ExerciseOption(string option)
    {
        _option = option;
        _correct = false;
    }
    public ExerciseOption(string option,bool correct)
    {
        _option = option;
        _correct = correct;
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