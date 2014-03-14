// This code is public domain
#define USE_ANGLE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using OpenTK;
using OpenTK.Graphics;

#if USE_ANGLE
using OpenTK.Graphics.ES20;
#else
using OpenTK.Graphics.OpenGL;
#endif

namespace WPF.Angle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GLControl glControl;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowsFormsHost_Initialized(object sender, EventArgs e)
        {
            var flags = GraphicsContextFlags.Debug;
#if USE_ANGLE
            flags = GraphicsContextFlags.Embedded;
#endif
            glControl = new GLControl(new GraphicsMode(32, 24), 2, 0, flags);
            glControl.MakeCurrent();
            glControl.Paint += GLControl_Paint;
            glControl.Dock = DockStyle.Fill;
            (sender as WindowsFormsHost).Child = glControl;
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(
                (float)Red.Value,
                (float)Green.Value,
                (float)Blue.Value,
                1);
            GL.Clear(
                ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit |
                ClearBufferMask.StencilBufferBit);

            glControl.SwapBuffers();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            glControl.Invalidate();
        }
    }
}
