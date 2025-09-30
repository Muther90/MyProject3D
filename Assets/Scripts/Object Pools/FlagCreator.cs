public class FlagCreator : BaseObjectPool<Flag>
{
    public Flag Create() => Get();
}