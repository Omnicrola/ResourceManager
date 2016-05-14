using System.Windows.Media;

namespace ResourceManagment.Windows.ManagePeople
{
    public class SelectablePersonViewModel
    {

        public SelectablePersonViewModel(PersonViewModel person, bool isSelectable)
        {
            Person = person;
            IsSelectable = isSelectable;
            TextColor = isSelectable ? Brushes.Black : Brushes.Gray;
        }

        public PersonViewModel Person { get; set; }
        public Brush TextColor { get; set; }
        public bool IsSelectable { get; set; }
        public string Name { get { return Person.ToString(); } set { } }

    }
}