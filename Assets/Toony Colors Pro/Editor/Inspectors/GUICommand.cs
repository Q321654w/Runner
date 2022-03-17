namespace Toony_Colors_Pro.Editor.Inspectors
{
    internal class GUICommand
    {
        public virtual bool Visible() { return true; }
        public virtual void OnGUI() { }
    }
}