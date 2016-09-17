using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace WPFDice
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private int dice;
        public int Dice { get { return dice; } set { dice = value; OnPropertyChanged("Dice"); } }

        public MyCamera Cam { get; private set; }
        //public string Positions { get; private set; }
        //public string Indices { get; private set; }

        public ViewModel()
        {
            Cam = new MyCamera(0, -4, 0, 0, 0, 0, 0, 0, 1);//0, -10, 0 - cam helye , 0, 0, 0- ide nez a cam, 0, 0, 1 - felfele vektor 
            //Positions = "0 0 0, 1 0 0, 0 1 0, 1 1 0, 0 0 1, 1 0 1, 0 1 1, 1 1 1";
            //Indices = "2 3 1, 2 1 0, 7 1 3, 7 5 1, 6 5 7, 6 4 5, 2 0 4,2 4 6,2 7 3, 2 6 7, 0 1 5, 0 5 4";
        }

        static Pen BlackPen = new Pen(Brushes.Black, 1);
        public DrawingImage GetImage(int i)
        {
            int side = 100;
            var random = new Random();
            var pixels = new byte[side * side * 4];
            random.NextBytes(pixels);
            BitmapSource bitmapSource = BitmapSource.Create(side, side, side, side, PixelFormats.Pbgra32, null, pixels, side * 4);
            var visual = new DrawingVisual();
            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawImage(bitmapSource, new Rect(0, 0, side, side));
                drawingContext.DrawRectangle(Brushes.White, BlackPen, new Rect(0, 0, side, side));
                switch (i)
                {
                    case 0:
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 2, side / 2), 5, 5);
                        break;
                    case 1:
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side * 3 / 4), 5, 5);
                        break;
                    case 2:
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 2, side / 2), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side * 3 / 4), 5, 5);
                        break;
                    case 3:
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side * 3 / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side * 3 / 4), 5, 5);
                        break;
                    case 4:
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side * 3 / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side * 3 / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 2, side / 2), 5, 5);
                        break;
                    case 5:
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 4, side * 3 / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 2, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side / 2, side * 3 / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side / 4), 5, 5);
                        drawingContext.DrawEllipse(Brushes.Black, BlackPen, new Point(side * 3 / 4, side * 3 / 4), 5, 5);
                        break;
                    default:
                        drawingContext.DrawText(
                                            new FormattedText("A" + i, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                                                new Typeface("Segoe UI"), 64, Brushes.Black), new Point(0, 0));
                        break;
                }
            }
            return new DrawingImage(visual.Drawing);
        }

        public ImageBrush GetBrush(int i)
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = GetImage(i);
            return ib;
        }

        public Model3DGroup Group
        {
            get
            {
                Model3DGroup output = new Model3DGroup();

                Int32Collection indices = Int32Collection.Parse("0 3 2, 0 1 3");
                PointCollection textureCoords = PointCollection.Parse("0 0, 0 1, 1 0, 1 1");
                string[] positions = { // positions.length = 6
                                         "0 0 0, 0 0 1, 0 1 0, 0 1 1", // x=0 FRONT
                                         "0 1 0, 0 1 1, 1 1 0, 1 1 1", // y=1 FRONT
                                         "0 0 0, 0 1 0, 1 0 0, 1 1 0", // z=0 FRONT
                                         // BACK = (xchange 1 and 2)
                                         "1 0 0, 1 1 0, 1 0 1, 1 1 1", // x=1 BACK 
                                         "0 0 0, 1 0 0, 0 0 1, 1 0 1", // y=0 BACK
                                         "0 0 1, 1 0 1, 0 1 1, 1 1 1"  // z=1 BACK
                                     };

                for (int i = 0; i < positions.Length; i++)
                {
                    GeometryModel3D model = new GeometryModel3D();
                    model.Geometry = new MeshGeometry3D()
                    {
                        Positions = Point3DCollection.Parse(positions[i]), //< az i.négyzethez tartozó 4 pont >
                        TriangleIndices = indices, //< az i.négyzethez 2 háromszög >
                        TextureCoordinates = textureCoords //< az i.négyzethez a textúra koordináták >
                    };
                    model.Material = new DiffuseMaterial(GetBrush(i));
                    model.BackMaterial = new DiffuseMaterial(Brushes.White);
                    output.Children.Add(model);
                }
                return output;
            }
        }

        public void Move(double dist)
        {
            Cam.Move(dist); OnPropertyChanged("Cam");
        }
        public void Strafe(double dist)
        {
            Cam.Strafe(dist); OnPropertyChanged("Cam");
        }
        public void Fly(double dist)
        {
            Cam.Fly(dist); OnPropertyChanged("Cam");
        }
        public void Roll(double angle)
        {
            Cam.Roll(angle); OnPropertyChanged("Cam");
        }
        public void Pitch(double angle)
        {
            Cam.Pitch(angle); OnPropertyChanged("Cam");
        }
        public void Yaw(double angle)
        {
            Cam.Yaw(angle); OnPropertyChanged("Cam");
        }

    }

}