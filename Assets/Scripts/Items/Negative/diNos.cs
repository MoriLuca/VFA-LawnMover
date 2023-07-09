public class diNos : DisposableItemBase
{
    public override string Name {get;set;} = "NOS";
    public override void Use()
    {
        _player.IncreseNOS(3);
    }
}