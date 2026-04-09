public class MobPool : BaseObjectPool<Mob>
{
    public new Mob Get()
    {
        return base.Get();
    }
}