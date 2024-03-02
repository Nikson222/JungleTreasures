namespace DataBases
{
    public interface IElementsDB
    {
        ElementsDB.ElementInfo GetElementInfo(ElementType type);
        ElementType GetRandomElement();
    }
}