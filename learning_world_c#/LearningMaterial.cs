public  abstract class LearningMaterial
{
    private CourseMaterialStatus _learningMaterialStatus = CourseMaterialStatus._draft;

    public void Publish()
    {
        ValidatePublish();
        if (_learningMaterialStatus != CourseMaterialStatus._draft)
        {
            throw new Exception("only drafted materials can be published");
        }
        _learningMaterialStatus = CourseMaterialStatus._published;
    }
    public void Archive()
    {
         if (_learningMaterialStatus == CourseMaterialStatus._archived)
        {
            throw new Exception("Archived already.");
        }
        _learningMaterialStatus = CourseMaterialStatus._archived;
    }
    public void Recover()
    {
         if (_learningMaterialStatus != CourseMaterialStatus._archived)
        {
            throw new Exception("Only archived courses can be recovered.");
        }
        _learningMaterialStatus = CourseMaterialStatus._draft;
    }
    protected abstract void ValidatePublish();
    public CourseMaterialStatus GetStatus()
    {
        return _learningMaterialStatus;
    }
    public void SetStatus(CourseMaterialStatus stats)
    {
       _learningMaterialStatus = stats;
    }
    public  T RemoveMaterial<T>(int index, List<T> materials)
         where T: LearningMaterial
    {
        if (_learningMaterialStatus != CourseMaterialStatus._draft)
        {
            throw new Exception("Failed. Unable to alter exercise, modify state.");

        }
        if(index < 1 || index >= materials.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index),"Specified index out of range.");
        }
        T val = materials[index];
        materials.Remove(val);
        return val;
    }
    public void AddMaterial<T>(List<T> material,T item)
    where T : LearningMaterial
    {
        if (_learningMaterialStatus != CourseMaterialStatus._draft)
        {
            throw new Exception("Failed. Unable to alter exercise, modify state.");
        }
         material.Add(item); 
    }
}