public class diSizeUP : DisposableItemBase
{
    public string Name {get;set;} = "SizeUP";
    public override void Use()
    {
        _player.SizeUp();
        var item = gameObject.GetComponent<diSizeUP>();
        Destroy(item);
    }
}