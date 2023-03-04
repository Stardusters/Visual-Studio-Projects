using XamarinForm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NameListPage : ContentPage
    {
        public NameListPage()
        {
            InitializeComponent();

            BindingContext = new NameListViewModel();
        }
    }
}