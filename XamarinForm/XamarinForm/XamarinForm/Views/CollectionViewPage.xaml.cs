using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinForm.ViewModels;

namespace XamarinForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionViewPage : ContentPage
    {
        public CollectionViewPage()
        {
            InitializeComponent();

            BindingContext = new NameListViewModel();
        }
    }
}