public class BossPool : BaseObjectPool<Boss>
{
    public new Boss Get()
    {
        return base.Get();
    }
}