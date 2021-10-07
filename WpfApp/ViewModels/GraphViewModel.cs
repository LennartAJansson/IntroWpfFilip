using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using ScottPlot;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    public class GraphViewModel : ObservableObject
    {
        public WpfPlot PlotControl { get; set; }

        public IEnumerable<string> ChartTypes { get; set; } = new string[] { "Line", "Scatter", "Spider" };

        public string SelectedChartType
        {
            get => selectedChartType;
            set
            {
                SetProperty(ref selectedChartType, value);
                UpdateGraph();
            }
        }

        private void UpdateGraph()
        {
            PlotControl.Reset();

            switch (SelectedChartType)
            {
                case "Spider":
                    double[,] values = { { 1, 2, 3, 4, 5, 6 }, { 10, 20, 30, 40, 50, 60 } };
                    var radarPlot = PlotControl.Plot.AddRadar(values: values, disableFrameAndGrid: true);
                    radarPlot.AxisType = RadarAxis.Polygon;
                    radarPlot.ShowAxisValues = false;
                    break;
                case "Line":
                    double x1 = 1;
                    double x2 = 10;
                    double y1 = 1;
                    double y2 = 5;
                    var linePlot = PlotControl.Plot.AddLine(x1, y1, x2, y2);
                    //linePlot.
                    break;
                case "Scatter":
                    double[] dataX = new double[] { 1, 2, 3, 4, 5 };
                    double[] dataY = new double[] { 1, 4, 9, 16, 25 };
                    var scatterPlot = PlotControl.Plot.AddScatter(dataX, dataY);
                    //scatterPlot.
                    break;
                default:
                    break;
            }

            PlotControl.Render();
            PlotControl.Plot.Render();
        }

        private string selectedChartType = "Spider";

        public AsyncRelayCommand<WpfPlot> LoadedCommand { get; }

        public GraphViewModel()
        {
            LoadedCommand = new AsyncRelayCommand<WpfPlot>(LoadedWpfPlot);
        }

        private Task LoadedWpfPlot(WpfPlot plotControl)
        {
            PlotControl = plotControl;
            PlotControl.Width = 600;// Width;
            PlotControl.Height = 600;// Height;
            SelectedChartType = "Spider";

            return Task.CompletedTask;
        }
    }
}
