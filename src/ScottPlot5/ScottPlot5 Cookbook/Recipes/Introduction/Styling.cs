﻿namespace ScottPlotCookbook.Recipes.Introduction;

public class Styling : ICategory
{
    public string Chapter => "Introduction";
    public string CategoryName => "Styling Plots";
    public string CategoryDescription => "How to customize appearance of plots";

    public class StyleClass : RecipeBase
    {
        public override string Name => "Style Helper Functions";
        public override string Description => "Plots contain many objects which can be customized individually " +
            "by assigining to their public properties, but helper methods exist in the Plot's Style object " +
            "that make it easier to customzie many items at once using a simpler API.";

        [Test]
        public override void Execute()
        {
            myPlot.Add.Signal(Generate.Sin(51));
            myPlot.Add.Signal(Generate.Cos(51));

            // visible items have public properties that can be customized
            myPlot.Axes.Bottom.Label.Text = "Horizontal Axis";
            myPlot.Axes.Left.Label.Text = "Vertical Axis";
            myPlot.Axes.Title.Label.Text = "Plot Title";

            // the Style object contains helper methods to easily style many items at once
            myPlot.Style.Background(figure: Color.FromHex("#07263b"), data: Color.FromHex("#0b3049"));
            myPlot.Style.ColorAxes(Color.FromHex("#a0acb5"));
            myPlot.Style.ColorGrids(Color.FromHex("#0e3d54"));
        }
    }

    public class AxisCustom : RecipeBase
    {
        public override string Name => "Axis Customization";
        public override string Description => "Axis labels, tick marks, and frame can all be customized.";

        [Test]
        public override void Execute()
        {
            myPlot.Add.Signal(Generate.Sin(51));
            myPlot.Add.Signal(Generate.Cos(51));

            myPlot.Axes.Title.Label.Text = "Plot Title";
            myPlot.Axes.Title.Label.ForeColor = Colors.RebeccaPurple;
            myPlot.Axes.Title.Label.FontSize = 32;
            myPlot.Axes.Title.Label.FontName = Fonts.Serif;
            myPlot.Axes.Title.Label.Rotation = -5;
            myPlot.Axes.Title.Label.Bold = false;

            myPlot.Axes.Left.Label.Text = "Vertical Axis";
            myPlot.Axes.Left.Label.ForeColor = Colors.Magenta;
            myPlot.Axes.Left.Label.Italic = true;

            myPlot.Axes.Bottom.Label.Text = "Horizontal Axis";
            myPlot.Axes.Bottom.Label.Bold = false;
            myPlot.Axes.Bottom.Label.FontName = Fonts.Monospace;

            myPlot.Axes.Bottom.MajorTickStyle.Length = 10;
            myPlot.Axes.Bottom.MajorTickStyle.Width = 3;
            myPlot.Axes.Bottom.MajorTickStyle.Color = Colors.Magenta;
            myPlot.Axes.Bottom.MinorTickStyle.Length = 5;
            myPlot.Axes.Bottom.MinorTickStyle.Width = 0.5f;
            myPlot.Axes.Bottom.MinorTickStyle.Color = Colors.Green;
            myPlot.Axes.Bottom.FrameLineStyle.Color = Colors.Blue;
            myPlot.Axes.Bottom.FrameLineStyle.Width = 3;

            myPlot.Axes.Right.FrameLineStyle.Width = 0;
        }
    }

    public class Palette : RecipeBase
    {
        public override string Name => "Palettes";
        public override string Description => "A palette is a set of colors, and the Plot's palette " +
            "defines the default colors to use when adding new plottables. ScottPlot comes with many " +
            "standard palettes, but users may also create their own.";

        [Test]
        public override void Execute()
        {
            // change the default palette used when adding new plottables
            myPlot.Add.Palette = new ScottPlot.Palettes.Nord();

            for (int i = 0; i < 5; i++)
            {
                double[] data = Generate.Sin(100, phase: -i / 20.0f);
                var sig = myPlot.Add.Signal(data);
                sig.LineWidth = 3;
            }
        }
    }

    public class Markers : RecipeBase
    {
        public override string Name => "Markers";
        public override string Description => "Many plot types have a MarkerStyle which can be customized.";

