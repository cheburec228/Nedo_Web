using System.Collections.Generic;

public class QuestionViewModel
{
    public string Question { get; set; }
    public QuestionType Type { get; set; }
    public List<string> Options { get; set; }
}
