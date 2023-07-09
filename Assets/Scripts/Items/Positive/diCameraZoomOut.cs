public class diCameraZoomOut : DisposableItemBase
{
    public override string Name {get;set;} = "Zoom Out";
    public override void Use() => _gameHandler.ChangeCameraZoom(2);
    
}