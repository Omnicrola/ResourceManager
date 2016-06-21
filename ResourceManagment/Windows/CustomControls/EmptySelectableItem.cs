namespace ResourceManagment.Windows.CustomControls
{
    internal class EmptySelectableItem : ISelectableItem
    {
        public static readonly ISelectableItem INSTANCE = new EmptySelectableItem();

        private EmptySelectableItem() { }
        public bool IsSelectable
        {
            get { return false; }
            set { }
        }

        public override string ToString()
        {
            return "---";
        }
    }
}