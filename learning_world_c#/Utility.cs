public static class Utility
{
   public static string ValidateString(string Input)
    {
        if (string.IsNullOrEmpty(Input))
        {
            throw new ArgumentNullException(nameof(Input),"Input provided is empty.");
        }
        return Input;
    }
}