using CkmModelingApp;
using System;
//using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KMconsole
{
    public class ImpulseModeling
    {
        private CkmModel model;

        public ImpulseModeling(CkmModel ckmModel)
        {
            model = ckmModel;
        }

        public void RunImpulseModeling()
        {
            var chart = new Chart();
            chart.Dock = DockStyle.Fill;
            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            var seriesPositive = new Series("Positive");
            seriesPositive.ChartType = SeriesChartType.Line;
            var seriesNegative = new Series("Negative");
            seriesNegative.ChartType = SeriesChartType.Line;

           for (int i = 0; i < 10; i++)
            {
              seriesPositive.Points.AddXY(i, i + 1);   // Положительная ситуация
               seriesNegative.Points.AddXY(i, 10 - i);  // Негативная ситуация
           }

            chart.Series.Add(seriesPositive);
            chart.Series.Add(seriesNegative);

            var form1 = new Form();
            form1.Controls.Add(chart);
            form1.ShowDialog();
        }
    }
}