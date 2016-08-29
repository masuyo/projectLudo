using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BoardGame
{
    class LudoFrameworkElement : FrameworkElement
    {

        private bool fullBoardRender;
        List<IMan> menList;
        int[,] fieldIDMatrix;
        const int DIM = 11;
        int width = 600;
        int height = 600;
        int X_offset = 0;
        int Y_offset = 0;



        public LudoFrameworkElement()
        {
            fullBoardRender = true;
            menList = new List<IMan>();
            fieldIDMatrix = new int[DIM, DIM] {
                {  11, 12, 0 , 0 ,118,119,120, 0 , 0 , 21, 22},
                {  13, 14, 0 , 0 ,117,201,121, 0 , 0 , 23, 24},
                {  0 , 0 , 0 , 0 ,116,202,122, 0 , 0 , 0 , 0 },
                {  0 , 0 , 0 , 0 ,115,203,123, 0 , 0 , 0 , 0 },
                { 110,111,112,113,114,204,124,125,126,127,128},
                { 149,101,102,103,104, 0 ,304,303,302,301,129},
                { 148,147,146,145,144,404,134,133,132,131,130},
                {  0 , 0 , 0 , 0 ,143,403,135, 0 , 0 , 0 , 0 },
                {  0 , 0 , 0 , 0 ,142,402,136, 0 , 0 , 0 , 0 },
                {  41, 42, 0 , 0 ,141,401,137, 0 , 0 , 31, 32},
                {  43, 44, 0 , 0 ,140,139,138, 0 , 0 , 33, 34}
            };

            this.Loaded += LudoFrameworkElement_Loaded;
            this.MouseDown += LudoFrameworkElement_MouseDown;

        }

        private void LudoFrameworkElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void LudoFrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            Focusable = true;
            Focus();

            InvalidateVisual();
            //fullBoardRender = false;
        }

        private Point GetXY(int search)
        {
            Point coordinate = new Point();
            for (int x = 0; x < DIM; x++)
            {
                for (int y = 0; y < DIM; y++)
                {
                    if (fieldIDMatrix[x, y] == search)
                    {
                        coordinate.X = y; //!!
                        coordinate.Y = x; //!!
                    }
                }
            }
            return coordinate;
        }
        private void MoveMan(int from, int where)
        {
            DeleteMan(from);
            DrawMan(from, where);
        }
        private Geometry DrawMan(int from, int where)
        {
            EllipseGeometry ell = new EllipseGeometry();

            double X_man = (GetXY(where).X) * width / DIM; //width / DIM + 
            double Y_man = (GetXY(where).Y) * height / DIM; //height / DIM + 
            double W_man = width / DIM / DIM * (DIM - 2);
            double H_man = height / DIM / DIM * (DIM - 2);

            ell = new EllipseGeometry(new Rect(X_man, Y_man, W_man, H_man));
            //Console.WriteLine(W_man + " - " + H_man);
            //Console.WriteLine(X_man + " : " + Y_man);

            return ell;
        }
        private Geometry DrawManGraphics(int from, int where, double resize_param)
        {
            EllipseGeometry ell = new EllipseGeometry();

            double X_man = (GetXY(where).X) * width / DIM; //width / DIM + 
            double Y_man = (GetXY(where).Y) * height / DIM; //height / DIM + 
            double W_man = width / DIM / DIM * (DIM - resize_param);
            double H_man = height / DIM / DIM * (DIM - resize_param);

            ell = new EllipseGeometry(new Rect(X_man, Y_man, W_man, H_man));
            //Console.WriteLine(W_man + " - " + H_man);
            //Console.WriteLine(X_man + " : " + Y_man);

            return ell;
        }
        private void DeleteMan(int from) { DrawField(from); }

        private Geometry DrawField(int where)
        {
            RectangleGeometry rec = new RectangleGeometry();

            double X_field = X_offset + (GetXY(where).X * width / DIM);
            double Y_field = Y_offset + (GetXY(where).Y * height / DIM);
            double W_field = width / DIM;
            double H_field = height / DIM;

            rec = new RectangleGeometry(new Rect(X_field, Y_field, W_field, H_field));

            return rec;
        }
        private Brush IDToColor(int ID)
        {
            switch (ID)
            {
                case 11:
                case 12:
                case 13:
                case 14:
                case 101:
                case 102:
                case 103:
                case 104:
                case 110:
                    return Brushes.Red;
                case 21:
                case 22:
                case 23:
                case 24:
                case 201:
                case 202:
                case 203:
                case 204:
                case 120:
                    return Brushes.Blue;
                case 31:
                case 32:
                case 33:
                case 34:
                case 301:
                case 302:
                case 303:
                case 304:
                case 130:
                    return Brushes.Yellow;
                case 41:
                case 42:
                case 43:
                case 44:
                case 401:
                case 402:
                case 403:
                case 404:
                case 140:
                    return Brushes.Green;
                case 0:
                    return null;
                default: return Brushes.DimGray;
            }
        }
        private void Init(DrawingContext drawingContext)
        {
            foreach (int item in fieldIDMatrix)
            {
                if (item != 0)
                {
                    if (item / 10 < 5)
                    {
                        EllipseGeometry tmp = (EllipseGeometry)DrawMan(1, item);

                        drawingContext.DrawGeometry(
                            Brushes.Transparent,
                            new Pen(IDToColor(item), 1),
                            new RectangleGeometry(
                                new Rect(
                                    tmp.Bounds.Left,
                                    tmp.Bounds.Top,
                                    tmp.Bounds.Width,
                                    tmp.Bounds.Height)
                                    )
                                 );
                        drawingContext.DrawGeometry(IDToColor(item), new Pen(Brushes.Black, 2), DrawMan(1, item));
                        //decor...
                        drawingContext.DrawGeometry(IDToColor(item), new Pen(Brushes.Black, 1), DrawManGraphics(1, item, 3));
                        drawingContext.DrawGeometry(IDToColor(item), new Pen(Brushes.Black, 0.5), DrawManGraphics(1, item, 6.5));
                        drawingContext.DrawGeometry(IDToColor(item), new Pen(Brushes.Black, 1), DrawManGraphics(1, item, 7));
                        //...

                        //Console.WriteLine(IDToColor(item).ToString());
                        //Console.WriteLine(
                        //    DrawMan((int)GetXY(item).X, (int)GetXY(item).Y).Bounds.Left
                        //    + " - "
                        //    + DrawMan((int)GetXY(item).X, (int)GetXY(item).Y).Bounds.Top
                        //    + " : "
                        //    + DrawMan((int)GetXY(item).X, (int)GetXY(item).Y).Bounds.Width
                        //    + " - "
                        //    + DrawMan((int)GetXY(item).X, (int)GetXY(item).Y).Bounds.Height
                        //    );
                    }
                    else
                    {
                        if (IDToColor(item) == Brushes.DimGray)
                        {
                            drawingContext.DrawGeometry(Brushes.Transparent, new Pen(IDToColor(item), 1), DrawField(item));
                        }
                        else
                        {
                            drawingContext.DrawGeometry(IDToColor(item), new Pen(IDToColor(item), 1), DrawField(item));
                        }
                        //Console.WriteLine(
                        //    DrawField(item).Bounds.X
                        //    + " - "
                        //    + DrawField(item).Bounds.X
                        //    + " : "
                        //    + DrawField(item).Bounds.Width
                        //    + " - "
                        //    + DrawField(item).Bounds.Height
                        //    );
                    }
                }
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (fullBoardRender)
            {
                Init(drawingContext);
            }
            else
            {
                Console.WriteLine("TODO");
                //MoveMan();
            }

        }
    }
}

