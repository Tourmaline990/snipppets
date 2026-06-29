public class InstructionalDesigner
{
    private string _accountId;
    private string _name;
    private List<Notification> _inbox = new List<Notification>();
    private CourseDesign _courseDesign;
    public void AddToInbox(Notification notification)
    {
        _inbox.Add(notification);
    }
}