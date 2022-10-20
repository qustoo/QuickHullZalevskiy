using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickHullZalevskiy
{
    public partial class Form1 : Form
    {
        private List<Point> points;
        private List<Point> hull_points;
        private Graphics g;
        private Pen pen;
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.Image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            pen = new Pen(Color.Black, 1);
            points = new List<Point>();
            hull_points = new List<Point>();
        }
        // определяем точка p выше чем линия,началом и концом которой являются p_line1/2
        private bool IsLeftOnLine(Point p_line1, Point p_line2, Point p)
        {
            var a = p_line2.Y - p_line1.Y;
            var b = p_line1.X - p_line2.X;
            var c = -1 * (a * p_line1.X + b * p_line1.Y);

            return a* p.X + b * p.Y + c < 0;
        }
        // определяем точка p ниже чем линия,началом и концом которой являются p_line1/2
        private bool IsRightOnLine(Point p_line1, Point p_line2, Point p)
        {
            var a = p_line2.Y - p_line1.Y;
            var b = p_line1.X - p_line2.X;
            var c = -1 * (a * p_line1.X + b * p_line1.Y);
            return a * p.X + b * p.Y + c > 0;
        }
        private double GetSideTriangle(Point A, Point B) 
        {
            // получает сторону треугольника то двум точкам
            var first_elem = Math.Pow((B.X - A.X), 2);
            var second_elem = Math.Pow((B.Y - A.Y), 2);

            return Math.Sqrt(first_elem + second_elem);
        }
        private double GetSquareTriangle(Point A, Point B, Point C)
        {
            // по формуле герона s = sqrt(p * (p-a) * (p-b) * (p-c))
            var side_a = GetSideTriangle(A, B);
            var side_b = GetSideTriangle(B, C);
            var side_c = GetSideTriangle(A, C);

            var perimetr = (side_a + side_b + side_c )/ 2;
            return Math.Sqrt(perimetr * (perimetr - side_a) * (perimetr - side_b) * (perimetr - side_c));

        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            points.Add(new Point(e.X, e.Y));
            DrawingOriginalPoints(this.points);
        }

        private void button_clear_canvas_Click(object sender, EventArgs e)
        {
            g.Clear(Color.DarkGray);
            this.points = new List<Point>();
            this.pictureBox1.Invalidate();
        }
        private void DrawingOriginalPoints(List<Point> original_points)
        {
            foreach (var p in points)
            {
                g.DrawEllipse(new Pen(Color.Black, 3), p.X - 3, p.Y - 3, 6, 6);

            }
            this.pictureBox1.Invalidate();
        }
        private void button_gererate_quick_hull_Click(object sender, EventArgs e)
        {
            hull_points.Clear(); // обнуляем hull_points - массив точек
            g.Clear(this.pictureBox1.BackColor); // очищаем экран
            DrawingOriginalPoints(this.points); // рисуем те же точки
            if(this.points.Count < 3)
            {
                MessageBox.Show("Недостаточно точек!");
                return;
            }
            else
            {

                QuickHullGenerate(this.points, ref this.hull_points); // возвращает hull_point - точки, которые нужно соединить
                // соединяем точки
                for(int i =0;i<hull_points.Count-1;i++)
                {
                    g.DrawLine(new Pen(Color.Red, 1), hull_points[i], hull_points[i + 1]);
                }
                g.DrawLine(new Pen(Color.Red, 1), hull_points[0], hull_points[hull_points.Count - 1]);
                //g.DrawPolygon(new Pen(Color.Red, 1), hull_points.ToArray()); // соединяет точки
            }
            pictureBox1.Invalidate();          
        }
        private List<Point> GetNextPoint(List<Point> points, Point left_point, Point right_point)
        {
            List<Point> result_points = new List<Point>();
            double max_s = int.MinValue;
            Point max_for_square_point = new Point();
            // поиск точки при которой площадь треугольника - максимальна
            foreach(var p in points)
            {
                if(GetSquareTriangle(left_point,right_point,p) > max_s)
                {
                    max_s = GetSquareTriangle(p, left_point, right_point);
                    max_for_square_point = p;
                }
            }

            result_points.Add(max_for_square_point); // добавляем эту точку в список
            
            List<Point> UpperPointsForNextPoint =new List<Point>();
            List<Point> BellowPointsForNextPoint = new List<Point>();

            // для точек левее и правее от точки с максимальной площадью
            foreach(var p in points)
            {
                if(p!= left_point && p!=right_point && p!=max_for_square_point)
                {
                    if(IsLeftOnLine(left_point,max_for_square_point,p))
                    {
                        UpperPointsForNextPoint.Add(p);
                        //g.FillRectangle(Brushes.Blue, p.X - 3, p.Y - 3, 6, 6);
                    }
                    else
                    {
                        if(IsLeftOnLine(max_for_square_point,right_point,p))
                        {
                            BellowPointsForNextPoint.Add(p);
                            //g.FillRectangle(Brushes.Yellow, p.X - 3, p.Y - 3, 6, 6);
                        }
                    }
                }
            }
            if(UpperPointsForNextPoint.Count > 0)
            {
                result_points.AddRange(GetNextPoint(UpperPointsForNextPoint, left_point, max_for_square_point));
            }
            if(BellowPointsForNextPoint.Count > 0)
            {
                result_points.AddRange(GetNextPoint(BellowPointsForNextPoint, max_for_square_point, right_point));
            }
            return result_points;
        }
        private void QuickHullGenerate(List<Point> original_points, ref List<Point> hull_points)
        {
            original_points = original_points.OrderBy(p => p.X).ToList(); // сортируем список точек по иксу
            Point left_point_line = original_points[0]; // самая левая точка
            Point right_point_line = original_points[original_points.Count-1]; // самая правая точка
            // g.DrawLine(this.pen, left_point_line, right_point_line); // тестовая линия

            hull_points.Add(left_point_line); //самая левая точка
            hull_points.Add(right_point_line); // самая правая точка

            // массив точек выше и ниже прямой
            List<Point> UpperPoints = new List<Point>();
            List<Point> BellowPoints = new List<Point>();

            foreach (var p in original_points)
            {
                if (p != left_point_line && p != right_point_line)
                {
                    if (IsLeftOnLine(left_point_line, right_point_line, p))
                    {
                        //MessageBox.Show($" p is left = {p.X} {p.Y}");
                        UpperPoints.Add(p);
                        //g.DrawEllipse(new Pen(Color.Red, 3), p.X - 3, p.Y - 3, 6, 6);
                    }
                    else
                    {
                        //MessageBox.Show($" p is right = {p.X} {p.Y}");
                        BellowPoints.Add(p);
                        //g.DrawEllipse(new Pen(Color.Green, 3), p.X - 3, p.Y - 3, 6, 6);
                    }
                }
            }
            if(UpperPoints.Count > 0)
            {
                hull_points.AddRange(GetNextPoint(UpperPoints, left_point_line, right_point_line));
            }
            hull_points = hull_points.OrderBy(p => p.X).ToList(); // сортировка по возрастанию
            
            if(BellowPoints.Count > 0)
            {
                hull_points.AddRange((GetNextPoint(BellowPoints, right_point_line, left_point_line).OrderBy(p=>p.X).Reverse()).ToList());
            }
        }
    }
}
