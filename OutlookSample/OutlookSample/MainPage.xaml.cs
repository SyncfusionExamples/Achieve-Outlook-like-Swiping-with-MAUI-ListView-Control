using Syncfusion.Maui.Core.Carousel;

namespace OutlookSample
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
        }

        private void listView_SwipeEnded(object sender, Syncfusion.Maui.ListView.SwipeEndedEventArgs e)
        {
            if (e.Offset <= 100)
            {
                return;
            }

            if (e.Direction == SwipeDirection.Right)
            {
                viewModel.ArchiveCommand.Execute(e.DataItem);
            }

            if (e.Direction == SwipeDirection.Left)
            {
                viewModel.DeleteCommand.Execute(e.DataItem);
            }
        }

    }

}
