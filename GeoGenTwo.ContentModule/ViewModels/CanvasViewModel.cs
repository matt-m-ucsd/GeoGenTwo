using GeoGenTwo.Core.Interfaces;
using GeoGenTwo.Core.Mvvm;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Windows.Forms.AxHost;

namespace GeoGenTwo.ContentModule.ViewModels
{
    public class CanvasViewModel : RegionViewModelBase
    {
        #region Fields

        private ObservableCollection<Line> _lines;
        private IEventAggregator _eventAggregator;
        private ISettings _settings;
        private Brush _canvasBrush;

        #endregion

        #region Properties

        public ISettings Settings
        {
            get { return _settings; }
            set 
            { 
                SetProperty(ref _settings, value);
                foreach (Line line in Lines)
                {
                    line.Fill = Settings.LineBrush;
                    line.Stroke = Settings.LineBrush;
                }
            }
        }

        public ObservableCollection<Line> Lines
        {
            get { return _lines; }
            set { SetProperty(ref _lines, value); }
        }

        public Brush CanvasBrush
        {
            get { return _canvasBrush; }
            set { SetProperty(ref _canvasBrush, value); }
        }

        #endregion

        #region Constructor/Destructor

        public CanvasViewModel(IEventAggregator eventAggregator, ISettings settings) : base()
        {
            Lines = new ObservableCollection<Line>();
            Settings = settings;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(OnSettingsChangedEventReceived);
            _eventAggregator.GetEvent<GeneratePatternEvent>().Subscribe(OnGeneratePatternEventReceived);
        }

        public override void Destroy()
        {
            _eventAggregator.GetEvent<SettingsChangedEvent>().Unsubscribe(OnSettingsChangedEventReceived);
            _eventAggregator.GetEvent<GeneratePatternEvent>().Unsubscribe(OnGeneratePatternEventReceived);
        }

        #endregion

        #region Callbacks

        private void OnSettingsChangedEventReceived(ISettings settings)
        {
            Settings = settings;
            CanvasBrush = settings.BackgroundBrush;
        }

        private void OnGeneratePatternEventReceived()
        {
            GenerateLines();
        }

        #endregion

        #region Methods

        private void GenerateLines()
        {
            Random random = new Random();
            int numLines = Settings.NumLines;

            Lines.Clear(); // Clear the existing lines

            for (int i = 0; i < numLines; i++)
            {
                Line line = new Line()
                {
                    X1 = 0, // Randomly generate X1 coordinate
                    X2 = Settings.PortraitResolution.Width / 2, // base X2 coordinate
                    Y2 = random.Next(0, Settings.PortraitResolution.Height / 2), // Randomly generate Y2 coordinate
                    Y1 = random.Next(0, Settings.PortraitResolution.Height / 2), // base Y1 coordinate

                    Fill = Settings.LineBrush, // Set line color
                    Stroke = Settings.LineBrush
                };

                // Check for intersection with existing lines
                foreach (var existingLine in Lines)
                {
                    if (FoundIntersection(line, existingLine, out double x, out double y))
                    {
                        // Update the endpoint of the current line to the intersection point
                        // IFF new line is shorter than previous
                        if (LineLength(line.X1, line.X2, line.Y1, line.Y2) > LineLength(line.X1, x, line.Y1, y))
                        {
                            line.X2 = x;
                            line.Y2 = y;
                        }
                    }
                }

                Lines.Add(line); // Add the line to the collection
            }

            List<Line> allMirroredLines = new List<Line>();

            foreach (Line line in Lines)
            {
                List<Line> mirroredLines = GenerateMirroredLines(line, Settings.PortraitResolution.Width, Settings.PortraitResolution.Height);
                foreach (Line mirrorLine in mirroredLines)
                {
                    allMirroredLines.Add(mirrorLine);
                }
            }

            foreach (Line line in allMirroredLines)
            {
                Lines.Add(line);
            }
        }

        public List<Line> GenerateMirroredLines(Line inputLine, double canvasWidth, double canvasHeight)
        {
            List<Line> mirroredLines = new List<Line>();

            // Generate the mirrored lines in the other three quadrants
            Line mirroredLineTopRight = new Line
            {
                X1 = canvasWidth - 1,
                Y1 = inputLine.Y1,
                X2 = canvasWidth - inputLine.X2,
                Y2 = inputLine.Y2,
                Fill = Settings.LineBrush,
                Stroke = Settings.LineBrush
            };

            Line mirroredLineBottomLeft = new Line
            {
                X1 = inputLine.X1,
                Y1 = canvasHeight - inputLine.Y1,
                X2 = inputLine.X2,
                Y2 = canvasHeight - inputLine.Y2,
                Fill = Settings.LineBrush,
                Stroke = Settings.LineBrush
            };

            Line mirroredLineBottomRight = new Line
            {
                X1 = canvasWidth - 1,
                Y1 = canvasHeight - inputLine.Y1,
                X2 = canvasWidth - inputLine.X2,
                Y2 = canvasHeight - inputLine.Y2,
                Fill = Settings.LineBrush,
                Stroke = Settings.LineBrush
            };

            // Add the mirrored lines to the list
            mirroredLines.Add(mirroredLineTopRight);
            mirroredLines.Add(mirroredLineBottomLeft);
            mirroredLines.Add(mirroredLineBottomRight);

            return mirroredLines;
        }

        private double LineLength(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(((y2 - y1) * (y2 - y1)) + ((x2 - x1) * (x2 - x1)));
        }

        private bool FoundIntersection(Line line1, Line line2, out double x, out double y)
        {
            double yDiff = line1.Y2 - line1.Y1;
            double xDiff = line1.X2 - line1.X1;
            double slope1 = yDiff / xDiff;
            yDiff = line2.Y2 - line2.Y1;
            xDiff = line2.X2 - line2.X1;
            double slope2 = yDiff / xDiff;

            if (slope1 == slope2)
            {
                x = y = 0;
                return false;
            }

            double b1 = line1.Y1 - (slope1 * line1.X1);
            double b2 = line2.Y1 - (slope2 * line2.X1);

            double interX = (b2 - b1) / (slope1 - slope2);
            double interY = (slope1 * interX) + b1;

            if ((interX <= line2.X2) && (interX > 0) && (interY > 0))
            {
                x = interX;
                y = interY;
                return true;
            }

            x = -1;
            y = -1;

            return false;
        }

        #endregion
    }
}
