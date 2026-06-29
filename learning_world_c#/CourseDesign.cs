using System.Data.Common;

public class CourseDesign
{
    private Forum _forum = null!;
    private string _courseId = null!;
    private string _CourseTitle = null!;
    private string _briefInfo = null!;
    private Exam _exam = null!;
    private GradingPolicy _gradingPolicy = null!;
    private List<Lesson > _lessons = new List<Lesson>();
    private List<InstructionalDesigner> _team = new List<InstructionalDesigner>();
    private List<List<Dictionary<string,bool>>> _consents = new List<List<Dictionary<string, bool>>>();
    private CreationStatus _creationStatus = CreationStatus._Id;
    private UpdateStatus _updateStatus;

    public void SetCourseId(string Id,string courseId)
    {
        ValidateInput(Id);
        ValidateInput(courseId);
        Propose();
        Inform($"{Id}:  Generated a course identifier; review the Course Blueprint for more details.");
        ConfirmApproval();
        Update();
        _courseId = courseId;
        Default();
    }
    public void Notify(string Text)
    {
        foreach (InstructionalDesigner item in _team)
        {
            item.AddToInbox(new Notification("Creation Team",Text,DateTime.UtcNow));
        }
    }
    public void Consent(bool value,string id)
    {
        if (_consents.Count == 0)
        {
           var statuses =  Enum.GetValues<CreationStatus>();
           foreach (CreationStatus item in statuses)
           {
              _consents.Add(new List<Dictionary<string, bool>>());
           }
        }
        if (_consents[(int)_creationStatus].Count > _team.Count)
        {
            throw new IndexOutOfRangeException("Failed!");
        }
        _consents[(int)_creationStatus].Add(new Dictionary<string,bool>{{id,value}});
          
    }
    public void Propose()
    {
        if (_updateStatus != UpdateStatus._NA)
        {
            throw new InvalidOperationException("Invalid state, cannot propose");
        }
        _updateStatus = UpdateStatus._proposed;
    }
    public void Inform(string text)
    {
        if (_updateStatus != UpdateStatus._proposed )
        {
            throw new InvalidOperationException("Failed, no proposal yet.");
        }
        Notify(text);
        _updateStatus = UpdateStatus._awaitingApproval;
    }
    public void ConfirmApproval()
    {
        if (_updateStatus != UpdateStatus._awaitingApproval)
        {
            throw new InvalidOperationException("Not awaiting approval, wrong call");
        }
        if (_consents[(int) _creationStatus].Count != _team.Count)
        {
            throw new InvalidOperationException("All teammates have not responded");
        }
        int num = 0;
        foreach (Dictionary<string, bool> item in _consents[(int) _creationStatus])
        {
            foreach (KeyValuePair<string,bool> pair in item)
            {
                string id = pair.Key;
                bool value = pair.Value;
                if (value == true)
                {
                    num++;
                }
            }
        }
        if (num != _team.Count)
        {
            Notify("Proposal is not been approved by all teammates.Failed");
            Default();
            return;
        }
        _updateStatus = UpdateStatus._approved;
        Notify("Proposal Approved");
    }
    public void Update()
    {
        if (_creationStatus == CreationStatus._completed)
        {
            throw new InvalidOperationException("All requirements for course creation has been met.");
        }
        if (_updateStatus != UpdateStatus._approved)
        {
            throw new InvalidOperationException("Not approved, cannot update");
        }
        _updateStatus  = UpdateStatus._approved;
        _creationStatus = (CreationStatus) ((int)_creationStatus + 1);
        Notify("Updated. Changes made has been recorded");
    }
    public void Default()
    {
        if (_updateStatus == UpdateStatus._NA)
        {
            throw new Exception("Already refreshed");
        }
        _updateStatus = UpdateStatus._NA;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
}