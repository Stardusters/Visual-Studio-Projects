using BarcodeScanner.Views;
using DCVXamarin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BarcodeScanner
{
    public partial class MainPage : ContentPage, ILicenseVerificationListener
    {
        private IDCVCameraEnhancer camera;
        private IDCVBarcodeReader barcodeReader;
        public MainPage(IDCVCameraEnhancer dce, IDCVBarcodeReader dbr)
        {
            camera = dce;
            barcodeReader = dbr;
            barcodeReader.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ==", this);
            InitializeComponent();
        }
        async void OnStartScanningButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScanningPage(camera, barcodeReader));
        }

        public void LicenseVerificationCallback(bool isSuccess, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
