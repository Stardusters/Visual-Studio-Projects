using DCVXamarin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeScanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanningPage : ContentPage
    {
        private IDCVCameraEnhancer camera;
        private IDCVBarcodeReader barcodeReader;
        public ScanningPage(IDCVCameraEnhancer dce, IDCVBarcodeReader dbr)
        {
            camera = dce;
            barcodeReader = dbr;

            InitializeComponent();

            barcodeReader.SetCameraEnhancer(camera);
            barcodeReader.AddResultListener(this);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            camera.Open();
            barcodeReader.StartScanning();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            camera.Close();
            barcodeReader.StopScanning();
        }

        public void BarcodeResultCallback(int frameID, BarcodeResult[] barcodeResults)
        {
            string newBarcodeText = "";
            if (barcodeResults != null && barcodeResults.Length > 0)
            {
                for (int i = 0; i < barcodeResults.Length; i++)
                {
                    {
                        newBarcodeText += barcodeResults[i].BarcodeText;
                        newBarcodeText += "\n ";
                    }
                }
            }
            else
            {
                Console.WriteLine("test");
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                barcodeResultLabel.Text = newBarcodeText;
            });
        }
    }
}