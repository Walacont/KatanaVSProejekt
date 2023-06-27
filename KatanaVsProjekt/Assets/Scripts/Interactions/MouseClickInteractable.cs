namespace haw.unitytutorium
{
    public class MouseClickInteractable : Interactable
    {
        private void OnMouseDown() => Notify();
    }
}