        [Test]
        public override void Execute()
        {
            MarkerShape[] markerShapes = Enum.GetValues<MarkerShape>().ToArray();

            for (int i = 0; i < markerShapes.Length; i++)
            {
                double[] xs = Generate.Consecutive(20);
                double[] ys = Generate.Sin(20, offset: markerShapes.Length - i);
                var scatter = myPlot.Add.Scatter(xs, ys);
                scatter.MarkerStyle.Shape = markerShapes[i];
                scatter.MarkerStyle.Size = 10;
            }
        }
    }

    public class MarkerNames : RecipeBase
    {
        public override string Name => "Marker Names";
        public override string Description => "Markers can be referred to by their name.";

        [Test]
        public override void Execute()
        {
            MarkerShape[] markerShapes = Enum.GetValues<MarkerShape>().ToArray();
            ScottPlot.Palettes.Category20 palette = new();

            for (int i = 0; i < markerShapes.Length; i++)
            {
                var mp = myPlot.Add.Marker(x: i, y: 0);
                mp.MarkerStyle.Shape = markerShapes[i];
                mp.MarkerStyle.Size = 10;
                mp.MarkerStyle.Outline.Width = 1.5f;
                mp.MarkerStyle.Outline.Color = palette.GetColor(i);
                mp.MarkerStyle.Fill.Color = palette.GetColor(i).WithAlpha(.5);

                var txt = myPlot.Add.Text(markerShapes[i].ToString(), i, 0.15);
                txt.Label.Rotation = -90;
                txt.Label.Alignment = Alignment.MiddleLeft;
            }

            myPlot.Title("Marker Names");
            myPlot.Axes.SetLimits(-1, markerShapes.Length, -1, 4);
            myPlot.HideGrid();
        }
    }

    public class LineStyles : RecipeBase
    {
        public override string Name => "Line Styles";
        public override string Description => "Many plot types have a LineStyle which can be customized.";

        [Test]
        public override void Execute()
        {
            LinePattern[] linePatterns = Enum.GetValues<LinePattern>().ToArray();

            for (int i = 0; i < linePatterns.Length; i++)
            {
                LinePattern pattern = linePatterns[i];

                var line = myPlot.Add.Line(0, -i, 1, -i);
                line.LinePattern = pattern;
                line.LineWidth = 2;
                line.Color = Colors.Black;

                var txt = myPlot.Add.Text(pattern.ToString(), 1.1, -i);
                txt.Size = 18;
                txt.Bold = true;
                txt.Color = Colors.Black;
                txt.Label.Alignment = Alignment.MiddleLeft;
            }

            myPlot.Axes.Margins(right: 1);
            myPlot.HideGrid();
            myPlot.Layout.Frameless();

            myPlot.ShowLegend();
        }
    }

    public class Scaling : RecipeBase
    {
        public override string Name => "Scaling";
        public override string Description => "All components of an image can be scaled up or down in size " +
            "by adjusting the ScaleFactor property. This is very useful for creating images that look nice " +
            "on high DPI displays with display scaling enabled.";

        [Test]
        public override void Execute()
        {
            myPlot.ScaleFactor = 2;
            myPlot.Add.Signal(Generate.Sin());
            myPlot.Add.Signal(Generate.Cos());
        }
    }

    public class DarkMode : RecipeBase
    {
        public override string Name => "Dark Mode";
        public override string Description => "Plots can be created using dark mode " +
            "by setting the colors of major plot components to ones consistent with a dark theme.";

        [Test]
        public override void Execute()
        {
            // set the color palette used when coloring new items added to the plot
            myPlot.Add.Palette = new ScottPlot.Palettes.Penumbra();

            // add things to the plot
            for (int i = 0; i < 5; i++)
            {
                var sig = myPlot.Add.Signal(Generate.Sin(51, phase: -.05 * i));
                sig.LineWidth = 3;
                sig.Label = $"Line {i + 1}";
            }
            myPlot.XLabel("Horizontal Axis");
            myPlot.YLabel("Vertical Axis");
            myPlot.Title("ScottPlot 5 in Dark Mode");
            myPlot.ShowLegend();

            // change figure colors
            myPlot.Style.ColorAxes(Color.FromHex("#d7d7d7"));
            myPlot.Style.ColorGrids(Color.FromHex("#404040"));
            myPlot.Style.Background(
                figure: Color.FromHex("#181818"),
                data: Color.FromHex("#1f1f1f"));
            myPlot.Style.ColorLegend(
                background: Color.FromHex("#404040"),
                foreground: Color.FromHex("#d7d7d7"),
                border: Color.FromHex("#d7d7d7"));
        }
    }
}
